using ReturnHome.Server.EntityObject.AI.Helpers;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.EntityObject.Stats;
using ReturnHome.Server.Managers;
using ReturnHome.Server.Network;
using ReturnHome.Server.Opcodes.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ReturnHome.Server.EntityObject.Actors
{
    public class Actor : Entity
    {
        public long killtime = 0;
        public int Tunar = 0;
        public Corpse corpse;
        public Dictionary<Character, int> aggroTable = new Dictionary<Character, int>();
        public long lastAtkTick = 0;

        public Actor() : base(false, 0)
        {

        }

        public Actor(string charName, float xCoord, float yCoord, float zCoord, int facing, int world, int modelid, float size,
            int primary, int secondary, int shield, int hair_color, int hair_length, int hair_style, int level, int torso, int forearms,
            int gloves, int legs, int feet, int head, EntityType npcType, int serverID) : base(false, level)
        {

            CharName = charName;
            x = xCoord;
            y = yCoord;
            z = zCoord;
            Position = new Vector3(x, y, z);
            Facing = (byte)facing;
            World = (World)world;
            ModelID = modelid;
            ModelSize = size;
            Primary = primary;
            Secondary = secondary;
            Shield = shield;
            HairColor = hair_color;
            HairLength = hair_length;
            HairStyle = hair_style;
            Level = level;
            Chest = (byte)torso;
            Bracer = (byte)forearms;
            Gloves = (byte)gloves;
            Legs = (byte)legs;
            Boots = (byte)feet;
            Helm = (byte)head;
            CurrentStats.Add(StatModifiers.STA, 300);
            HPFlag = true;
            Target = 0xFFFFFFFF;
            EntityType = npcType;
            ServerID = serverID;
            corpse = new(this);

            //staticly assign tunar onhand to a npc for now, only really relevant for when mobs die and money goes around
            //Inventory = new(3000);
        }

        public void AttackPlayer(int damage, uint targetID)
        {

            if (PlayerManager.QueryForPlayer(targetID, out Character player))
            {

            }

        }

        public void EvaluateAggroTable()
        {
            int damage = 3;
            lastAtkTick = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var c = aggroTable.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            ServerCastSpell.CastSpell(c.characterSession, 0xDA9BEA11, ObjectID, c.ObjectID, 0);
            if (EntityManager.QueryForEntity(c.ObjectID, out Entity player))
            {
                player.TakeDamage(c.characterSession, (uint)c.characterSession.MyCharacter.ObjectID, damage);
                //Blacksmith starts on 0x0008, 0x0018
            }
        }

        public void EvaluateAggro(int damage, Character c)
        {

            int mostAggro = damage;
            int aggro = 0;
            aggro += damage;

            if (aggroTable.ContainsKey(c))
            {
                aggroTable[c] += damage;
                //Console.WriteLine($"Aggro table already contains playerID {playerID}");
            }
            else
            {
                aggroTable.Add(c, damage);
            }
        }

        public void OnMobDeath()
        {
            Dead = true;
            _killTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Animation = (byte)AnimationState.Die;
            //Move npc inventory to Loot Object for npc's
            if (!isPlayer)
                if (Inventory != null)
                    corpse.UpdateCorpseOnDeath(Inventory.itemContainer);

            foreach (KeyValuePair<Character, int> c in aggroTable)
            {
                GrantXP(c.Key.characterSession, ExperienceCalculations.CalculateMobXP(Level, c.Key.Level));
            }
            aggroTable.Clear();


        }

        public void OnMobSpawn()
        {

        }
    }
}
