using System;
using System.Text;

using ReturnHome.Utilities;
using ReturnHome.Server.EntityObject.Player;
using System.Numerics;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        private static float _speedAdjust = 6.25f;
        //This will be our class for storing our C9 object updates that will go out to clients
        public Memory<byte> SerializeObjectUpdate(Character character)
        {
            //Vector3 tempPosition = getPosition();
            int offset = 0;
            Memory<byte> characterSerialize = new Memory<byte>(new byte[0xC9]);
            Span<byte> temp = characterSerialize.Span;

            //Seems to indicate if channel is changing
            temp.Write((byte)1, ref offset);

            //Ties this update channel and client together, based on the SessionID
            temp.Write(ObjectID, ref offset);

            //Unsure what this really does
            temp.Write((byte)0x82, ref offset);

            //Writes 3byte x coordinate
            temp.Write3Bytes((uint)(position.X * 128.0f), ref offset);

            //Writes 3byte y coordinate
            temp.Write3Bytes((uint)(position.Y * 128.0f), ref offset);

            //Writes 3byte z coordinate
            temp.Write3Bytes((uint)(position.Z * 128.0f), ref offset);
            
            //Facing
            temp.Write(Facing, ref offset);
            temp.Write(character.FirstPerson, ref offset);
            temp.Write((byte)World, ref offset);
            //Kill time
            temp.Write((long)0, ref offset);
            //Hp Flag?
            temp.Write((byte)1, ref offset);
            temp.Write((byte) (255.0f*(CurrentHP/HPMax)), ref offset); //Formula to calculate this byte I believe (((float)character.MaxHP / character.CurrentHP) * 255), ref offset);
            temp.Write(ModelID, ref offset);
            //Upgraded model ID?
            temp.Write(0, ref offset);
            temp.Write(ModelSize, ref offset);
            //XOffset
            temp.Write((sbyte)Math.Round(VelocityX * _speedAdjust), ref offset);
            temp.Write((sbyte)Math.Round(VelocityY * _speedAdjust), ref offset);
            //ZOffset
            temp.Write((sbyte)0 /*Math.Round(VelocityZ * _speedAdjust)*/, ref offset);
            temp.Write((byte)0, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            //East/West
            temp.Write(EastToWest, ref offset);
            //Direction
            temp.Write(LateralMovement, ref offset);
            //North/South
            temp.Write(NorthToSouth, ref offset);
            temp.Write(Turning, ref offset);
            //Down/Up? Camera stuff?
            temp.Write(SpinDown, ref offset);
            temp.Write(0/*y*/, ref offset);
            temp.Write(0/*FacingF*/, ref offset);
            temp.Write(Animation, ref offset); //Has to do with "animations", may need to explore this more  character.Animation, ref offset);
            temp.Write(Target, ref offset);
            //Another unk
            temp.Write((short)0x0105, ref offset);
            //Additional unk
            temp.Write((short)0, ref offset);
            temp.Write(Primary, ref offset);
            temp.Write(Secondary, ref offset);
            temp.Write(Shield, ref offset);
            //upgraded weapon graphics? Primary/Secondary/Shield
            temp.Write(0, ref offset);
            temp.Write(0, ref offset);
            temp.Write(0, ref offset);
            //unk again
            temp.Write((short)0, ref offset);
            temp.Write(Chest, ref offset);
            temp.Write(Bracer, ref offset);
            temp.Write(Gloves, ref offset);
            temp.Write(Legs, ref offset);
            temp.Write(Boots, ref offset);
            temp.Write(Helm, ref offset);
            //vanilla stuff related to above, believe it's colors?
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.Write((ushort)0xFFFF, ref offset);
            temp.WriteBE(ChestColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.WriteBE(BracerColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.WriteBE(GlovesColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.WriteBE(LegsColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.WriteBE(BootsColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.WriteBE(HelmColor, ref offset);
            temp.Write((byte)0, ref offset);
            temp.WriteBE(RobeColor, ref offset);
            temp.Write((byte)HairColor, ref offset);
            temp.Write((byte)HairLength, ref offset);
            temp.Write((byte)HairStyle, ref offset);
            temp.Write((byte)FaceOption, ref offset);
            temp.Write((byte)Robe, ref offset);
            //unk
            temp.Write(5, ref offset);
            //unk
            temp.Write((byte)0, ref offset);
            temp.Write(Encoding.UTF8.GetBytes(CharName), ref offset);
            temp.Write(new byte[24 - CharName.Length], ref offset);

            temp.Write((byte)character.Level, ref offset);
            //movement
            temp.Write(Movement, ref offset);
            //conflag
            temp.Write((byte)2, ref offset);
            //nameplate
            temp.Write((byte)0, ref offset);
            temp.Write((byte)character.Race, ref offset);
            temp.Write((byte)character.Class, ref offset);
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

            return characterSerialize;
        }
    }
}
