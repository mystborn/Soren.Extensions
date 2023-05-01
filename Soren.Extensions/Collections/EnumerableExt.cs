using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Extensions.Collections
{
    public static class EnumerableExt
    {
        public static bool Unanimous<T>(this IEnumerable<T> enumerable)
            => Unanimous(enumerable, EqualityComparer<T>.Default);

        public static bool Unanimous<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
        {
            T? first = enumerable.FirstOrDefault();
            foreach(var item in enumerable)
            {
                if (!comparer.Equals(item, first))
                    return false;
            }

            return true;
        }
    }
}
