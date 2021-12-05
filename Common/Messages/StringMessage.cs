using Common.Buffer;

namespace Common.Messages
{
    public class StringMessage : BaseMessage
    {
        public string Message { get; set; }

        public StringMessage() : base()
        {
        }

        public StringMessage(ByteBuffer? buffer) : base(buffer)
        {

        }

        public override void ReadBuffer()
        {
            base.ReadBuffer();

            Message = _buffer.Read<string>();
        }

        protected override void WriteToBuffer()
        {
            _buffer.Write(Message);
        }
    }
}
