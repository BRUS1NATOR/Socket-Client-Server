using Common.Buffer;
using Common.Messages;

namespace Common.Message.Server
{
    public class ServerResponseNumMessage : IntMessage
    {
        public ServerResponseNumMessage() : base()
        {
        }

        public ServerResponseNumMessage(ByteBuffer? buffer) : base(buffer)
        {

        }
    }
}