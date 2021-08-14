using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Net;
using System.Text;

using ReturnHome.Opcodes;
using ReturnHome.Server.Network;
using ReturnHome.Utilities;

namespace ReturnHome.Server.Managers
{
    public static class ServerListManager
    {

        private static Encoding unicode = Encoding.Unicode;
        private static ConcurrentDictionary<IPEndPoint, Session> sessionDict = new();
        private static byte[] _serverList;
        public static void AddSession(Session session)
        {
            if (sessionDict.TryAdd(session.MyIPEndPoint, session))
                return;
            
            Console.WriteLine("Error occured and session was not added to ServerList Queue");
        }

        public static void DistributeServerList()
        {
            //Check to see if there is any point to even send a server list

            if (sessionDict.IsEmpty)
                return;

            //Means there is client's on the server list menu, let's send the list
            foreach (var result in sessionDict)
            {
                result.Value.rdpCommIn.sessionQueueMessages(result.Value, _serverList, 0xFC);
            }
        }

        public static void RemoveSession(Session session)
        {
            if (sessionDict.TryRemove(session.MyIPEndPoint, out _))
            {
                Console.WriteLine("Session removed from ServerList");
                return;
            }

            Console.WriteLine("Session not removed");
            return;
        }

        public static void ReadConfig()
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
                        int ServerCount = int.Parse(result);

                        //Standard server listing has 14 bytes to represent data not including length of servername and 3 bytes for server count and opcode
                        int size = 14 * ServerCount + 3;

                        //Get length of server names
                        for (int i = 0; i < ServerCount; i++)
                        {
                            //Length * 2 because unicode
                            size += appSettings[$"Server{i}"].Length * 2;
                        }

                        _serverList = new byte[size];
                        Span<byte> temp = _serverList;

                        int offset = 0;

                        BitConverter.GetBytes((ushort)GameOpcode.GameServers).CopyTo(temp.Slice(offset, 2));

                        offset += 2;
                        ///Add Server count to our preformed packet
                        byte[] bytetemp = Utility_Funcs.Technique(ServerCount);
                        bytetemp.CopyTo(temp.Slice(offset, bytetemp.Length));
                        offset += bytetemp.Length;

                        ///Cycle through servers in Config List
                        for (int i = 0; i < ServerCount; i++)
                        {

                            ///Gets Server Name and Name Length
                            string ServerName = appSettings[$"Server{i}"];
                            BitConverter.GetBytes(ServerName.Length).CopyTo(temp.Slice(offset, 4));
                            offset += 4;

                            unicode.GetBytes(ServerName).CopyTo(temp.Slice(offset, ServerName.Length * 2));
                            offset += ServerName.Length * 2;

                            ///Add Server Recommendation
                            temp[offset] = (byte)(Convert.ToUInt32(appSettings[$"ServerRecommended{i}"]));
                            offset += 1;

                            ///Add Server End Point
                            BitConverter.GetBytes(Convert.ToUInt16(appSettings[$"ServerEndPointID{i}"])).CopyTo(temp.Slice(offset, 2));
                            offset += 2;

                            ///Add Server Port
                            BitConverter.GetBytes(Convert.ToUInt16(appSettings[$"ServerPort{i}"])).CopyTo(temp.Slice(offset, 2));
                            offset += 2;

                            ///Add Server IP 
                            IPAddress tempIP = IPAddress.Parse(appSettings[$"ServerIP{i}"]);
                            byte[] tempbyte = tempIP.GetAddressBytes();

                            //Swap bytes for endianess here on the fly
                            byte a = tempbyte[0];
                            byte b = tempbyte[1];
                            tempbyte[0] = tempbyte[3];
                            tempbyte[1] = tempbyte[2];
                            tempbyte[2] = b;
                            tempbyte[3] = a;

                            tempbyte.CopyTo(temp.Slice(offset, 4));
                            offset += 4;

                            temp[offset] = (byte)(Convert.ToUInt32(appSettings[$"ServerLanguage{i}"]));
                            offset += 1;

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
