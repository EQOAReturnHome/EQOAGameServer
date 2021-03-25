using System;
using System.Collections.Generic;
using System.Text;
using ReturnHome.Utilities;

namespace ReturnHome.Playercharacter.Actor
{
    public class Hotkey
    {
        public string Direction { get; private set; }
        public string NLabel { get; private set; }
        public string NMessage { get; private set; }
        public string WLabel { get; private set; }
        public string WMessage { get; private set; }
        public string ELabel { get; private set; }
        public string EMessage { get; private set; }
        public string SLabel { get; private set; }
        public string SMessage { get; private set; }
        private List<byte> tempMessage = new List<byte> { };
        private List<byte> ourMessage = new List<byte> { };




        public Hotkey()
        { }

        //Will instantiate a Hotkey object
        public Hotkey(string thisDirection, string thisNLabel, string thisNMessage, string thisWLabel, string thisWMessage, string thisELabel, string thisEMessage, string thisSLabel, string thisSMessage)
        {
            Direction = thisDirection;
            NLabel = thisNLabel;
            NMessage = thisNMessage;
            WLabel = thisWLabel;
            WMessage = thisWMessage;
            ELabel = thisELabel;
            EMessage = thisEMessage;
            SLabel = thisSLabel;
            SMessage = thisSMessage;
        }

        public byte[] PullHotkey()
        {
            //Make sure this is clear before gathering and sending
            ourMessage.Clear();

            //Packdata in list here and return it to calling method
            //Get directions integer value and perform technique
            ourMessage.AddRange(Utility_Funcs.Technique(HotKeyFuncs.OutHoingHotkeyDict[Direction]));

            //North HK
            ourMessage.Add(0);
            ourMessage.AddRange(ConvertHotKey(NMessage, NLabel));

            //West HK
            ourMessage.Add(2);
            ourMessage.AddRange(ConvertHotKey(WMessage, WLabel));

            //East HK
            ourMessage.Add(4);
            ourMessage.AddRange(ConvertHotKey(EMessage, ELabel));

            //South HK
            ourMessage.Add(6);
            ourMessage.AddRange(ConvertHotKey(SMessage, SLabel));

            return ourMessage.ToArray();
        }

        private List<byte> ConvertHotKey(string message, string label)
        {
            //Clear out TempMessage Holder
            tempMessage.Clear();

            //If message
            if(label != null)
            {
                //Add string length, 4 bytes then string as utf-16-le
                tempMessage.AddRange(BitConverter.GetBytes(label.Length));
                tempMessage.AddRange(Encoding.Unicode.GetBytes(label));

                //If label
                if (message != null)
                {
                    //Add string length, 4 bytes then string as utf-16-le
                    tempMessage.AddRange(BitConverter.GetBytes(message.Length));
                    tempMessage.AddRange(Encoding.Unicode.GetBytes(message));
                }

                //no label
                else
                {
                    //Length of 0, 4 bytes
                    tempMessage.AddRange(BitConverter.GetBytes(0));
                }
            }

            //No message
            else
            {
                //Length of 0, 4 bytes
                tempMessage.AddRange(BitConverter.GetBytes(0));

                //If label
                if (message != null)
                {
                    //Add string length, 4 bytes then string as utf-16-le
                    tempMessage.AddRange(BitConverter.GetBytes(message.Length));
                    tempMessage.AddRange(Encoding.Unicode.GetBytes(message));
                }

                //no label
                else
                {
                    //Length of 0, 4 bytes
                    tempMessage.AddRange(BitConverter.GetBytes(0));
                }
            }

            return tempMessage;
        }
    }
}
