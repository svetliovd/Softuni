using Bashsoft.IO.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Bashsoft.DataStructures
{
    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerCollection;
        private int size;
        private IComparer<T> comparison;

        public SimpleSortedList(IComparer<T> comparison, int capacity)
        {
            this.comparison = comparison;
            InitializeInnerCollection(capacity);
        }

        public SimpleSortedList(int capacity)
            :this(Comparer<T>.Create((x, y) => x.CompareTo(y)), capacity)
        {
        }

        public SimpleSortedList(IComparer<T> comparer)
            :this(comparer, DefaultSize)
        {
        }

        public SimpleSortedList()
            : this(DefaultSize)
        {
        }

        private void InitializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.innerCollection = new T[capacity];
        }

        public int Size => this.size;

        public int Capacity => this.innerCollection.Length;

        public void Add(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }
            if (this.innerCollection.Length <= this.size)
            {
                Resize();
            }
            this.innerCollection[size] = element;
            this.size++;
            //Array.Sort(this.innerCollection, 0, size, comparison);
            QuickSort(this.innerCollection, 0, this.Size - 1, comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            if (this.Size + collection.Count >= this.innerCollection.Length)
            {
                this.MultiResize(collection);
            }
            foreach (var element in collection)
            {
                this.innerCollection[this.Size] = element;
                this.size++;
            }

            //Array.Sort(this.innerCollection, 0, this.size, this.comparison);
            QuickSort(this.innerCollection, 0, this.Size - 1, comparison);
        }

        private void MultiResize(ICollection<T> collection)
        {
            int newSize = this.innerCollection.Length * 2;
            while (this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(this.innerCollection, newCollection, this.size);
            this.innerCollection = newCollection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        public string JoinWith(string joiner)
        {
            if (joiner == null)
            {
                throw new ArgumentNullException();
            }
            StringBuilder sb = new StringBuilder();
            foreach (var element in this)
            {
                sb.Append(element);
                sb.Append(joiner);
            }

            sb.Remove(sb.Length - joiner.Length, joiner.Length);
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void Resize()
        {
            T[] newCollection = new T[this.size * 2];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }

        public static void QuickSort<C>(C[] data, int left, int right, IComparer<C> comparer) where C : IComparable<C>
        {
            int i, j;
            C pivot, temp;
            i = left;
            j = right;
            pivot = data[(left + right) / 2];

            do
            {
                while (comparer.Compare(data[i], pivot) < 0 && (i < right)) i++;
                while (comparer.Compare(pivot, data[j]) < 0 && (j > left)) j--;
                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }
            } while (i <= j);

            if (left < j) QuickSort(data, left, j, comparer);
            if (i < right) QuickSort(data, i, right, comparer);
        }

        public bool Remove(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            bool hasBeenRemoved = false;
            int indexOfRemovedElement = 0;
            for (int i = 0; i < this.Size; i++)
            {
                if (this.innerCollection[i].Equals(element))
                {
                    indexOfRemovedElement = i;
                    this.innerCollection[i] = default(T);
                    hasBeenRemoved = true;
                    break;
                }
            }

            if (hasBeenRemoved)
            {
                for (int i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerCollection[i] = this.innerCollection[i + 1];
                }
                this.innerCollection[this.Size - 1] = default(T);
                this.size--;
            }
            return hasBeenRemoved;
        }
    }
}
