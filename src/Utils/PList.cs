using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp.Utils
{
    internal class PList<T>
    {
        private const int DefaultCapacity = 4;

        internal T[] Items = Array.Empty<T>();

        [ExcludeFromCodeCoverage]
        public int Capacity
        {
            get => Items.Length;
            set
            {
                if (value == Items.Length) return;
                if (value > 0)
                {
                    var newItems = new T[value];
                    if (Count > 0)
                        Array.Copy(Items, newItems, Count);
                    Items = newItems;
                }
                else
                    Items = Array.Empty<T>();
            }
        }

        public int Count { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item)
        {
            var array = Items;
            var size = Count;
            if ((uint)size < (uint)array.Length)
            {
                Count = size + 1;
                array[size] = item;
            }
            else
                AddWithResize(item);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddWithResize(T item)
        {
            var size = Count;
            Grow(size + 1);
            Count = size + 1;
            Items[size] = item;
        }

        [ExcludeFromCodeCoverage]
        internal void Grow(int capacity)
        {
            var newCapacity = Items.Length == 0 ? DefaultCapacity : 2 * Items.Length;
            if (newCapacity < capacity) newCapacity = capacity;
            Capacity = newCapacity;
        }
    }

}
