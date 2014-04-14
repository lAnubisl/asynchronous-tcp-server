using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
