using System.Runtime.InteropServices;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        public void UpdateStrength() => MemoryMarshal.Write(StatUpdate.Span[0..], ref _strength);

        public void UpdateStamina() => MemoryMarshal.Write(StatUpdate.Span[4..], ref _stamina);

        public void UpdateAgility() => MemoryMarshal.Write(StatUpdate.Span[8..], ref _agility);

        public void UpdateDexterity() => MemoryMarshal.Write(StatUpdate.Span[12..], ref _dexterity);

        public void UpdateWisdom() => MemoryMarshal.Write(StatUpdate.Span[16..], ref _wisdom);

        public void UpdateIntelligence() => MemoryMarshal.Write(StatUpdate.Span[20..], ref _intelligence);

        public void UpdateCharisma() => MemoryMarshal.Write(StatUpdate.Span[24..], ref _charisma);

        public void UpdateCurrentHP()
        {
            MemoryMarshal.Write(StatUpdate.Span[28..], ref _currentHP);
            MemoryMarshal.Write(StatUpdate.Span[144..], ref _currentHP);
        }
        public void UpdateMaxHP() => MemoryMarshal.Write(StatUpdate.Span[32..], ref _hpMax);

        public void UpdateCurrentPower()
        {
            MemoryMarshal.Write(StatUpdate.Span[36..], ref _currentPower);
            MemoryMarshal.Write(StatUpdate.Span[152..], ref _currentPower);
        }

        public void UpdateMaxPower() => MemoryMarshal.Write(StatUpdate.Span[40..], ref _powerMax);

        public void UpdateUnknown1()
        {
            MemoryMarshal.Write(StatUpdate.Span[44..], ref _randoValue);
            MemoryMarshal.Write(StatUpdate.Span[160..], ref _randoValue);
        }

        public void UpdateHealthOverTime()
        {
            MemoryMarshal.Write(StatUpdate.Span[48..], ref _healthOverTime);
            MemoryMarshal.Write(StatUpdate.Span[164..], ref _healthOverTime);
        }

        public void UpdatePowerOverTime()
        {
            MemoryMarshal.Write(StatUpdate.Span[52..], ref _powerOverTime);
            MemoryMarshal.Write(StatUpdate.Span[168..], ref _powerOverTime);
        }

        public void UpdateAC() => MemoryMarshal.Write(StatUpdate.Span[56..], ref _ac);

        public void UpdatePoisonResist() => MemoryMarshal.Write(StatUpdate.Span[88..], ref _poisonResist);

        public void UpdateDiseaseResist() => MemoryMarshal.Write(StatUpdate.Span[92..], ref _diseaseResist);

        public void UpdateFireResist() => MemoryMarshal.Write(StatUpdate.Span[96..], ref _fireResist);

        public void UpdateColdResist() => MemoryMarshal.Write(StatUpdate.Span[100..], ref _coldResist);

        public void UpdateLightningResist() => MemoryMarshal.Write(StatUpdate.Span[104..], ref _lightningResist);

        public void UpdateArcaneResist() => MemoryMarshal.Write(StatUpdate.Span[108..], ref _arcaneResist);

        public void UpdateFishing() => MemoryMarshal.Write(StatUpdate.Span[112..], ref _fishing);

        public void UpdateBaseStrength() => MemoryMarshal.Write(StatUpdate.Span[116..], ref BaseStrength);

        public void UpdateBaseStamina() => MemoryMarshal.Write(StatUpdate.Span[120..], ref BaseStamina);

        public void UpdateBaseAgility() => MemoryMarshal.Write(StatUpdate.Span[124..], ref BaseAgility);

        public void UpdateBaseDexterity() => MemoryMarshal.Write(StatUpdate.Span[128..], ref BaseDexterity);

        public void UpdateBaseWisdom() => MemoryMarshal.Write(StatUpdate.Span[132..], ref BaseWisdom);

        public void UpdateBaseIntelligence() => MemoryMarshal.Write(StatUpdate.Span[136..], ref BaseIntelligence);

        public void UpdateBaseCharisma() => MemoryMarshal.Write(StatUpdate.Span[140..], ref BaseCharisma);
    }
}
