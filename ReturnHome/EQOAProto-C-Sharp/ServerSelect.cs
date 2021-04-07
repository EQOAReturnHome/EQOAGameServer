using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Linq;
using ReturnHome.PacketProcessing;
using ReturnHome.Opcodes;
using ReturnHome.Utilities;
using System.Threading.Channels;

namespace ServerSelect
{
    class SelectServer
    {
        private Encoding unicode = Encoding.Unicode;
        private byte[] ServerList;
        private ConcurrentHashSet<serverListChannel> _serverListSubscribers = new();

        public SelectServer()
        {

        }

        public void GenerateServerSelect()
        {
            foreach (var b in _serverListSubscribers)
            {
                //if false, close the channel
                if (!b.session.serverSelect)
                {
                    b.channel.Complete();
                    _serverListSubscribers.Remove(b);
                    continue;
                }

                b.channel.WriteAsync(ServerList);
            }
        }

        public void ReadConfig()
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

                        ServerList = new byte[size];
                        Span<byte> temp = ServerList;

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

                        foreach (byte b in ServerList)
                        {
                            Console.WriteLine(b);
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

        public ChannelReader<byte[]> getChannel(Session session)
        {
            var channel = Channel.CreateUnbounded<byte[]>();
            _serverListSubscribers.Add(new serverListChannel(channel.Writer, session));
            return channel.Reader;
        }
    }

    public readonly struct serverListChannel
    {
        public readonly ChannelWriter<byte[]> channel;
        public readonly Session session;

        public serverListChannel(ChannelWriter<byte[]> c, Session s)
        {
            channel = c;
            session = s;
        }
    }
}
