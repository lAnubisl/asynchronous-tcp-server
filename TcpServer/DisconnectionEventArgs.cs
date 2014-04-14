using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class DisconnectionEventArgs : EventArgs
    {
        private Connection connection;

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
