using System;
using System.Text;

using ReturnHome.Utilities;
using ReturnHome.Server.EntityObject.Player;
using System.Numerics;
using System.Buffers.Binary;

namespace ReturnHome.Server.EntityObject
{
    public partial class Entity
    {
        private static float _speedAdjust = 6.25f;

        public void ObjectUpdateObjectID()
        {
            BitConverter.GetBytes(ObjectID).CopyTo(ObjectUpdate.Slice(0, 4));
        }

        public void ObjectUpdateEntity(byte temp = 0x82)
        {
            new byte[] { temp }.CopyTo(ObjectUpdate.Slice(4, 1));
        }

        public void ObjectUpdatePosition()
        {
            int offset = 5;
            Span<byte> temp = ObjectUpdate.Span;
            temp.Write3Bytes((uint)(Position.X * 128.0f), ref offset);
            temp.Write3Bytes((uint)(Position.Y * 128.0f), ref offset);
            temp.Write3Bytes((uint)(Position.Z * 128.0f), ref offset);
        }

        public void ObjectUpdateFacing()
        {
            ObjectUpdate.Span[14] = Facing;
        }

        public void ObjectUpdateFirstPerson()
        {
            ObjectUpdate.Span[15] = FirstPerson;
        }

        public void ObjectUpdateWorld()
        {
            ObjectUpdate.Span[16] = (byte)World;
        }

        public void ObjectUpdateKillTime()
        {
            BitConverter.GetBytes(KillTime).CopyTo(ObjectUpdate.Slice(17, 8));
        }

        public void ObjectUpdateHPFlag()
        {
            ObjectUpdate.Span[25] = HPFlag ? (byte)1 : (byte)0;
        }

        public void ObjectUpdateHPBar()
        {
            if (HPMax == 0 || CurrentHP == 0)
                return;
            ObjectUpdate.Span[26] = (byte)((255 * CurrentHP) / HPMax);
        }

        public void ObjectUpdateModelID()
        {
            BitConverter.GetBytes(ModelID).CopyTo(ObjectUpdate.Slice(27, 4));
        }

        public void ObjectUpdateModelIDUpgraded()
        {
            BitConverter.GetBytes(ModelID).CopyTo(ObjectUpdate.Slice(31, 4));
        }

        public void ObjectUpdateModelSize()
        {
            BitConverter.GetBytes(ModelSize).CopyTo(ObjectUpdate.Slice(35, 4));
        }

        public void ObjectUpdateVelocityX()
        {
            sbyte svx = (sbyte)Math.Round(VelocityX * _speedAdjust);
            if (svx > 127) { Console.WriteLine("WARNING: svx=" + svx); svx = 127; }
            if (svx < -128) { Console.WriteLine("WARNING: svx=" + svx); svx = -128; }
            new byte[] { (byte)svx }.CopyTo(ObjectUpdate.Slice(39, 1));
        }

        public void ObjectUpdateVelocityY()
        {
            /*Don't need to adjust Y, for now
            sbyte svx = (sbyte)Math.Round(VelocityX * _speedAdjust);
            if (svx > 127) { Console.WriteLine("WARNING: svx=" + svx); svx = 127; }
            if (svx < -128) { Console.WriteLine("WARNING: svx=" + svx); svx = -128; }
            new byte[] { (byte)svx }.CopyTo(ObjectUpdate.Slice(39, 1));
            */
        }

        public void ObjectUpdateVelocityZ()
        {
            sbyte svz = (sbyte)Math.Round(VelocityZ * _speedAdjust);
            if (svz > 127) { Console.WriteLine("WARNING: svx=" + svz); svz = 127; }
            if (svz < -128) { Console.WriteLine("WARNING: svx=" + svz); svz = -128; }
            new byte[] { (byte)svz }.CopyTo(ObjectUpdate.Slice(41, 1));
        }

        public void ObjectUpdateEastWest()
        {
            ObjectUpdate.Span[44] = EastToWest;
        }

        public void ObjectUpdateLateralMovement()
        {
            ObjectUpdate.Span[45] = LateralMovement;
        }

        public void ObjectUpdateNorthSouth()
        {
            ObjectUpdate.Span[46] = NorthToSouth;
        }
        public void ObjectUpdateTurning()
        {
            ObjectUpdate.Span[47] = Turning;
        }

        public void ObjectUpdateSpinDown()
        {
            ObjectUpdate.Span[48] = SpinDown;
        }

        public void ObjectUpdateCoordY()
        {
            BitConverter.GetBytes(y).CopyTo(ObjectUpdate.Slice(49, 4));
        }

        public void ObjectUpdateFacingF()
        {
            BitConverter.GetBytes(FacingF).CopyTo(ObjectUpdate.Slice(53, 4));
        }

        public void ObjectUpdateAnimation()
        {
            ObjectUpdate.Span[57] = Animation;
        }

        public void ObjectUpdateTarget()
        {
            BitConverter.GetBytes(Target).CopyTo(ObjectUpdate.Slice(58, 4));
        }

        public void ObjectUpdateUnknown()
        {
            BitConverter.GetBytes((ushort)0x0105).CopyTo(ObjectUpdate.Slice(62, 2));
        }

        public void ObjectUpdatePrimary()
        {
            BitConverter.GetBytes(Primary).CopyTo(ObjectUpdate.Slice(66, 4));
        }

        public void ObjectUpdateSecondary()
        {
            BitConverter.GetBytes(Secondary).CopyTo(ObjectUpdate.Slice(70, 4));
        }

        public void ObjectUpdateShield()
        {
            BitConverter.GetBytes(Shield).CopyTo(ObjectUpdate.Slice(74, 4));
        }

        public void ObjectUpdatePrimaryUpgraded()
        {
            BitConverter.GetBytes(Primary).CopyTo(ObjectUpdate.Slice(78, 4));
        }

        public void ObjectUpdateSecondaryUpgraded()
        {
            BitConverter.GetBytes(Secondary).CopyTo(ObjectUpdate.Slice(82, 4));
        }

        public void ObjectUpdateShieldUpgraded()
        {
            BitConverter.GetBytes(Shield).CopyTo(ObjectUpdate.Slice(86, 4));
        }

        public void ObjectUpdateChest()
        {
            ObjectUpdate.Span[92] = Chest;
        }

        public void ObjectUpdateBracer()
        {
            ObjectUpdate.Span[93] = Bracer;
        }

        public void ObjectUpdateGloves()
        {
            ObjectUpdate.Span[94] = Gloves;
        }

        public void ObjectUpdateLegs()
        {
            ObjectUpdate.Span[95] = Legs;
        }

        public void ObjectUpdateBoots()
        {
            ObjectUpdate.Span[96] = Boots;
        }

        public void ObjectUpdateHelm()
        {
            ObjectUpdate.Span[97] = Helm;
        }

        public void ObjectUpdateVanillaColors()
        {
            byte[] temp = new byte[] { 0xFF, 0xFF }; 
            for(int i = 0; i < 6; i++)
            {
                temp.CopyTo(ObjectUpdate.Slice(98 + (i * 2), 2));
            }
        }

        public void ObjectUpdateChestColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(110, 4).Span, ChestColor);
        }

        public void ObjectUpdateBracerColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(115, 4).Span, BracerColor);
        }

        public void ObjectUpdateGloveColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(120, 4).Span, GloveColor);
        }

        public void ObjectUpdateLegColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(125, 4).Span, LegColor);
        }

        public void ObjectUpdateBootsColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(130, 4).Span, BootsColor);
        }

        public void ObjectUpdateHelmColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(135, 4).Span, HelmColor);
        }

        public void ObjectUpdateRobeColor()
        {
            BinaryPrimitives.WriteUInt32BigEndian(ObjectUpdate.Slice(140, 4).Span, RobeColor);
        }

        public void ObjectUpdateHairColor()
        {
            ObjectUpdate.Span[144] = (byte)HairColor;
        }

        public void ObjectUpdateHairLength()
        {
            ObjectUpdate.Span[145] = (byte)HairLength;
        }

        public void ObjectUpdateHairStyle()
        {
            ObjectUpdate.Span[146] = (byte)HairStyle;
        }

        public void ObjectUpdateFaceOption()
        {
            ObjectUpdate.Span[147] = (byte)FaceOption;
        }

        public void ObjectUpdateRobe()
        {
            ObjectUpdate.Span[148] = (byte)Robe;
        }

        public void ObjectUpdateName()
        {
            Encoding.UTF8.GetBytes(CharName).CopyTo(ObjectUpdate.Slice(154, CharName.Length));
        }

        public void ObjectUpdateLevel()
        {
            ObjectUpdate.Span[178] = (byte)Level;
        }

        public void ObjectUpdateMovement()
        {
            ObjectUpdate.Span[179] = Movement;
        }

        public void ObjectUpdateNameColor(byte color = 255)
        {
            if(isPlayer)
                ObjectUpdate.Span[180] = color == 255 ? (byte)2 : color;
            else
                ObjectUpdate.Span[180] = color == 255 ? (byte)0 : color;
        }

        public void ObjectUpdateNamePlate(byte color = 255)
        {
            ObjectUpdate.Span[181] = color == 255 ? (byte)0 : color;
        }

        public void ObjectUpdateRace()
        {
            ObjectUpdate.Span[182] = (byte)Race;
        }

        public void ObjectUpdateClass()
        {
            ObjectUpdate.Span[183] = (byte)Class;
        }

        public void ObjectUpdateNPCType()
        {
            BitConverter.GetBytes(_npcType).CopyTo(ObjectUpdate.Slice(184, 2));
        }

        public void ObjectUpdatePattern()
        {
            BitConverter.GetBytes(0xFFFFFFFF).CopyTo(ObjectUpdate.Slice(186, 4));
        }

        public void ObjectUpdateStatus(byte status = 0)
        {
            ObjectUpdate.Span[192] = status;
        }

        public void ObjectUpdateEnd()
        {
            Encoding.UTF8.GetBytes("trsq").CopyTo(ObjectUpdate.Slice(196, 4));
        }

        public Memory<byte> SerializeObjectUpdate(Character character)
        {
            //Vector3 tempPosition = getPosition();
            int offset = 0;
            Memory<byte> characterSerialize = new Memory<byte>(new byte[0xC9]);
            Span<byte> temp = characterSerialize.Span;

            //Seems to indicate if channel is changing
            temp.Write((byte)1, ref offset);

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
