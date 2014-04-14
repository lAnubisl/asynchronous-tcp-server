using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpMessages;

namespace ConsoleApplication
{
    public class Class1
    {
        public static void Main()
        {
            var ser = new JsonMessageSerializer<LoginMessage>();
            var message = new LoginMessage { username = "admin", password = "123456" };
            var json = ser.Serialize(message);
            var wrapper = new MessageWrapper();
            wrapper.MessageType = message.GetType().ToString();
            wrapper.MessageBody = json;


            var ser2 = new JsonMessageSerializer<MessageWrapper>();
            var str = ser2.Serialize(wrapper);

            var m = ser2.Deserialize(str);


            var server = new TcpServer.TcpServer(9000, Encoding.ASCII);
        }
    }
}
