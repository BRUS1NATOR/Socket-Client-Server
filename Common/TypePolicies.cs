using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Allowed type to write/read to ByteBuffer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface SocketTypePolicy : DefaultTypePolicy, CustomPolicies
    {
    }

    public interface DefaultTypePolicy : NumericPolicies, BaseTypePolicies
    {
    }


    public interface IType
    {

    }

    public interface IType<T> : IType
    {
        public T Read(bool Peek);

        public void Write(T value);
    }

    public interface IArrayType<T> : IType
    {
        public T[] Read(int length, bool Peek);

        public void Write(T[] value);
    }

    public interface CustomPolicies
    {

    }


    public interface BaseTypePolicies :
        IType<bool>,
        IType<string>,
        IType<byte>,
        IArrayType<byte>
    {

    }

    public interface NumericPolicies :
        IType<int>,
        IType<uint>,
        IType<float>,
        IType<long>
    {

    }
}
