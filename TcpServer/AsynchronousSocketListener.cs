using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpServer
{
    public class AsynchronousSocketListener : IDisposable
    {
        private readonly int port;
        private static readonly ManualResetEvent allDone = new ManualResetEvent(false);
        private readonly Socket acceptSocket;
        private EventHandler<NewConnectionEventArgs> newConnectionEvent = delegate { };

        public EventHandler<NewConnectionEventArgs> NewConnectionEvent
        {
            get { return this.newConnectionEvent; }
            set { this.newConnectionEvent = value; }
        }

        public AsynchronousSocketListener(int port)
        {
            this.port = port;
            this.acceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, this.port);
            acceptSocket.Bind(localEndPoint);
            acceptSocket.Listen(100);
            while (true)
            {
                allDone.Reset();
                acceptSocket.BeginAccept(AcceptCallback, null);
                allDone.WaitOne();
            }
        }

        public void AcceptCallback(IAsyncResult asyncResult)
        {
            allDone.Set();
            var socket = this.acceptSocket.EndAccept(asyncResult);
            var connection = new Connection(socket);
            NewConnectionEvent.Invoke(this, new NewConnectionEventArgs(connection));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                acceptSocket.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}