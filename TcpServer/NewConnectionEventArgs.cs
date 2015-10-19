using System;

namespace TcpServer
{
    public class NewConnectionEventArgs : EventArgs
    {
        private readonly Connection connection;

        public Connection Connection
        {
            get { return this.connection; }
        }

        public NewConnectionEventArgs(Connection connection) : base()
        {
            this.connection = connection;
        }
    }
}
