using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace TcpMessages
{
    public class JsonMessageSerializer<T> : IMessageSerializer<T>
    {
        public string Serialize(T message)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, message);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public T Deserialize(string str)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}