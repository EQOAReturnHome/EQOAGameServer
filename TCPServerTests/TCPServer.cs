using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using EQOAClientSpace;

namespace EQOATCPServer
{
    //Based on
    //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example
    // State object for reading client data asynchronously  
    public class StateObject
    {
        public int BytesToRead = 0;
        public bool FirstConnect = true;
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Client socket.
        public Socket workSocket = null;

        public EQOAClient EQOAStateClient;
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static ManualResetEvent disconnectDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            IPAddress ipAddress = IPAddress.Any;

            //Account login server port
            ushort Port = 9735;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            //Create Client session
            EQOAClient NewEQOAClient = new EQOAClient((IPEndPoint)handler.RemoteEndPoint, handler);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;

            //Add client session
            state.EQOAStateClient = NewEQOAClient;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(ar);

            Console.WriteLine($"Received {bytesRead} bytes...");

            Console.WriteLine("Bytes received: ");

            if (bytesRead > 0)
            {
                //Verify its within the realm of our game somewhat, not foolproof ofc
                if (bytesRead < 1024)
                {

                    foreach (byte b in state.buffer)
                    {
                        Console.Write(" " + b);
                    }

                    //Add buffer to Client Packet Processing List
                    state.EQOAStateClient.ClientPacket.AddRange(state.buffer[0..bytesRead]);
                    state.EQOAStateClient.CheckClientRequest();

                    //To continue receiving data as it comes in
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

                }
            }

            //Client is disconnecting since we received 0
            else
            {
                //Remove the session
                EQOAClientTracker.RemoveEQOAClient(state.EQOAStateClient);

                // Release the socket.
                handler.Shutdown(SocketShutdown.Both);
                handler.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), handler);

                // Wait for the disconnect to complete.
                disconnectDone.WaitOne();
                if (handler.Connected)
                    Console.WriteLine("We're still connected");
                else
                    Console.WriteLine("We're disconnected");
            }
        }

        //Replies back to Client
        public static void Send(Socket handler, byte[] data)
        {

            // Begin sending the data to the remote device.  
            handler.BeginSend(data, 0, data.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {

                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                //Get our state object
                StateObject state = (StateObject)ar.AsyncState;

                //Error occured, shut the socket down
                //
                //Remove the session
                EQOAClientTracker.RemoveEQOAClient(state.EQOAStateClient);

                // Release the socket.
                handler.Shutdown(SocketShutdown.Both);
                handler.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), handler);

                // Wait for the disconnect to complete.
                disconnectDone.WaitOne();
                if (handler.Connected)
                    Console.WriteLine("We're still connected");
                else
                    Console.WriteLine("We're disconnected");
            }
        }

        private static void DisconnectCallback(IAsyncResult ar)
        {
            // Complete the disconnect request.
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);

            // Signal that the disconnect is complete.
            disconnectDone.Set();
        }

        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }
    }
}