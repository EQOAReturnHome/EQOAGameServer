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
    public int Strength;
    public int Stamina;
    public int Wisdom;
    public int Intelligence;
    public int Charisma;
    public int Agility;
    public int Dexterity;

    //Influenced by CM's and gear and buffs
    public int AC;

    //Calculated on the fly during combat etc
    private bool _hpFlag;
    private int _currentHP;
    public int CurrentPower;

    public int ArcaneResist = 0;
    public int PoisonResist = 0;
    public int DiseaseResist = 0;
    public int LightningResist = 0;
    public int ColdResist = 0;
    public int FireResist = 0;

    //This is decided by the character's arch type, should npc/boss type mobs have special modifier's?
    private byte HPModifier = 0;

    //Need to calculate HP/PWR max on the fly
    //Will be influenced by archtype, level, stamina, gear, buffs and CM's
    private int _hpMax;
    public int PowerMax = 0;

    //Related to regeneration of health and PowerOverTime
    //There is a standard rate of 50 HP/PWR = 1 HoT/PoT, then buffs/heal's can get stacked ontop of this passive healing, incling gear/CM's that add directly to it
    public int HealthOverTime;
    public int PowerOverTime;

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

        #endregion
    }
}
