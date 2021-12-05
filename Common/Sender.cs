using Common.Buffer;
using Common.MessageHandlers;
using Common.Messages;
using System.Net.Sockets;

namespace Common
{
    public class MessageSender
    {
        protected MessageHandle _messageHandler;

        public virtual void Send(IBaseMessage message, Socket socket)
        {
            if (socket == null)
            {
                return;
            }
            int messageId = _messageHandler.MsgConfig.GetMessageId(message);
            var buffer = CreateBuffer(messageId, message);

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

    public class ResponseToClient : MessageSender
    {
        readonly Socket _socket;

        public ResponseToClient(MessageHandle msgHandler, Socket socket)
        {
            _messageHandler = msgHandler;
            _socket = socket;
        }

        public void Send(IBaseMessage message)
        {
            Send(message, _socket);
        }
    }
}
