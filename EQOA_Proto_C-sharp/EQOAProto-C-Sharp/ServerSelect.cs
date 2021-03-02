using EQLogger;
using SessManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Utility;
using System.Text;
using RdpComm;
using Opcodes;
using System.Linq;
using Sessions;
using EQOAProto;

namespace ServerSelect
{
    class SelectServer
    {
        private static Encoding unicode = Encoding.Unicode;
        private static List<byte> ServerList = new List<byte>();

        public static void GenerateServerSelect(SessionQueueMessages sessionQueueMessages,Session MySession)
        {
            ///Grab the server List
            MySession.messageCreator.MessageWriter(ServerList.ToArray());

            Logger.Info("Collecting Server select list for client");
            sessionQueueMessages.PackMessage(MySession, MessageOpcodeTypes.ShortUnreliableMessage);
        }

        static public void ReadConfig()
        {
            try
            {
                Logger.Info("Connecting to config file");
                ///Connects to our config file
                var appSettings = ConfigurationManager.AppSettings;
                ///Checks for our "ServerCount" config option
                string result = appSettings["Servercount"] ?? "Not Found";

                ///Check if results were found
                if (result == "Not Found")
                {
                    Logger.Err("Config file not found, is it present?");
                }

                else
                {
                    
                    try
                    {
                        Logger.Info($"Acquiring Server Config, total of {result} config's");

                        ///Convert our result to an int
                        int ServerCount = Int32.Parse(result);

                        ServerList.AddRange(BitConverter.GetBytes((ushort)GameOpcode.GameServers));
                        ///Add Server count to our preformed packet
                        ServerList.AddRange(Utility_Funcs.Technique(ServerCount));

                        ///Cycle through servers in Config List
                        for (int i = 0; i < ServerCount; i++)
                        {

                            ///Gets Server Name and Name Length
                            string ServerName = appSettings[$"Server{i}"];
                            int ServerNameLength = ServerName.Length;
                            ///Add Server name length
                            ServerList.AddRange(BitConverter.GetBytes(ServerNameLength));
                            ///Add Server Name in unicode
                            ServerList.AddRange(unicode.GetBytes(ServerName));

                            byte ServerRecommended = (byte)(Convert.ToUInt32(appSettings[$"ServerRecommended{i}"]));
                            ///Add Server Recommendation
                            ServerList.Add(ServerRecommended);

                            ushort ServerEndPointID = Convert.ToUInt16(appSettings[$"ServerEndPointID{i}"]);
                            ///Add Server End Point
                            ServerList.AddRange(BitConverter.GetBytes(ServerEndPointID));

                            ushort ServerPort = Convert.ToUInt16(appSettings[$"ServerPort{i}"]);
                            ///Add Server Port
                            ServerList.AddRange(BitConverter.GetBytes(ServerPort));

                            IPAddress ServerIP = IPAddress.Parse(appSettings[$"ServerIP{i}"]);
                            ///Add Server IP and reverse bytes
                            ServerList.AddRange((ServerIP.GetAddressBytes()).Reverse());

                            byte ServerLanguage = (byte)(Convert.ToUInt32(appSettings[$"ServerLanguage{i}"]));
                            ServerList.Add(ServerLanguage);

                            Logger.Info($"Acquired Server #{i + 1}");
                        }

                        Logger.Info("Done...");
                    }

                    catch (FormatException)
                    {
                        Logger.Err($"Unable to parse '{result}'");
                    }

                }
            }
            catch (ConfigurationErrorsException)
            {
                Logger.Err("Error reading app settings");
            }
        }
    }
}
