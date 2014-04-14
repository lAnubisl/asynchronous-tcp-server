using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
            var message = new LoginMessage { Username = "1111111", Password = "1111111" };
            var processor = new MessageProcessor();
            var json = processor.PrepareBeforeSend(message);

            var message2 = processor.PrepareAfterRecieve(json);





        }
    }
}
