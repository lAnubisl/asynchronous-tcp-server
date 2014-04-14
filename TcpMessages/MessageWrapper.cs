using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TcpMessages
{
    [DataContract]
    public class MessageWrapper
    {
        [DataMember]
        public string MessageType;

        [DataMember]
        public string MessageBody;
    }
}
