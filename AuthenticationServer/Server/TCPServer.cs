using System;
using System.IO;
using System.IO.Pipelines;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AuthServer.Server
{
    //Based on
    //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example
    // State object for reading client data asynchronously  

    public class AsynchronousSocketListener
    {
        private Stream _stream;
        private Pipe _readPipe, _writePipe;

        public PipeReader Input => _readPipe.Reader;
        public PipeWriter Output => _writePipe.Writer;
        private readonly EQOAClientTracker _eqoaClientTracker;
        private readonly Socket _listener = new(SocketType.Stream, ProtocolType.Tcp);

        public AsynchronousSocketListener(EQOAClientTracker eqoaclientTracker)
        {
            _eqoaClientTracker = eqoaclientTracker;
        }

        public async Task StartListener()
        {
            _listener.Bind(new IPEndPoint(IPAddress.Any, 9735));

            _listener.Listen(100);

            while (true)
            {
                Socket socket = await _listener.AcceptAsync();
                _ = ProcessAsync(socket);
            }
        }

        private async Task ProcessAsync(Socket socket)
        {
            _readPipe = new();
            _writePipe = new();

            // Create a PipeReader over the network stream
            _stream = new NetworkStream(socket);

            //Create Client session
            EQOAClient theClient = new((IPEndPoint)socket.RemoteEndPoint, _stream, socket, Input, Output);
            Task.Run(theClient.CheckClientRequest);
            Task.Run(ProcessSend);

            //Add Session to Tracking
            _eqoaClientTracker.AddEQOAClient(theClient);

            while (true)
            {
                // note we'll usually get *much* more than we ask for
                Memory<byte> buffer = _readPipe.Writer.GetMemory(1);
                int bytes = await _stream.ReadAsync(buffer);
                _readPipe.Writer.Advance(bytes);

                if (bytes == 0)
                    break;

                var flush = await _readPipe.Writer.FlushAsync();

                if (flush.IsCompleted || flush.IsCanceled)
                    break;
            }

            // Mark the PipeReader as complete.
            await Input.CompleteAsync();

            Console.WriteLine($"[{socket.RemoteEndPoint}]: disconnected");
        }

        private async Task ProcessSend()
        {
            while(true)
            {
                ReadResult read = await _writePipe.Reader.ReadAsync();
                await _stream.WriteAsync(read.Buffer.First);
                _writePipe.Reader.AdvanceTo(read.Buffer.End);
            }
        }
    }
}
