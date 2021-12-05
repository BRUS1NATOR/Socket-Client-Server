using Common.Buffer;

namespace Common.Messages.Client
{
    public class ClientResponseFactorialMessage : BaseMessage
    {
        public int Num { get; set; }
        public long Factorial { get; set; }
        public ClientResponseFactorialMessage() : base()
        {
        }

        public ClientResponseFactorialMessage(ByteBuffer? buffer) : base(buffer)
        {

        }

        public override void ReadBuffer()
        {
            base.ReadBuffer();

            Num = _buffer.Read<int>();
            Factorial = _buffer.Read<long>();
        }

        protected override void WriteToBuffer()
        {
            _buffer.Write(Num);
            _buffer.Write(Factorial);
        }
    }
}
