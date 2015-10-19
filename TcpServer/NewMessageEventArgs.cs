using System;
using TcpMessages;

namespace TcpServer
{
    public class NewMessageEventArgs : EventArgs
    {
        private readonly IMessage message;
        private readonly Connection connection;

        public IMessage Message
        {
            get { return this.message; }
        }

        public Connection Connection
        {
            get { return this.connection; }
        }

        public NewMessageEventArgs(Connection connection, IMessage message)
        {
            this.connection = connection;
            this.message = message;
        }
    }
}
