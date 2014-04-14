using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class Connection
    {
        private readonly Socket socket;
        private readonly int bufferSize;
        private byte[] buffer;
        private StringBuilder sb = new StringBuilder();
        public EventHandler<NewMessageEventArgs> newMessageEvent = delegate { };
        public EventHandler<DisconnectionEventArgs> disconnectEvent = delegate { };

        public Connection(Socket socket, int bufferSize)
        {
            this.bufferSize = bufferSize;
            this.buffer = new byte[bufferSize];
            this.socket = socket;
            try
            {
                this.socket.BeginReceive(this.buffer, 0, this.bufferSize, 0, new AsyncCallback(ReceiveCallback), this);
            }
            catch (SocketException)
            {
                Disconnect();
            }          
        }

        private void Disconnect()
        {
            this.socket.Dispose();
            disconnectEvent.Invoke(this, new DisconnectionEventArgs(this));
        }

        public void Send(String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            this.socket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), socket);
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            int bytesSent = this.socket.EndSend(asyncResult);
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            string content = null;
            try
            {
                var readBytesCount = this.socket.EndReceive(asyncResult);
                if (readBytesCount > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(this.buffer, 0, readBytesCount));
                    content = this.sb.ToString();
                    if (content.IndexOf("<EOF>") > -1)
                    {
                        newMessageEvent.Invoke(this, new NewMessageEventArgs(this, content));
                        sb.Clear();
                    }
                }

                this.socket.BeginReceive(this.buffer, 0, this.bufferSize, 0, new AsyncCallback(ReceiveCallback), this);
            }
            catch (SocketException)
            {
                Disconnect();
            }
        }
    }
}