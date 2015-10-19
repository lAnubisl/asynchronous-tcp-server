using System;
using System.Linq;
using System.Net.Sockets;
using TcpMessages;

namespace TcpServer
{
    public sealed class Connection
    {
        private readonly Socket socket;
        private readonly byte[] messageSizeInBytes = new byte[8];
        private EventHandler<NewMessageEventArgs> newMessageEvent = delegate { };
        private EventHandler<DisconnectionEventArgs> disconnectEvent = delegate { };
        private readonly IMessageProcessor processor = new MessageProcessor();

        public EventHandler<NewMessageEventArgs> NewMessageEvent
        {
            get { return this.newMessageEvent; }
            set { this.newMessageEvent = value; }
        }

        public EventHandler<DisconnectionEventArgs> DisconnectEvent
        {
            get { return this.disconnectEvent; }
            set { this.disconnectEvent = value; }
        }

        public Connection(Socket socket)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }

            this.socket = socket;
            try
            {
                this.socket.BeginReceive(this.messageSizeInBytes, 0, this.messageSizeInBytes.Length, 0, ReceiveCallback, this);
            }
            catch (SocketException)
            {
                Disconnect();
            }          
        }

        public void Disconnect()
        {
            this.socket.Close();
            DisconnectEvent.Invoke(this, new DisconnectionEventArgs(this));
        }

        public void Send(IMessage message)
        {
            var messageInBytes = processor.PrepareBeforeSend(message);
            var currentMessageSizeInBytes = BitConverter.GetBytes(messageInBytes.Length);
            this.socket.BeginSend(currentMessageSizeInBytes.Concat(messageInBytes).ToArray(), 0, messageInBytes.Length, 0, SendCallback, socket);
        }

        private void SendCallback(IAsyncResult asyncResult)
        {
            this.socket.EndSend(asyncResult);
        }

        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                this.socket.EndReceive(asyncResult);
                var messageSize = BitConverter.ToInt64(messageSizeInBytes, 0);
                var messageInBytes = new byte[messageSize];
                this.socket.Receive(messageInBytes);
                var message = processor.PrepareAfterReceive(messageInBytes);
                NewMessageEvent.Invoke(this, new NewMessageEventArgs(this, message));
                this.socket.BeginReceive(this.messageSizeInBytes, 0, this.messageSizeInBytes.Length, 0, ReceiveCallback, this);
            }
            catch (SocketException)
            {
                Disconnect();
            }
        }
    }
}