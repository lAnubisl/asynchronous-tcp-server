using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpServer;

public class AsynchronousSocketListener
{
    private readonly int port;
    private readonly Encoding encoding;
    public static ManualResetEvent allDone = new ManualResetEvent(false);
    private readonly Socket acceptSocket;

    public EventHandler<NewConnectionEventArgs> newConnectionEvent = delegate { };

    public AsynchronousSocketListener(int port, Encoding encoding)
    {
        this.port = port;
        this.encoding = encoding;
        this.acceptSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void StartListening()
    {
        try
        {
            IPHostEntry ipHostInfo = Dns.Resolve("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, this.port);
            acceptSocket.Bind(localEndPoint);
            acceptSocket.Listen(100);
            while (true)
            {
                allDone.Reset();    
                acceptSocket.BeginAccept(new AsyncCallback(AcceptCallback), acceptSocket);
                allDone.WaitOne();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void AcceptCallback(IAsyncResult asyncResult)
    {
        allDone.Set();
        var acceptSocket = (Socket)asyncResult.AsyncState;
        Socket socket = acceptSocket.EndAccept(asyncResult);
        Connection state = new Connection(socket, 1024);
        newConnectionEvent.Invoke(this, new NewConnectionEventArgs(state));
    }
}