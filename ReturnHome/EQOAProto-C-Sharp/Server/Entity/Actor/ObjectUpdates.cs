using System;
using System.Text;

using ReturnHome.Utilities;

namespace ReturnHome.Server.Entity.Actor
{
    public static class ObjectUpdate
    {
        //This will be our class for storing our C9 object updates that will go out to clients
        public static Memory<byte> SerializeClientUpdate(Character character)
        {
            int offset = 0;
            Memory<byte> characterSerialize = new Memory<byte>(new byte[0xC9]);
            Span<byte> temp = characterSerialize.Span;

            //Seems to indicate if channel is changing
            temp.Write((byte)1, ref offset);

            //Ties this update channel and client together, based on the SessionID
            temp.Write(character.characterSession.SessionID, ref offset);

            //Unsure what this really does
            temp.Write((byte)0x82, ref offset);

            //Writes 3byte x coordinate
            temp.Write3Bytes((uint)(character.XCoord * 128.0f), ref offset);

            //Writes 3byte y coordinate
            temp.Write3Bytes((uint)(character.YCoord * 128.0f), ref offset);

            //Writes 3byte z coordinate
            temp.Write3Bytes((uint)(character.ZCoord * 128.0f), ref offset);

            //Facing
            temp.Write((byte)(character.Facing / 0.024543693), ref offset);
            temp.Write(character.FirstPerson, ref offset);
            temp.Write((byte)character.World, ref offset);
            //Kill time
            temp.Write((long)0, ref offset);
            //Hp Flag?
            temp.Write((byte)1, ref offset);
            temp.Write((byte)0xFF, ref offset); //Formula to calculate this byte I believe (((float)character.MaxHP / character.CurrentHP) * 255), ref offset);
            temp.Write(character.ModelID, ref offset);
            //Upgraded model ID?
            temp.Write(0, ref offset);
            temp.Write(character.ModelSize, ref offset);
            //XOffset
            temp.Write((short)0, ref offset);
            //ZOffset
            temp.Write((short)0, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            //East/West
            temp.Write((byte)0, ref offset);
            //Direction
            temp.Write((byte)0, ref offset);
            //North/South
            temp.Write((byte)0, ref offset);
            temp.Write(character.Turning, ref offset);
            //Down/Up? Camera stuff?
            temp.Write((byte)0, ref offset);
            temp.Write(character.YCoord, ref offset);
            temp.Write(character.Facing, ref offset);
            temp.Write((byte)0, ref offset); //Has to do with "animations", may need to explore this more  character.Animation, ref offset);
            temp.Write(character.Target, ref offset);
            //Another unk
            temp.Write((short)0x0105, ref offset);
            //Additional unk
            temp.Write((short)0, ref offset);
            temp.Write(character.Primary, ref offset);
            temp.Write(character.Secondary, ref offset);
            temp.Write(character.Shield, ref offset);
            //upgraded weapon graphics? Primary/Secondary/Shield
            temp.Write(0, ref offset);
            temp.Write(0, ref offset);
            temp.Write(0, ref offset);
            //unk again
            temp.Write((short)0, ref offset);
            temp.Write(character.Chest, ref offset);
            temp.Write(character.Bracer, ref offset);
            temp.Write(character.Gloves, ref offset);
            temp.Write(character.Legs, ref offset);
            temp.Write(character.Boots, ref offset);
            temp.Write(character.Helm, ref offset);
            //vanilla stuff related to above, believe it's colors?
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write(character.ChestColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.Write(character.BracerColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.Write(character.GlovesColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.Write(character.LegsColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.Write(character.BootsColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.Write(character.HelmColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.Write(character.RobeColor, ref offset);
            temp.Write((byte)character.HairColor, ref offset);
            temp.Write((byte)character.HairLength, ref offset);
            temp.Write((byte)character.HairStyle, ref offset);
            temp.Write((byte)character.FaceOption, ref offset);
            temp.Write((byte)character.Robe, ref offset);
            //unk
            temp.Write(5, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            temp.Write(Encoding.UTF8.GetBytes(character.CharName), ref offset);
            temp.Write(new byte[24 - character.CharName.Length], ref offset);

            temp.Write((byte)character.Level, ref offset);
            //movement
            temp.Write((byte)1, ref offset);
            //conflag
            temp.Write((byte)2, ref offset);
            //nameplate
            temp.Write((byte)0, ref offset);
            temp.Write((byte)character.Race, ref offset);
            temp.Write((byte)character.TClass, ref offset);
            //npctype?
            temp.Write((short)0, ref offset);
            temp.Write(0xFFFFFFFF, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            //unk
            temp.Write((byte)0x1F, ref offset);
            //If target has invis/poison?
            temp.Write((byte)0, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            //unk
            temp.Write((byte)1, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            temp.Write(Encoding.UTF8.GetBytes("tsrq"), ref offset);

            Console.WriteLine(offset);
            return characterSerialize;
        }
    }
}
