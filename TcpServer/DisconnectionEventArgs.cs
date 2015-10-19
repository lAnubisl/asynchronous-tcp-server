using System;

namespace TcpServer
{
    public class DisconnectionEventArgs : EventArgs
    {
        private readonly Connection connection;

        public DisconnectionEventArgs(Connection connection)
        {
            this.connection = connection;
        }

        public Connection Connection
        {
            get { return this.connection; }
        }
    }
}
