using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Extensions.Collections
{
    /// <summary>
    /// An unstable list that allows for very fast removals at the cost of changing the order of the list.
    /// <remarks>
    /// <para>
    /// Iterating over the list forward is still safe even if removals are performed.
    /// </para>
    /// </remarks>
    /// </summary>
    public class FastList<T> : IList<T>, IEnumerable<T>
    {
        private const int MIN_SIZE = 4;

        /// <summary>
        /// The backing array. Use to access elements.
        /// </summary>
        public T[] Buffer;

        /// <summary>
        /// The number of elements in the list.
        /// </summary>
        public int Count { get; private set; } = 0;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return Buffer[index];
            }

            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Buffer[index] = value;
            }
        }

        /// <summary>
        /// Creates a new list with the default initial capacity.
        /// </summary>
        public FastList() : this(MIN_SIZE) { }

        /// <summary>
        /// Creates a new list with the specified initial capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public FastList(int capacity)
        {
            Buffer = new T[capacity < MIN_SIZE ? MIN_SIZE : capacity];
        }

        /// <summary>
        /// Creates a list from a collection of items.
        /// </summary>
        /// <param name="items"></param>
        public FastList(IEnumerable<T> items)
        {
            Buffer = items.ToArray();
            Count = Buffer.Length;
        }

        /// <summary>
        /// Clears all items from the list.
        /// </summary>
        public void Clear()
        {
            Array.Clear(Buffer, 0, Count);
            Count = 0;
        }

        /// <summary>
        /// Resets the list size to 0, without removing any items.
        /// </summary>
        public void Reset()
        {
            Count = 0;
        }

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (Count == Buffer.Length)
                Array.Resize(ref Buffer, Count << 1);
            Buffer[Count++] = item;
        }

        /// <summary>
        /// Removes an item from the list. O(n) time.
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(T item)
        {
            var comp = EqualityComparer<T>.Default;
            for (var i = 0; i < Count; ++i)
            {
                if (comp.Equals(Buffer[i], item))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the specified index from the list. O(1) time.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Buffer[index] = Buffer[Count - 1];
            Buffer[--Count] = default;
        }

        /// <summary>
        /// Determines if the list contains the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            var comp = EqualityComparer<T>.Default;
            for (var i = 0; i < Count; ++i)
            {
                if (comp.Equals(Buffer[i], item))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gets an enumerator used to iterate through the list.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return Buffer[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            var comp = EqualityComparer<T>.Default;
            for (var i = 0; i < Count; ++i)
            {
                if (comp.Equals(Buffer[i], item))
                    return i;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == Count)
                Add(item);

            if (Count == Buffer.Length)
                Array.Resize(ref Buffer, Count << 1);

            Buffer[Count++] = Buffer[index];
            Buffer[index] = item;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Buffer.CopyTo(array, arrayIndex);
        }

        public void Truncate()
        {
            var size = Count < MIN_SIZE ? MIN_SIZE : Count;
            Array.Resize(ref Buffer, size);
        }
    }
}
