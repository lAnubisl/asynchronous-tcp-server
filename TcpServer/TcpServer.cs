using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class TcpServer
    {
        private readonly AsynchronousSocketListener socketListener;

        private readonly ICollection<Connection> connections;

        public TcpServer(int port, Encoding encoding)
        {
            this.connections = new Collection<Connection>();
            this.socketListener = new AsynchronousSocketListener(port, encoding);
            this.socketListener.newConnectionEvent += NewConnection;
            this.socketListener.StartListening();
        }

        private void NewConnection(object sender, NewConnectionEventArgs e)
        {
            Console.WriteLine("Connected");
            connections.Add(e.Connection);
            e.Connection.newMessageEvent += NewMessage;
            e.Connection.disconnectEvent += Disconnect;
        }

        private void Disconnect(object sender, DisconnectionEventArgs e)
        {
            Console.WriteLine("Disconnected");
            connections.Remove(e.Connection);
        }

        private void NewMessage(object sender, NewMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
