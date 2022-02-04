namespace BinaryHeap
{
    using System;
    using System.Collections.Generic;

    public class BinaryHeap<T> where T : IComparable<T>
    {
        private readonly List<T> heap;

        public BinaryHeap() => this.heap = new List<T>();

        public int Count => this.heap.Count;

        public void Insert(T item)
            => throw new NotImplementedException();

        public T Peek()
            => throw new NotImplementedException();

        public T Pull()
            => throw new NotImplementedException();
    }
}
