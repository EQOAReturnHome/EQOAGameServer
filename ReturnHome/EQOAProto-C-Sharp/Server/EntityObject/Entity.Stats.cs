using System;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        //This is the absolute max amount of a "base" stat that can be assigned, everything above here needs to be buff's and CM's
        public readonly int BaseMaxStat;
        private int _minimumStat = -100;
        private int _randoValue = 0x64;

        #region Properties of Base Stats and private Var's
        //Base starting stats based on race/class, along with TP spent and CM's acquired
        private int _baseStrength;
        private int _baseStamina;
        private int _baseAgility;
        private int _baseDexterity;
        private int _baseWisdom;
        private int _baseIntelligence;
        private int _baseCharisma;

        public int BaseStrength
        {
            get { return _baseStrength; }
            set
            {
                _baseStrength = value;
                CalculateStrength();
                UpdateBaseStrength();
            }
        }

        public int BaseStamina
        {
            get { return _baseStamina; }
            set
            {
                _baseStamina = value;
                CalculateStamina();
                UpdateBaseStamina();
            }
        }

        public int BaseWisdom
        {
            get { return _baseWisdom; }
            set
            {
                _baseWisdom = value;
                CalculateWisdom();
                UpdateBaseWisdom();
            }
        }

        public int BaseIntelligence
        {
            get { return _baseIntelligence; }
            set
            {
                _baseIntelligence = value;
                CalculateIntelligence();
                UpdateBaseIntelligence();
            }
        }

        public int BaseDexterity
        {
            get { return _baseDexterity; }
            set
            {
                _baseDexterity = value;
                CalculateDexterity();
                UpdateBaseDexterity();
            }
        }

        public int BaseAgility
        {
            get { return _baseAgility; }
            set
            {
                _baseAgility = value;
                CalculateAgility();
                UpdateBaseAgility();
            }
        }

        public int BaseCharisma
        {
            get { return _baseCharisma; }
            set
            {
                _baseCharisma = value;
                CalculateCharisma();
                UpdateBaseCharisma();
            }
        }

        #endregion

        #region Properties for stats and private var's

        //Our most current stats. This is stat totals influenced by gear, buff's, CM's and TP's.
        private int _strength;
        public int GearBuffStrength = 0;
        private int _stamina;
        public int GearBuffStamina = 0;
        private int _wisdom;
        public int GearBuffWisdom = 0;
        private int _intelligence;
        public int GearBuffIntelligence = 0;
        private int _charisma;
        public int GearBuffCharisma = 0;
        private int _agility;
        public int GearBuffAgility = 0;
        private int _dexterity;
        public int GearBuffDexterity = 0;

        public int Strength
        {
            get { return Clamp(_strength, _minimumStat, MaxStrength); }
            private set
            {
                _strength = value;
                CalculatePower();
                UpdateStrength();
            }
        }

        public int Stamina
        {
            get { return Clamp(_stamina, _minimumStat, MaxStamina); }
            private set
            {
                _stamina = value;
                CalculatePower();
                CalculateHP();
                UpdateStamina();
            }
        }

        public int Wisdom
        {
            get { return Clamp(_wisdom, _minimumStat, MaxWisdom); }
            private set
            {
                _wisdom = value;
                CalculatePower();
                UpdateWisdom();
            }
        }

        public int Intelligence
        {
            get { return Clamp(_intelligence, _minimumStat, MaxIntelligence); }
            private set
            {
                _intelligence = value;
                CalculatePower();
                UpdateIntelligence();
            }
        }

        public int Dexterity
        {
            get { return Clamp(_dexterity, _minimumStat, MaxDexterity); }
            private set
            {
                _dexterity = value;
                CalculatePower();
                UpdateDexterity();
            }
        }

        public int Agility
        {
            get { return Clamp(_agility, _minimumStat, MaxAgility); }
            private set
            {
                _agility = value;
                CalculatePower();
                UpdateAgility();
            }
        }

        public int Charisma
        {
            get { return Clamp(_charisma, _minimumStat, MaxCharisma); }
            private set
            {
                _charisma = value;
                CalculatePower();
                UpdateCharisma();
            }
        }

        #endregion

        #region Properties for Stat Maxes and Private Var's

        //Our most current stats. This is stat totals influenced by gear, buff's, CM's and TP's.
        private int _maxStrength;
        private int _maxStamina;
        private int _maxWisdom;
        private int _maxIntelligence;
        private int _maxCharisma;
        private int _maxAgility;
        private int _maxDexterity;

        public int MaxStrength
        {
            get { return _maxStrength + BaseMaxStat; }
            set { _maxStrength = value; }
        }

        public int MaxStamina
        {
            get { return _maxStamina + BaseMaxStat; }
            set { _maxStamina = value; }
        }

        public int MaxWisdom
        {
            get { return _maxWisdom + BaseMaxStat; }
            set { _maxWisdom = value; }
        }

        public int MaxIntelligence
        {
            get { return _maxIntelligence + BaseMaxStat; }
            set { _maxIntelligence = value; }
        }

        public int MaxDexterity
        {
            get { return _maxDexterity + BaseMaxStat; }
            set { _maxDexterity = value; }
        }

        public int MaxAgility
        {
            get { return _maxAgility + BaseMaxStat; }
            set { _maxAgility = value; }
        }

        public int MaxCharisma
        {
            get { return _maxCharisma + BaseMaxStat; }
            set { _maxCharisma = value; }
        }

        #endregion

        #region Properties of base/current HP/Power/AC and private var's

        //This is decided by the character's arch type + CM's, should npc/boss type mobs have special modifier's?
        private int _hpFactor;

        private int _baseAC;
        private int _baseHP;
        private int _basePower;

        public int HPFactor
        {
            get { return _hpFactor; }
            set
            {
                _hpFactor = value;
            }
        }
        public int BaseAC
        {
            get { return _baseAC; }
            set
            {
                _baseAC = value;
                UpdateBaseAC();
            }
        }

        public int BaseHP
        {
            get { return _baseHP; }
            set
            {
                _baseHP = value;
                CurrentHP = _baseHP;
                UpdateBaseHP();
            }
        }

        public int BasePower
        {
            get { return _basePower; }
            set
            {
                _basePower = value;
                UpdateBasePower();
            }
        }

        //Gear and Buff's should write to this variable to add to the total HPMax Value
        public int GearBuffHPMax = 0;
        public int GearBuffPowerMax = 0;
        public int GearBuffAC = 0;

        private int _currentAC;
        private int _currentHP;
        private int _currentPower;
        private bool _hpFlag;

        public int CurrentAC
        {
            get { return _currentAC; }
            set
            {
                _currentAC = value;
                UpdateAC();
            }
        }

        public int CurrentHP
        {
            get { return _currentHP; }
            set
            {

                if (value > HPMax)
                    _currentHP = _hpMax;

                else
                    _currentHP = value;

                //TODO: Trigger dying stuff here?
                if (value <= 0)
                    Console.WriteLine($"{CharName} died.");

                //Update HP for stat message
                UpdateCurrentHP();

                //Update HP bar for the object update message
                ObjectUpdateHPBar();
            }
        }

        public int CurrentPower
        {
            get { return _currentPower; }
            set
            {
                _currentPower = value;
                UpdateCurrentPower();
            }
        }

        //Need to calculate HP/PWR max on the fly
        //Will be influenced by archtype, level, stamina, gear, buffs and CM's
        private int _hpMax;
        private int _powerMax;

        public int HPMax
        {
            get { return _hpMax; }
            set
            {
                _hpMax = value;
                UpdateMaxHP();
                ObjectUpdateHPBar();

                if (_currentHP > _hpMax)
                    _currentHP = _hpMax;
            }
        }

        public int PowerMax
        {
            get { return _powerMax; }
            set
            {
                _powerMax = value;
                UpdateMaxPower();
            }
        }

        public bool HPFlag
        {
            get { return _hpFlag; }
            set
            {
                _hpFlag = value;
                ObjectUpdateHPFlag();
            }
        }
        #endregion

        #region Properties of HoT/PoT

        //Related to regeneration of health and PowerOverTime
        //There is a standard rate of 50 HP/PWR = 1 HoT/PoT, then buffs/heal's can get stacked ontop of this passive healing, incling gear/CM's that add directly to it
        private int _healthOverTime;
        private int _powerOverTime;

        public int HealthOverTime
        {
            get { return _healthOverTime; }
            set
            {
                _healthOverTime = value;
                UpdateHealthOverTime();
            }
        }

        public int PowerOverTime
        {
            get { return _powerOverTime; }
            set
            {
                _powerOverTime = value;
                UpdatePowerOverTime();
            }
        }

        #endregion

        #region Properties of Base resists + current Resists

        private int _baseArcaneResist;
        private int _basePoisonResist;
        private int _baseDiseaseResist;
        private int _baseLightningResist;
        private int _baseColdResist;
        private int _baseFireResist;

        public int BasePoisonResist
        {
            get { return _basePoisonResist; }
            set
            {
                _basePoisonResist = value;
                UpdateBasePoisonResist();
            }
        }

        public int BaseDiseaseResist
        {
            get { return _baseDiseaseResist; }
            set
            {
                _baseDiseaseResist = value;
                UpdateBaseDiseaseResist();
            }
        }

        public int BaseLightningResist
        {
            get { return _baseLightningResist; }
            set
            {
                _baseLightningResist = value;
                UpdateBaseLightningResist();
            }
        }

        public int BaseFireResist
        {
            get { return _baseFireResist; }
            set
            {
                _baseFireResist = value;
                UpdateBaseFireResist();
            }
        }

        public int BaseColdResist
        {
            get { return _baseColdResist; }
            set
            {
                _baseColdResist = value;
                UpdateBaseColdResist();
            }
        }

        public int BaseArcaneResist
        {
            get { return _baseArcaneResist; }
            set
            {
                _baseArcaneResist = value;
                UpdateBaseArcaneResist();
            }
        }

        private int _arcaneResist;
        private int _poisonResist;
        private int _diseaseResist;
        private int _lightningResist;
        private int _coldResist;
        private int _fireResist;

        public int PoisonResist
        {
            get { return _poisonResist; }
            set
            {
                _poisonResist = value;
                UpdatePoisonResist();
            }
        }

        public int DiseaseResist
        {
            get { return _diseaseResist; }
            set
            {
                _diseaseResist = value;
                UpdateDiseaseResist();
            }
        }

        public int LightningResist
        {
            get { return _lightningResist; }
            set
            {
                _lightningResist = value;
                UpdateLightningResist();
            }
        }

        public int FireResist
        {
            get { return _fireResist; }
            set
            {
                _fireResist = value;
                UpdateFireResist();
            }
        }

        public int ColdResist
        {
            get { return _coldResist; }
            set
            {
                _coldResist = value;
                UpdateColdResist();
            }
        }

        public int ArcaneResist
        {
            get { return _arcaneResist; }
            set
            {
                _arcaneResist = value;
                UpdateArcaneResist();
            }
        }

        #endregion

        private int _fishing;

        private void CalculateHP()
        {
            //BaseHP is calculated by utilizing the current Stamina of the player + and HP Factors they may have
            BaseHP = (Level * ((_hpFactor * 100) + (Stamina * 100) / 11)) / 100;

            //Utilizing BaseHP + Gear HP + buffs
            HPMax = BaseHP + GearBuffHPMax;

            //For now set current HP to HP MAX
            CurrentHP = BaseHP;
        }

        private void CalculatePower()
        {
            //Create a switch expression to calculate Base Power
            BasePower = _class switch
            {
                //Warrior/Paladin/Shadowknight
                Class.Warrior or Class.Paladin or Class.ShadowKnight => Calculatetemp(Strength, Stamina),

                //Ranger/Rogue
                Class.Ranger or Class.Rogue => Calculatetemp(Agility, Dexterity),

                //Monk/Bard
                Class.Monk or Class.Bard => Calculatetemp(Agility, Wisdom),

                //Druid
                Class.Druid => Calculatetemp(Wisdom, Dexterity),

                //Shaman
                Class.Shaman => Calculatetemp(Wisdom, Stamina),

                //Cleric
                Class.Cleric => Calculatetemp(Wisdom, Charisma),

                //Magician
                Class.Magician => Calculatetemp(Intelligence, Agility),

                //Necromancer
                Class.Necromancer => Calculatetemp(Intelligence, Stamina),

                //Enchanter
                Class.Enchanter => Calculatetemp(Intelligence, Charisma),

                //Wizard
                Class.Wizard => Calculatetemp(Intelligence, Dexterity),

                //Alchemist
                Class.Alchemist => Calculatetemp(Intelligence, Wisdom),

                _ => throw new NotImplementedException()
            }; // + CMPower;

            PowerMax = BasePower + GearBuffPowerMax;

            CurrentPower = BasePower;
        }
        private void CalculateStrength() => Strength = BaseStrength + GearBuffStrength;
        private void CalculateStamina() => Stamina = BaseStamina + GearBuffStamina;
        private void CalculateAgility() => Agility = BaseAgility + GearBuffAgility;
        private void CalculateWisdom() => Wisdom = BaseWisdom + GearBuffWisdom;
        private void CalculateDexterity() => Dexterity = BaseDexterity + GearBuffDexterity;
        private void CalculateIntelligence() => Intelligence = BaseIntelligence + GearBuffIntelligence;
        private void CalculateCharisma() => Charisma = BaseCharisma + GearBuffCharisma;

        private int Calculatetemp(int Primary, int Secondary) => (((28 * 100) + ((Primary * 100) / 10) + ((Secondary * 100) / 20)) * Level) / 100;

        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
                throw new ArgumentException($"{nameof(min)} can not exceed {nameof(max)}");

            if (value < min)
                return min;
            else if (value > max)
                return max;

            return value;
        }
    }
}
