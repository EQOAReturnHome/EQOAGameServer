using System;
using System.IO;
using System.Text;
using ReturnHome.PacketProcessing;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Entity.Actor
{
    public static class ObjectUpdate
    {
        //This will be our class for storing our C9 object updates that will go out to clients
        public static byte[] GatherObjectUpdate(Character myCharacter, uint objectID)
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                ThreeByteInt Xcoord = (int)(myCharacter.XCoord * 128.0f);
                ThreeByteInt Ycoord = (int)(myCharacter.YCoord * 128.0f);
                ThreeByteInt Zcoord = (int)(myCharacter.ZCoord * 128.0f);

                //Active or not
                writer.Write((byte)1);
                writer.Write(objectID);
                //not sure
                writer.Write((byte)0x82);
                //XCoord
                writer.Write(Xcoord.byte2);
                writer.Write(Xcoord.byte1);
                writer.Write(Xcoord.byte0);
                //YCoord
                writer.Write(Ycoord.byte2);
                writer.Write(Ycoord.byte1);
                writer.Write(Ycoord.byte0);
                //ZCoord
                writer.Write(Zcoord.byte2);
                writer.Write(Zcoord.byte1);
                writer.Write(Zcoord.byte0);
                writer.Write((byte)(myCharacter.Facing / 0.024543693));
                writer.Write(myCharacter.FirstPerson);
                writer.Write((byte)myCharacter.World);
                //Kill time
                writer.Write((long)0);
                //Hp Flag?
                writer.Write((byte)1);
                writer.Write((byte)0xFF); //Formula to calculate this byte I believe (((float)myCharacter.MaxHP / myCharacter.CurrentHP) * 255));
                writer.Write(myCharacter.ModelID);
                //Upgraded model ID?
                writer.Write(0);
                writer.Write(myCharacter.ModelSize);
                //XOffset
                writer.Write((short)0);
                //ZOffset
                writer.Write((short)0);
                //unk
                writer.Write((byte)0);
                //East/West
                writer.Write((byte)0);
                //Direction
                writer.Write((byte)0);
                //North/South
                writer.Write((byte)0);
                writer.Write(myCharacter.Turning);
                //Down/Up? Camera stuff?
                writer.Write((byte)0);
                writer.Write(myCharacter.YCoord);
                writer.Write(myCharacter.Facing);
                writer.Write((byte)0); //Has to do with "animations", may need to explore this more  myCharacter.Animation);
                writer.Write(myCharacter.Target);
                //Another unk
                writer.Write((short)0x0105);
                //Additional unk
                writer.Write((short)0);
                writer.Write(myCharacter.Primary);
                writer.Write(myCharacter.Secondary);
                writer.Write(myCharacter.Shield);
                //upgraded weapon graphics? Primary/Secondary/Shield
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                //unk again
                writer.Write((short)0);
                writer.Write(myCharacter.Chest);
                writer.Write(myCharacter.Bracer);
                writer.Write(myCharacter.Gloves);
                writer.Write(myCharacter.Legs);
                writer.Write(myCharacter.Boots);
                writer.Write(myCharacter.Helm);
                //vanilla stuff related to above, believe it's colors?
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write((ushort)0xFFFF);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.ChestColor));
                writer.Write((byte)0);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.BracerColor));
                writer.Write((byte)0);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.GlovesColor));
                writer.Write((byte)0);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.LegsColor));
                writer.Write((byte)0);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.BootsColor));
                writer.Write((byte)0);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.HelmColor));
                writer.Write((byte)0);
                writer.Write(ByteSwaps.SwapBytes(myCharacter.RobeColor));
                writer.Write((byte)myCharacter.HairColor);
                writer.Write((byte)myCharacter.HairLength);
                writer.Write((byte)myCharacter.HairStyle);
                writer.Write((byte)myCharacter.FaceOption);
                writer.Write((byte)myCharacter.Robe);
                //unk
                writer.Write(5);
                //unk
                writer.Write((byte)0);
                writer.Write(Encoding.UTF8.GetBytes(myCharacter.CharName));
                for (int i = 0;  i < (24 - myCharacter.CharName.Length); i++)
                {
                    writer.Write((byte)0);
                }
                writer.Write((byte)myCharacter.Level);
                //movement
                writer.Write((byte)1);
                //conflag
                writer.Write((byte)2);
                //nameplate
                writer.Write((byte)0);
                writer.Write((byte)myCharacter.Race);
                writer.Write((byte)myCharacter.TClass);
                //npctype?
                writer.Write((short)0);
                writer.Write(0xFFFFFFFF);
                //unk
                writer.Write((byte)0);
                //unk
                writer.Write((byte)0x1F);
                //If target has invis/poison?
                writer.Write((byte)0);
                //unk
                writer.Write((byte)0);
                //unk
                writer.Write((byte)1);
                //unk
                writer.Write((byte)0);
                writer.Write(Encoding.UTF8.GetBytes("tsrq"));

                return stream.ToArray();
            }
        }
    }
}
