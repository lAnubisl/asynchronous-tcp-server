namespace TcpMessages
{
    public interface IMessageProcessor
    {
        byte[] PrepareBeforeSend(object message);

        object PrepareAfterRecieve(byte[] str);
    }
}