namespace TcpMessages
{
    public interface ISymmetricMessageEncryptor
    {
        byte[] Encrypt(byte[] message, string password);

        byte[] Decrypt(byte[] message, string password);
    }
}