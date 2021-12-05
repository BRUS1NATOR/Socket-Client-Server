namespace Common.Buffer
{
    public partial class ByteBuffer : IDisposable
    {
        private List<byte> buff;
        private byte[] readBuff;
        private int readpos = 0;
        private bool buffUpdated = false;

        public ByteBuffer()
        {
            buff = new List<byte>();
        }

        public ByteBuffer(byte[] data)
        {
            buff = new List<byte>();
            Write(data);
        }

        public static implicit operator byte[](ByteBuffer buffer)
        {
            return buffer.ToArray();
        }

        public static implicit operator ByteBuffer(byte[] buffer)
        {
            return new ByteBuffer(buffer);
        }

        public int GetReadPos()
        {
            return readpos;
        }

        public byte[] ToArray()
        {
            return buff.ToArray();
        }

        public int Count()
        {
            return buff.Count;
        }

        public int Length()
        {
            return Count() - readpos;
        }

        public void Clear()
        {
            buff.Clear();
            readpos = 0;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    buff.Clear();
                    readpos = 0;
                }
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}