using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesssoaLibrary
{
    public interface IReadWriteFile<T>
    {
        void Serializer(T obj);
        T Deserializer();
    }
}