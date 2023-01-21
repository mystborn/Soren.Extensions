using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Extensions.Collections
{
    public interface IPool<T>
    {
        T Get();
        void Release(T item);
    }
}
