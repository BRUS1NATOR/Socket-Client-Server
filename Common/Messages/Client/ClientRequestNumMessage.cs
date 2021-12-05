using Common.Buffer;

namespace Common.Messages.Client
{
    public class ClientRequestNumMessage : StringMessage
    {
        public ClientRequestNumMessage() : base()
        {
        }

        public ClientRequestNumMessage(ByteBuffer? buffer) : base(buffer)
        {

        }
    }
}