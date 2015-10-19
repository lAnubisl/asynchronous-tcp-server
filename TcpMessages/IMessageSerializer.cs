using System;

namespace TcpMessages
{
    public interface IMessageSerializer
    {
        string Serialize(object message);

        object Deserialize(string text, Type messageType);
    }
}