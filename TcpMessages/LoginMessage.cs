using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TcpMessages
{
    [DataContract]
    public class LoginMessage
    {
        [DataMember]
        public string Username;

        [DataMember]
        public string Password;
    }
}
