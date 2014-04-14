using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpMessages
{
    public interface IMessageSerializer<T>
    {
        string Serialize(T message);

        T Deserialize(string str);
    }
}
