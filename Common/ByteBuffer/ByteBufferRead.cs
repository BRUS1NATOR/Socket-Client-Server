using System.Text;

namespace Common.Buffer
{
    public partial class ByteBuffer : SocketTypePolicy
    {
        public T Read<T>(bool Peek = true)
        {
            if (this is not IType<T>)
            {
                throw new Exception("Type is not defined in any policy!");
            }
            return ((IType<T>)this).Read(Peek);
        }

        private T ReadBase<T>(Func<T> func, int bytesToRead, bool Peek)
        {
            if (buff.Count > readpos)
            {
                if (buffUpdated)
                {
                    readBuff = buff.ToArray();
                    buffUpdated = false;
                }
                T result = func();
                if (Peek && buff.Count > readpos)
                {
                    readpos += bytesToRead;
                }
                return result;
            }
            else
            {
                throw new Exception($"You are not trying to read a {typeof(T)}");
            }
        }

        byte IType<byte>.Read(bool Peek)
        {
            return ReadBase(() =>
            {
                return readBuff[readpos];
            }, 1, Peek);
        }

        byte[] IArrayType<byte>.Read(int Length, bool Peek)
        {
            return ReadBase(() =>
            {
                return buff.GetRange(readpos, Length).ToArray();
            }, 4, Peek);
        }

        int IType<int>.Read(bool Peek)
        {
            return ReadBase(() =>
            {
                return BitConverter.ToInt32(readBuff, readpos);
            }, 4, Peek);
        }

        uint IType<uint>.Read(bool Peek)
        {
            return ReadBase(() =>
            {
                return BitConverter.ToUInt32(readBuff, readpos);
            }, 4, Peek);
        }

        float IType<float>.Read(bool Peek)
        {
            return ReadBase(() =>
            {
                return BitConverter.ToSingle(readBuff, readpos);
            }, 4, Peek);
        }

        long IType<long>.Read(bool Peek)
        {
            return ReadBase(() =>
            {
                return BitConverter.ToInt64(readBuff, readpos);
            }, 8, Peek);
        }

        string IType<string>.Read(bool Peek)
        {
            int stringLength = Read<int>(true);

            return ReadBase(() =>
            {
                string result = Encoding.ASCII.GetString(readBuff, readpos, stringLength);
                if (Peek == false)
                {
                    readpos -= stringLength;
                }
                return result;
            }, stringLength, Peek);
        }

        bool IType<bool>.Read(bool Peek)
        {
            return ReadBase(() =>
            {
                return BitConverter.ToBoolean(readBuff, readpos);
            }, 1, Peek);
        }
    }
}
