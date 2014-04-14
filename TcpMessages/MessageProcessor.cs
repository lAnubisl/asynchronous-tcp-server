using System;
using System.Text;

namespace TcpMessages
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMessageSerializer messageSerializer = new JsonMessageSerializer();
        private readonly ISymmetricMessageEncryptor messageEncryptor = new RijndaelMessageEncryptor();
        private readonly IMessageCompressor messageCompressor = new GZipMessageCompressor();

        public byte[] PrepareBeforeSend(object message)
        {
            var messageJson = messageSerializer.Serialize(message);
            var wrapper = new MessageWrapper();
            wrapper.MessageBody = messageJson;
            wrapper.MessageType = message.GetType().FullName;
            var json = messageSerializer.Serialize(wrapper);
            var bytes = Encoding.UTF8.GetBytes(json);
            var gzipped = messageCompressor.Compress(bytes);
            var encrypted = messageEncryptor.Encrypt(gzipped, "12345678");
            return encrypted;
        }

        public object PrepareAfterRecieve(byte[] bytes)
        {
            var decrypted = messageEncryptor.Decrypt(bytes, "12345678");
            var decompressed = messageCompressor.Decompress(decrypted);
            var json = Encoding.UTF8.GetString(decompressed);
            var wrapper = (MessageWrapper)messageSerializer.Deserialize(json, typeof (MessageWrapper));
            var message = messageSerializer.Deserialize(wrapper.MessageBody, Type.GetType(wrapper.MessageType));
            return message;
        }
    }
}