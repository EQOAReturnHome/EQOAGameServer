using System;
using System.Collections.Generic;
using ReturnHome.Server.EntityObject.Player;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        //Should this be an array? There is a max capacity to inventory, bank and auctions. 

        public List<Item> Inventory = new();
    
        //Implies if object is visible or not
        public bool Invisible = false;

        public int Level;

        public byte chatMode = 0; //Default to 0, say = 0, Shout = 3 NPC's can technically talk in chat too?

        //Store latest character update directly to character for other characters to pull
        //Doesn't seem right? But we can trigger each session to serialize to this array and distribute to other client's this way
        public Memory<byte> characterUpdate = new Memory<byte> ( new byte[0xC9]);

        /* These are all values for character creation, likely don't need to be attributes of the character object at all*/
        //Default character data should probably be stored in script's to generate from on client's request, saving that to the database
        /*CONSIDER REMOVING IN FAVOR OF ABOVE IN TIME?*/
        public string TestCharName;
        public int StartingClass;
        public int Gender;
        //Note this is for holding the HumType from the client that is an int and base Character has a string HumType
        public int HumTypeNum;
        //Addxxxx attributes of the class are to hold a new characters initial allocated stat points in each category
        public int AddStrength;
        public int AddStamina;
        public int AddAgility;
        public int AddDexterity;
        public int AddWisdom;
        public int AddIntelligence;
        public int AddCharisma;
        //Defaultxxx attributes of the class pulled from the defaultClass table in the DB for new character creation
        public int DefaultStrength;
        public int DefaultStamina;
        public int DefaultAgility;
        public int DefaultDexterity;
        public int DefaultWisdom;
        public int DefaultIntelligence;
        public int DefaultCharisma;

        public bool isPlayer;

        public Entity(bool isplayer)
        {
            isPlayer = isplayer;
        }
    }
}