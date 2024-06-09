using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace AUTD3Sharp.Utils
{
    internal class PList<T>
    {
        private const int DefaultCapacity = 4;

        internal T[] Items;
        private int _size;

        public PList()
        {
            Items = Array.Empty<T>();
        }

        public PList(int capacity)
        {
            Items = capacity == 0 ? Array.Empty<T>() : new T[capacity];
        }

        public PList(IEnumerable<T> collection)
        {
            if (collection is ICollection<T> c)
            {
                var count = c.Count;
                if (count == 0)
                    Items = Array.Empty<T>();
                else
                {
                    Items = new T[count];
                    c.CopyTo(Items, 0);
                    _size = count;
                }
            }
            else
            {
                Items = Array.Empty<T>();
                using var en = collection.GetEnumerator();
                while (en.MoveNext()) Add(en.Current);
            }
        }

        public int Capacity
        {
            get => Items.Length;
            set
            {
                if (value == Items.Length) return;
                if (value > 0)
                {
                    var newItems = new T[value];
                    if (_size > 0)
                        Array.Copy(Items, newItems, _size);
                    Items = newItems;
                }
                else
                    Items = Array.Empty<T>();
            }
        }

        public int Count => _size;

        public T this[int index]
        {
            get => Items[index];
            set => Items[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            var array = Items;
            var size = _size;
            if ((uint)size < (uint)array.Length)
            {
                _size = size + 1;
                array[size] = item;
            }
            else
                AddWithResize(item);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddWithResize(T item)
        {
            Debug.Assert(_size == Items.Length);
            var size = _size;
            Grow(size + 1);
            _size = size + 1;
            Items[size] = item;
        }

        internal void Grow(int capacity)
        {
            var newCapacity = Items.Length == 0 ? DefaultCapacity : 2 * Items.Length;
            if (newCapacity < capacity) newCapacity = capacity;
            Capacity = newCapacity;
        }
    }

}
