using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Extensions.Collections
{
    public static class ListExt
    {
        public static T Pop<T>(this List<T> list)
        {
            var result = list[^1];
            list.RemoveAt(list.Count - 1);
            return result;
        }
    }
}
