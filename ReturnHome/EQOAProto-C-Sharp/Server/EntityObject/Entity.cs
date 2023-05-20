using System;
using ReturnHome.Server.EntityObject.Items;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.EntityObject.Stats;
using ReturnHome.Utilities;
using ReturnHome.Server.EntityObject.Spells;
using ReturnHome.Server.Network;
using ReturnHome.Server.EntityObject.Effect;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Xml.Linq;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Server.Managers;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {

        public EquippedGear equippedGear;
        //Implies if object is visible or not
        public bool Invisible = false;

        public List<StatusEffect> EntityStatusEffects = new List<StatusEffect>();
        public SpellBook MySpellBook;
        private int _level;
        private uint _objectID;
        public long _killTime;
        public long deathTime = 0;
        public long respawnTime = 0;

        private EntityType _npcType = 0;
        public int ServerID;
        public AIContainer aiContainer;
        public int _respawnTime = 30;
        public bool despawn { get; set; } = false;
        public bool canRespawn { get; set; } = true;
        public bool respawn { get; set; } = false;


        public byte chatMode = 0; //Default to 0, say = 0, Shout = 3 NPC's can technically talk in chat too?

        //Store latest character update directly to character for other characters to pull
        //Doesn't seem right? But we can trigger each session to serialize to this array and distribute to other client's this way
        public Memory<byte> ObjectUpdate = new Memory<byte>(new byte[0xC8]);
        public Memory<byte> StatUpdate = new Memory<byte>(new byte[0xEC]);
        public Memory<byte> GroupUpdate = new Memory<byte>(new byte[0X27]);
        //TODO: Need to calculate the variable length for this later. Max length for now for testing
        public Memory<byte> BuffUpdate = new Memory<byte>(new byte[1060]);


        /* These are all values for character creation, likely don't need to be attributes of the character object at all*/
        //Default character data should probably be stored in script's to generate from on client's request, saving that to the database
        /*CONSIDER REMOVING IN FAVOR OF ABOVE IN TIME?*/

        public bool isPlayer;

        #region ObjectUpdate
        public int Level
        {
            get { return _level; }
            set
            {
                if (isPlayer)
                {
                    if (value >= 1 && value <= 61)
                    {
                        _level = value;
                        ObjectUpdateLevel();
                    }

                    else
                        Logger.Err($"Error setting Level {value} for {_charName}");
                }
                else
                {
                    if (value >= 1 && value <= 100)
                    {
                        _level = value;
                        ObjectUpdateLevel();
                    }
                }
            }
        }

        public uint ObjectID
        {
            get { return _objectID; }
            set
            {
                if (true)
                {
                    _objectID = value;
                    ObjectUpdateObjectID();
                }

                else
                    Logger.Err($"Error setting ObjectID {value} for {_charName}");
            }
        }

        public long KillTime
        {
            get { return _killTime; }
            set
            {
                //I think kill time only applies to npc's?
                if (!isPlayer)
                {
                    _killTime = value;
                    ObjectUpdateKillTime();
                }
            }
        }

        public EntityType EntityType
        {
            get { return _npcType; }
            set
            {
                if (true)
                {
                    _npcType = value;
                    ObjectUpdateNPCType();
                }
            }
        }
        #endregion
        public Entity(bool isplayer, int Level2)
        {
            isPlayer = isplayer;
            CurrentStats = new ModifierDictionary(this);
            if (isPlayer)
                equippedGear = new(this);
            #region Stat stuff
            //Players have limits on stats, NPC's will not
            if (isplayer)
            {
                if (Level2 < 45)
                    BaseMaxStat = 350;

                else
                    BaseMaxStat = 400;
            }

            //NPC, no limits atm
            else
                BaseMaxStat = 100000;

            #endregion
            ObjectUpdateEntity();
            ObjectUpdateVanillaColors();
            ObjectUpdateEnd();
            ObjectUpdateNameColor();
            ObjectUpdateNamePlate();
            ObjectUpdateUnknown();
            ObjectUpdatePattern();
            if (isPlayer)
            {
                ObjectUpdateOnline();
            }
            //Set armour to defaults
            HelmColor = 0xFFFFFFFF;
            ChestColor = 0xFFFFFFFF;
            BracerColor = 0xFFFFFFFF;
            GloveColor = 0xFFFFFFFF;
            LegColor = 0xFFFFFFFF;
            BootsColor = 0xFFFFFFFF;
            RobeColor = 0xFFFFFFFF;
            Robe = -1;
            ModelSize = 1.0f;
            Movement = 1;
        }
        public static string GetClass(Class playerClass)
        {
            string className = playerClass.ToString().Split(':')[0];
            return className;
        }

        public static string GetRace(Race playerRace)
        {
            string raceName = playerRace.ToString().Split(':')[0];
            return raceName;
        }

        public static string GetHumanType(HumanType humanType)
        {
            string playerHumanType = humanType.ToString().Split(':')[0];
            return playerHumanType;
        }

        public bool IsDead()
        {
            if (this.Dead)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PostUpdate(DateTime tick)
        {

        }

        public void IsValidTarget()
        {
        }


        public void RemoveStatusEffect(string effectName)
        {

            EntityStatusEffects.RemoveAll(s => s.name == effectName);

            BufferWriter writer = new(BuffUpdate.Span);

            int size;
            if (EntityStatusEffects.Count >= 8)
                size = 8;
            else
                size = EntityStatusEffects.Count;

            writer.Write(size);

            for (int i = 0; i < size; ++i)
                writer.Write(EntityStatusEffects[i].status);
        }


        public void AddStatusEffect(int id, string name, uint effectIcon, uint duration, uint tier)
        {
            StatusEffect statusEffect = new StatusEffect(id, name, effectIcon, duration, tier);

            //Console.WriteLine($"Status effect has name {statusEffect.name}, and icon {statusEffect.icon}");

            EntityStatusEffects.Add(statusEffect);

            BufferWriter writer = new(BuffUpdate.Span);

            int size;
            if (EntityStatusEffects.Count >= 8)
                size = 8;
            else
                size = EntityStatusEffects.Count;

            writer.Write(size);

            for (int i = 0; i < size; ++i)
                writer.Write(EntityStatusEffects[i].status);
        }

        public bool CanAttack()
        {
            return true;
        }

        public bool CanUse()
        {
            return false;
        }


        public void GetAttackDelayMs()
        {
        }

        public void GetAttackRange()
        {
        }

        public virtual bool Engage()
        {
            return false;
        }

        public virtual bool Engage(Character target)
        {
            aiContainer.Engage(target);
            return false;
        }

        public bool IsEngaged()
        {
            return aiContainer.IsEngaged();
        }

        public bool Disengage()
        {
            /*if (newMainState != 0xFFFF)
            {
                currentMainState = newMainState;// this.newMainState = newMainState;
                updateFlags |= ActorUpdateFlags.State;
            }
            else*/
            if (IsEngaged())
            {
                aiContainer.Disengage();
                return true;
            }
            return false;
        }

        public virtual void Cast(uint spellId, uint targetId = 0)
        {
            /*if (aiContainer.CanChangeState())
                aiContainer.Cast(zone.FindActorInArea<Character>(targetId == 0 ? currentTarget : targetId), spellId);*/
        }

        public virtual void Ability(uint abilityId, uint targetId = 0)
        {
            /*if (aiContainer.CanChangeState())
                aiContainer.Ability(zone.FindActorInArea<Character>(targetId == 0 ? currentTarget : targetId), abilityId);*/
        }

        public virtual void WeaponSkill(uint skillId, uint targetId = 0)
        {
            /*if (aiContainer.CanChangeState())
                aiContainer.WeaponSkill(zone.FindActorInArea<Character>(targetId == 0 ? currentTarget : targetId), skillId);*/
        }

        public virtual void Spawn(DateTime tick)
        {
            aiContainer.Reset();
            // todo: reset hp/mp/tp etc here
            //ChangeState(SetActorStatePacket.MAIN_STATE_PASSIVE);
            SetHP((uint)GetMaxHP());
            SetMP((uint)GetMaxMP());

        }

        //AdditionalActions is the list of actions that EXP/Chain messages are added to
        public virtual void Die(DateTime tick)
        {
            // todo: actual despawn timer
        }

        public virtual void Despawn(DateTime tick)
        {

        }

        public bool IsAlive()
        {
            if (CurrentHP > 0)
            {
                return true;
            }
            else { return false; }
            //return !aiContainer.IsDead();// && GetHP() > 0;
        }

        public int GetHP()
        {
            // todo: 
            return this.CurrentHP;
        }

        public int GetMaxHP()
        {
            return HPMax;
        }

        public int GetMP()
        {
            return this.CurrentPower;
        }

        public void GetTP()
        {

        }

        public int GetMaxMP()
        {
            return this.PowerMax;
        }


        public void SetHP(uint hp)
        {

        }

        public void SetMaxHP(uint hp)
        {

        }

        public void SetMP(uint mp)
        {

        }

        public void SetMaxMP(uint mp)
        {

        }

        // todo: the following functions are virtuals since we want to check hidden item bonuses etc on player for certain conditions
        public virtual void AddHP(int hp)
        {

        }

        public Class GetClass()
        {
            return this.EntityClass;
        }

        public static int GetLevel(Session session)
        {
            return session.MyCharacter.Level;
        }


    }
}
