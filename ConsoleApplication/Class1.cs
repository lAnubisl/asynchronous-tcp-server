using System;
using TcpServer;

namespace ConsoleApplication
{
    public class Class1
    {
        private static AsynchronousTcpServer server;

        public static void Main()
        {
            server = new AsynchronousTcpServer(9100);
            Console.ReadLine();
        }
    }
}
