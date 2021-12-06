using Common.Buffer;
using Common.MessageHandlers;
using Common.Messages;
using System.Net.Sockets;

namespace Common
{
    public class MessageSender
    {
        public virtual void Send(IBaseMessage message, Socket socket)
        {
            if (socket == null)
            {
                return;
            }
            int messageId = MessageHandle.MsgConfig.GetMessageId(message);
            if (messageId == 0)
            {
                throw new Exception($"No id specified for type {message.GetType()} create it with 'AddMessageType' in MessageFactory");
            }
            var buffer = CreateBuffer(messageId, message);

            Console.WriteLine($"SEND MESSAGE {message} ({messageId})");

            socket.Send(buffer);
        }

        public byte[] CreateBuffer(int messageId, IBaseMessage message)
        {
            var msgBuff = message.GetBuffer();

            var buffer = new ByteBuffer();
            buffer.Write(messageId);
            buffer.Write(msgBuff.ToArray());

            return buffer;
        }
    }

    public class ResponseToMachine : MessageSender
    {
        readonly Socket _socket;

        public ResponseToMachine(Socket socket)
        {
            _socket = socket;
        }

        public void Send(IBaseMessage message)
        {
            Send(message, _socket);
        }
    }
}
