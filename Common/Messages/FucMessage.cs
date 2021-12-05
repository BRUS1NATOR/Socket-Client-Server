using Common.Buffer;

namespace Common.Messages
{
    public class IntMessage : BaseMessage
    {
        public int Num { get; set; }

        public IntMessage() : base()
        {
        }

        public IntMessage(ByteBuffer? buffer) : base(buffer)
        {
        }

        public override void ReadBuffer()
        {
            base.ReadBuffer();

            Num = _buffer.Read<int>();
        }

        protected override void WriteToBuffer()
        {
            _buffer.Write(Num);
        }
    }
}
