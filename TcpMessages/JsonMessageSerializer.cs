using System;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace TcpMessages
{
    public class JsonMessageSerializer : IMessageSerializer
    {
        public string Serialize(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            var serializer = new DataContractJsonSerializer(message.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, message);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public object Deserialize(string text, Type messageType)
        {
            var serializer = new DataContractJsonSerializer(messageType);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(text)))
            {
                return serializer.ReadObject(ms);
            }
        }
    }
}