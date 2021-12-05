using System.Text;

namespace Common.Buffer
{
    public partial class ByteBuffer : SocketTypePolicy
    {
        public void Write(byte input)
        {
            buff.Add(input);
            buffUpdated = true;
        }
        public void Write(byte[] input)
        {
            buff.AddRange(input);
            buffUpdated = true;
        }
        public void Write(short input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void Write(int input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void Write(uint input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void Write(float input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void Write(long input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
        public void Write(string input)
        {
            buff.AddRange(BitConverter.GetBytes(input.Length));
            buff.AddRange(Encoding.ASCII.GetBytes(input));
            buffUpdated = true;
        }
        public void Write(bool input)
        {
            buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }
    }
}
