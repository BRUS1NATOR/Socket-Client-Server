using Common.Buffer;

namespace Common.Messages
{
    public interface IBaseMessage
    {
        public ByteBuffer GetBuffer();
    }

    public class BaseMessage : IBaseMessage
    {
        protected ByteBuffer _buffer;

        public BaseMessage()
        {
            _buffer = new ByteBuffer();
        }

        public BaseMessage(ByteBuffer? buffer)
        {
            if (buffer == null)
            {
                _buffer = new ByteBuffer();
            }
            else
            {
                _buffer = buffer;
                ReadBuffer();
            }
        }

        protected virtual void WriteToBuffer()
        {
        }

        public virtual void ReadBuffer()
        {
        }

        public ByteBuffer GetBuffer()
        {
            WriteToBuffer();
            return _buffer;
        }
    }
}
