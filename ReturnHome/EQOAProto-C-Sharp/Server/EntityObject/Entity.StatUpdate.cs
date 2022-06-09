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

        public void UpdateMaxPower() => MemoryMarshal.Write(StatUpdate.Span[40..], ref _maxPower);

        public void UpdateCharisma() => MemoryMarshal.Write(StatUpdate.Span[24..], ref _charisma);
    }
}
