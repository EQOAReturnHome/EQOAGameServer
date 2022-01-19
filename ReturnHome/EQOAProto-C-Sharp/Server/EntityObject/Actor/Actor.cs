using System;

namespace ReturnHome.Server.EntityObject.Actor
{
  public class Actor : Entity
  {
        public long killtime = 0;

        public Actor() : base(false)
        {

        }

        public Actor(string charName, float xCoord, float yCoord, float zCoord, int facing, int world, int modelid, float size,
            int primary, int secondary, int shield, int hair_color, int hair_length, int hair_style, int level, int torso, int forearms,
            int gloves, int legs, int feet, int head) : base(false)
        {
            CharName = charName;
            x = xCoord;
            y = yCoord;
            z = zCoord;
            Facing = (byte)facing;
            World = world;
            ModelID = modelid;
            ModelSize = size;
            Primary = primary;
            Secondary = secondary;
            Shield = shield;
            HairColor = hair_color;
            HairLength = hair_length;
            HairStyle = hair_style;
            Level = level;
            Chest = (byte)torso;
            Bracer = (byte)forearms;
            Gloves = (byte)gloves;
            Legs = (byte)legs;
            Boots = (byte)feet;
            Helm = (byte)head;
            HPFlag = true;
            CurrentHP = 300;
            HPMax = 500;
            Target = 0xFFFFFFFF;
            NPCType = 0x0082;
        }
  }
}
