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
            var serializer = new DataContractJsonSerializer(message.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, message);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public object Deserialize(string json, Type messaType)
        {
            var serializer = new DataContractJsonSerializer(messaType);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return serializer.ReadObject(ms);
            }
        }
    }
}