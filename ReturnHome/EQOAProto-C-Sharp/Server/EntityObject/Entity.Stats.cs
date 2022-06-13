using System;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
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
            set { _baseStrength = value; }
        }

        public int BaseStamina
        {
            get { return _baseStamina; }
            set { _baseStamina = value; }
        }

        public int BaseWisdom
        {
            get { return _baseWisdom; }
            set { _baseWisdom = value; }
        }

        public int BaseIntelligence
        {
            get { return _baseIntelligence; }
            set { _baseIntelligence = value; }
        }

        public int BaseDexterity
        {
            get { return _baseDexterity; }
            set { _baseDexterity = value; }
        }

        public int BaseAgility
        {
            get { return _baseAgility; }
            set { _baseAgility = value; }
        }

        public int BaseCharisma
        {
            get { return _baseCharisma; }
            set { _baseCharisma = value; }
        }

        #endregion

        #region Properties for stats and private var's

        //Our most current stats. This is stat totals influenced by gear, buff's, CM's and TP's.
        private int _strength;
        private int _stamina;
        private int _wisdom;
        private int _intelligence;
        private int _charisma;
        private int _agility;
        private int _dexterity;

        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public int Stamina
        {
            get { return _stamina; }
            set { _stamina = value; }
        }

        public int Wisdom
        {
            get { return _wisdom; }
            set { _wisdom = value; }
        }

        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }

        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }

        public int Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        public int Charisma
        {
            get { return _charisma; }
            set { _charisma = value; }
        }

        #endregion

        #region Properties of base/current HP/Power/AC and private var's

        //This is decided by the character's arch type + CM's, should npc/boss type mobs have special modifier's?
        private byte _hpFactor;

        private int _baseAC;
        private int _baseHP;
        private int _basePower;

        public int BaseAC
        {
            get { return _baseAC; }
            set
            {
                _baseAC = value;
            }
        }

        public int BaseHP
        {
            get { return _baseHP; }
            set
            {
                _baseHP = value;
            }
        }

        public int BasePower
        {
            get { return _basePower; }
            set
            {
                _basePower = value;
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
            set { _currentAC = value; }
        }

        public int CurrentHP
        {
            get { return _currentHP; }
            set
            {
                if (value <= _hpMax)
                {
                    //TODO: Player/NPC died, make needed adjustments
                    if (value <= 0)
                        Console.WriteLine($"{CharName} died.");

                    else
                        _currentHP = value;
                }

                else
                    _currentHP = _hpMax;

                ObjectUpdateHPBar();

            }
        }

        public int CurrentPower
        {
            get { return _currentPower; }
            set { _currentPower = value; }
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
                if (true)
                {
                    _hpMax = value;
                    ObjectUpdateHPBar();
                }
            }
        }

        public int PowerMax
        {
            get { return _powerMax; }
            set
            {
                if (true)
                {
                    _powerMax = value;
                }
            }
        }

        public bool HPFlag
        {
            get { return _hpFlag; }
            set
            {
                if (true)
                {
                    _hpFlag = value;
                    ObjectUpdateHPFlag();
                }
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
            set { _healthOverTime = value; }
        }

        public int PowerOverTime
        {
            get { return _powerOverTime; }
            set { _powerOverTime = value; }
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
            set { _basePoisonResist = value; }
        }

        public int BaseDiseaseResist
        {
            get { return _baseDiseaseResist; }
            set { _baseDiseaseResist = value; }
        }

        public int BaseLightningResist
        {
            get { return _baseLightningResist; }
            set { _baseLightningResist = value; }
        }

        public int BaseFireResist
        {
            get { return _baseFireResist; }
            set { _baseFireResist = value; }
        }

        public int BaseColdResist
        {
            get { return _baseColdResist; }
            set { _baseColdResist = value; }
        }

        public int BaseArcaneResist
        {
            get { return _baseArcaneResist; }
            set { _baseArcaneResist = value; }
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
            set { _poisonResist = value; }
        }

        public int DiseaseResist
        {
            get { return _diseaseResist; }
            set { _diseaseResist = value; }
        }

        public int LightningResist
        {
            get { return _lightningResist; }
            set { _lightningResist = value; }
        }

        public int FireResist
        {
            get { return _fireResist; }
            set { _fireResist = value; }
        }

        public int ColdResist
        {
            get { return _coldResist; }
            set { _coldResist = value; }
        }

        public int ArcaneResist
        {
            get { return _arcaneResist; }
            set { _arcaneResist = value; }
        }

        #endregion

        private int _fishing;

        public void CalculateHP()
        {
            //BaseHP is calculated by utilizing the current Stamina of the player + and HP Factors they may have
            BaseHP = (Level * ((_hpFactor * 100) + (Stamina * 100) / 11)) / 100;

            //Utilizing BaseHP + Gear HP + buffs
            HPMax = BaseHP + GearBuffHPMax; 
        }
    }
}
