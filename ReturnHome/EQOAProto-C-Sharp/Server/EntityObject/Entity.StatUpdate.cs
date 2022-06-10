using System.Runtime.InteropServices;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        public void UpdateStrength() => MemoryMarshal.Write(StatUpdate.Span[0..], ref Strength);

        public void UpdateStamina() => MemoryMarshal.Write(StatUpdate.Span[4..], ref Stamina);

        public void UpdateAgility() => MemoryMarshal.Write(StatUpdate.Span[8..], ref Agility);

        public void UpdateDexterity() => MemoryMarshal.Write(StatUpdate.Span[12..], ref Dexterity);

        public void UpdateWisdom() => MemoryMarshal.Write(StatUpdate.Span[16..], ref Wisdom);

        public void UpdateIntelligence() => MemoryMarshal.Write(StatUpdate.Span[20..], ref Intelligence);

        public void UpdateCharisma() => MemoryMarshal.Write(StatUpdate.Span[24..], ref Charisma);

        public void UpdateCurrentHP()
        {
            MemoryMarshal.Write(StatUpdate.Span[28..], ref _currentHP);
            MemoryMarshal.Write(StatUpdate.Span[144..], ref _currentHP);
        }
        public void UpdateMaxHP() => MemoryMarshal.Write(StatUpdate.Span[32..], ref _hpMax);

        public void UpdateCurrentPower()
        {
            MemoryMarshal.Write(StatUpdate.Span[36..], ref CurrentPower);
            MemoryMarshal.Write(StatUpdate.Span[152..], ref CurrentPower);
        }

        public void UpdateMaxPower() => MemoryMarshal.Write(StatUpdate.Span[40..], ref PowerMax);

        public void UpdateUnknown1()
        {
            MemoryMarshal.Write(StatUpdate.Span[44..], ref _randoValue);
            MemoryMarshal.Write(StatUpdate.Span[160..], ref _randoValue);
        }

        public void UpdateHealthOverTime()
        {
            MemoryMarshal.Write(StatUpdate.Span[48..], ref HealthOverTime);
            MemoryMarshal.Write(StatUpdate.Span[164..], ref HealthOverTime);
        }

        public void UpdatePowerOverTime()
        {
            MemoryMarshal.Write(StatUpdate.Span[52..], ref PowerOverTime);
            MemoryMarshal.Write(StatUpdate.Span[168..], ref PowerOverTime);
        }

        public void UpdateAC() => MemoryMarshal.Write(StatUpdate.Span[56..], ref AC);

        public void UpdatePoisonResist() => MemoryMarshal.Write(StatUpdate.Span[88..], ref PoisonResist);

        public void UpdateDiseaseResist() => MemoryMarshal.Write(StatUpdate.Span[92..], ref DiseaseResist);

        public void UpdateFireResist() => MemoryMarshal.Write(StatUpdate.Span[96..], ref FireResist);

        public void UpdateColdResist() => MemoryMarshal.Write(StatUpdate.Span[100..], ref ColdResist);

        public void UpdateLightningResist() => MemoryMarshal.Write(StatUpdate.Span[104..], ref LightningResist);

        public void UpdateArcaneResist() => MemoryMarshal.Write(StatUpdate.Span[108..], ref ArcaneResist);

        public void UpdateFishing() => MemoryMarshal.Write(StatUpdate.Span[112..], ref ArcaneResist);

        public void UpdateBaseStrength() => MemoryMarshal.Write(StatUpdate.Span[116..], ref BaseStrength);

        public void UpdateBaseStamina() => MemoryMarshal.Write(StatUpdate.Span[120..], ref BaseStamina);

        public void UpdateBaseAgility() => MemoryMarshal.Write(StatUpdate.Span[124..], ref BaseAgility);

        public void UpdateBaseDexterity() => MemoryMarshal.Write(StatUpdate.Span[128..], ref BaseDexterity);

        public void UpdateBaseWisdom() => MemoryMarshal.Write(StatUpdate.Span[132..], ref BaseWisdom);

        public void UpdateBaseIntelligence() => MemoryMarshal.Write(StatUpdate.Span[136..], ref BaseIntelligence);

        public void UpdateBaseCharisma() => MemoryMarshal.Write(StatUpdate.Span[140..], ref BaseCharisma);
    }
}
