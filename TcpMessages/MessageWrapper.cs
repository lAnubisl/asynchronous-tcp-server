using System.Runtime.Serialization;
namespace TcpMessages
{
    [DataContract]
    public class MessageWrapper
    {
        [DataMember]
        public string MessageType { get; set; }

        [DataMember]
        public string MessageBody { get; set; }
    }
}
