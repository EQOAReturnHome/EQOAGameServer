using System;
using System.Collections.Generic;
using System.Linq;
using ReturnHome.Server.EntityObject.Player;
using ReturnHome.Server.Opcodes.Messages.Server;
using ReturnHome.Utilities;

namespace ReturnHome.Server.EntityObject.Spells
{
    public class SpellBook
    {
        private Entity _e;

        //Use this to track player spell hotbar eventually
        private Memory<Spell> _spellHotbar;

        private List<Spell> _spellCasting = new();
        private List<int> _spellCastingIndex = new();
        private List<int> _spellCoolDownIndex = new();
        private List<Spell> _spellCoolDown = new();
        private List<Spell> _spellList;

        public int Count => _spellList.Count;

        //Verify Spells are in order and add them to our spellbook
        //Need to eventually check if any of these spells here are on cool down, too
        public SpellBook(Entity e, List<Spell> SpellList)
        {
            _e = e;

            //Sort list to be in order of spells added to spellbook previously
            //Should database be doing this for us? Probably wise to double check it's correct as this as order is extremely important to utilize indexing against the list.
            _spellList = SpellList.OrderBy(o => o.AddedOrder).ToList();
            if (_e.isPlayer)
                _spellHotbar = new(new Spell[10]);

            //Cycle over the list of spells and place appropriate spells on hotbar?
            foreach(Spell spell in _spellList)
            {
                if (spell.OnHotBar == 1)
                    _spellHotbar.Span[spell.WhereOnHotBar] = spell;
            }
        }

        //Is this right? Maybe we need some kind of Spell Scroll to Spell dictionary? Where we can pull the expected spell against the spell scroll and verify class/race etc is correct?
        public bool AddSpellToBook(Spell spell)
        {
            //Check Level, Race and Class to make sure it's acceptable to have this spell from said scroll
            if (true)
                //AddSpell to Spell book by doing something like spell.AddedOrder = _spellList.Count; _SpellList.Add(spell);
                return true;

            //If it fails, return false indicating we cannot add this spell
            return false;
        }

        //Are we passing AddedOrder here or SpellID? SpellID might be difficult
        public bool UseSpell(int hotBarLocation)
        {
            Spell spell = _spellHotbar.Span[hotBarLocation];
            //When we go to use a spell, perform range checks, LOS, and if target is attackable, if good start casting spell
            if (spell.StartSpellCast(_e))
            {
                _spellCasting.Add(spell);
                _spellCoolDown.Add(spell);
                return true;
            }

            //Utilize the Spell Fizzing out animation here?
            else
                //Fizzle spell out
                return false;
        }

        //This would end up getting called in the server tick somewhere
        public void CheckCoolDownAndCast()
        {
            //Complete casts and send damage/heal numbers
            for (int i = 0; i < _spellCasting.Count; i++)
            {
                //Check if cast is completing
                if (_spellCasting[i].CastCompleted())
                {
                    //Calculate damage and disperse server side
                    _spellCastingIndex.Add(i);
                }
            }

            foreach(int i in _spellCastingIndex)
                _spellCasting.RemoveAt(i);

            _spellCastingIndex.Clear();

            for (int i = 0; i < _spellCoolDown.Count; i++)
            {
                //Checks if spell should be available again
                if (_spellCoolDown[i].CoolDownCompleted())
                {
                    _spellCoolDownIndex.Add(i);
                    //Re-enable ability for client
                    if(_e.isPlayer)
                        ServerSpellCoolDown.SpellCoolDown(((Character)_e).characterSession, _spellCoolDown[i].AddedOrder, 0);
                }
            }

            //Should remove our spells off cooldown as they are able
            foreach(int i in _spellCoolDownIndex)
                _spellCoolDown.RemoveAt(i);

            _spellCoolDownIndex.Clear();
        }

        public void DumpSpellBook(ref BufferWriter writer)
        {
            foreach (Spell spell in _spellList)
                spell.DumpSpell(ref writer);
        }
    }
}
