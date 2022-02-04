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
        {
            this.heap.Add(item);
            this.HeapifyUpIterative(this.heap.Count - 1);
        }

        public T Peek()
        {
            if (this.heap.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.heap[0];
        }

        public T Pull()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var element = this.heap[0];
            this.Swap(0, this.Count - 1);
            this.heap.RemoveAt(this.Count - 1);
            this.HeapifyDown(0);

            return element;
        }

        private void HeapifyUpIterative(int index)
        {
            var childIndex = index;
            var element = this.heap[childIndex];
            var parentIndex = (childIndex - 1) / 2;
            var compare = this.heap[parentIndex].CompareTo(element);

            while (parentIndex >= 0 && compare < 0)
            {
                this.Swap(parentIndex, childIndex);
                childIndex = parentIndex;
                parentIndex = (parentIndex - 1) / 2;
                if (childIndex == parentIndex)
                {
                    break;
                }
            }
        }

        private void Swap(int parent, int index)
        {
            var temp = this.heap[parent];
            this.heap[parent] = this.heap[index];
            this.heap[index] = temp;
        }

        private void HeapifyDown(int index)
        {
            var parentIndex = index;

            while (parentIndex < this.Count / 2)
            {
                // Left child
                var childIndex = (parentIndex * 2) + 1;

                // Check if there is right child && right child > left child
                if (childIndex + 1 < this.Count && this.IsGreater(childIndex, childIndex + 1))
                {
                    // Right Child ( left child + 1)
                    childIndex += 1;
                }

                var compare = this.heap[parentIndex].CompareTo(this.heap[childIndex]);

                if (compare < 0)
                {
                    this.Swap(childIndex, parentIndex);
                }

                parentIndex = childIndex;
            }
        }

        private bool IsGreater(int left, int right)
            => this.heap[left].CompareTo(this.heap[right]) < 0;
    }
}
