using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EQOAUtilities
{
	static public class Utilities
	{
		static public int ReturnInt(List<byte> PacketList)
		{
			//Get our integer
			int Data = BitConverter.ToInt32(PacketList.Take(4).Reverse().ToArray());

			//Remove the bytes from the List
			PacketList.RemoveRange(0, 4);

			return Data;
		}

		static public string ReturnString(List<byte> PacketList, int Count)
        {
			//Get our string
			string TheString = Encoding.UTF8.GetString(PacketList.Take(Count).ToArray());

			//Remove the bytes from the List
			PacketList.RemoveRange(0, Count);

			return TheString;
        }

		static public byte[] ReturnByteArray(byte ThisObject)
        {
			byte[] TheseBytes = BitConverter.GetBytes(ThisObject);
			Array.Reverse(TheseBytes, 0, TheseBytes.Length);
			return TheseBytes;
		}

		static public byte[] ReturnByteArray(short ThisObject)
		{
			byte[] TheseBytes = BitConverter.GetBytes(ThisObject);
			Array.Reverse(TheseBytes, 0, TheseBytes.Length);
			return TheseBytes;
		}

		static public byte[] ReturnByteArray(int ThisObject)
		{
			byte[] TheseBytes = BitConverter.GetBytes(ThisObject);
			Array.Reverse(TheseBytes, 0, TheseBytes.Length);
			return TheseBytes;
		}
	}
}
