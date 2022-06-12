using System;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        //Base starting stats based on race/class, along with TP spent and CM's acquired
        public int BaseStrength;
        public int BaseStamina;
        public int BaseAgility;
        public int BaseDexterity;
        public int BaseWisdom;
        public int BaseIntelligence;
        public int BaseCharisma;
        private int _randoValue = 0x64;

            //Our most current stats. This is stats influenced by gear and buff's.
        private int _strength;
        private int _stamina;
        private int _wisdom;
        private int _intelligence;
        private int _charisma;
        private int _agility;
        private int _dexterity;

        //Influenced by CM's and gear and buffs
        private int _ac;

        //Calculated on the fly during combat etc
        private bool _hpFlag;
        private int _currentHP;
        private int _currentPower;

        private int _arcaneResist = 0;
        private int _poisonResist = 0;
        private int _diseaseResist = 0;
        private int _lightningResist = 0;
        private int _coldResist = 0;
        private int _fireResist = 0;

        //This is decided by the character's arch type, should npc/boss type mobs have special modifier's?
        private byte HPModifier = 0;

        private int _fishing = 0;

        //Need to calculate HP/PWR max on the fly
        //Will be influenced by archtype, level, stamina, gear, buffs and CM's
        private int _hpMax;
        private int _powerMax = 0;

        //Related to regeneration of health and PowerOverTime
        //There is a standard rate of 50 HP/PWR = 1 HoT/PoT, then buffs/heal's can get stacked ontop of this passive healing, incling gear/CM's that add directly to it
        public int _healthOverTime;
        public int _powerOverTime;

        #region Properrty sets

        public int CurrentHP
        {
            get { return _currentHP; }
            set
            {
                if (true)
                {
                    _currentHP = value;
                    ObjectUpdateHPBar();
                }
            }
        }

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

        public bool HPFlag
        {
            get { return _hpFlag; }
            set
            {
                if(true)
                {
                    _hpFlag = value;
                    ObjectUpdateHPFlag();
                }
            }
        }

        //Add more evtnually
        public int Strength
        {
            get {  return _strength; }
            set
            {
                _strength = value;
            }
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

        public int CurrentPower
        {
            get { return _currentPower; }
            set { _currentPower = value; }
        }

        public int PowerMax
        {
            get { return _powerMax; }
            set { _powerMax = value; }
        }

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

        public int AC
        {
            get { return _ac; }
            set { _ac = value; }
        }

        public int PoisonResist
        {
            get { return _poisonResist; }
            set { _poisonResist = value; }
        }

        public int DiseaseResist
        {
            get {  return _diseaseResist; }
            set { _diseaseResist = value; }
        }

        public int LightningResist
        {
            get { return _lightningResist; }
            set {  _lightningResist = value; }
        }

        public int FireResist
        {
            get { return _fireResist; }
            set {  _fireResist = value; }
        }

        public int ColdResist
        {
            get {  return _coldResist; }
            set { _coldResist = value; }
        }

        public int ArcaneResist
        {
            get { return _arcaneResist; }
            set { _arcaneResist = value; }
        }

        #endregion
    }
}
