using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class NewMessageEventArgs : EventArgs
    {
        private readonly string message;

        public string Message
        {
            get { return this.message; }
        }

        public NewMessageEventArgs(Connection stateObject, string message) : base()
        {
            this.message = message;
        }
    }
}
