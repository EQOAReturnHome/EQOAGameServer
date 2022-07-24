using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ReturnHome.Server.EntityObject;

namespace ReturnHome.Utilities
{
    /*
    public interface ModifierDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
    {
        // Interfaces are not serializable
        // The Item property provides methods to read and edit entries
        // in the Dictionary.
        TValue this[TKey key]
        {
            get;
            set;
        }

        // Returns a collections of the keys in this dictionary.
        ICollection<TKey> Keys
        {
            get;
        }

        // Returns a collections of the values in this dictionary.
        ICollection<TValue> Values
        {
            get;
        }

        // Returns whether this dictionary contains a particular key.
        //
        bool ContainsKey(TKey key);

        // Adds a key-value pair to the dictionary.
        //
        void Add(TKey key, TValue value, Entity e)
        {
            this[key] = value;

            switch(key)
            {
                case StatModifiers.STR:
                    e.CalculatePower();
                    e.UpdateStrength();
                    break;

                case StatModifiers.STA:
                    e.CalculatePower();
                    e.CalculateHP();
                    e.UpdateStamina();
                    break;

                case StatModifiers.AGI:
                    e.CalculatePower();
                    e.UpdateAgility();
                    break;

                case StatModifiers.DEX:
                    e.CalculatePower();
                    e.UpdateDexterity();
                    break;

                case StatModifiers.WIS:
                    e.CalculatePower();
                    e.UpdateWisdom();
                    break;

                case StatModifiers.INT:
                    e.CalculatePower();
                    e.UpdateIntelligence();
                    break;

                case StatModifiers.CHA:
                    e.CalculatePower();
                    e.UpdateCharisma();
                    break;

                case StatModifiers.HPMAX:
                    //Check to make sure currentHP doesn't exceed MaxHP
                    e.UpdateMaxHP();
                    e.ObjectUpdateHPBar();
                    break;
            }

        }

        // Removes a particular key from the dictionary.
        //
        bool Remove(TKey key);

        bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
    }*/
}
