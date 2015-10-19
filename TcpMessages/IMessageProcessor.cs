namespace TcpMessages
{
    public interface IMessageProcessor
    {
        byte[] PrepareBeforeSend(IMessage message);

        IMessage PrepareAfterReceive(byte[] data);
    }
}