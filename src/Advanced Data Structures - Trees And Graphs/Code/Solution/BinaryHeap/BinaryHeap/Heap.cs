namespace BinaryHeap
{
    using System;

    public static class Heap<T> where T : IComparable<T>
    {
        public static void Sort(T[] arr)
        {
            ConstructHeap(arr);
            HeapSort(arr);
        }

        private static void ConstructHeap(T[] arr)
        {
            for (var i = arr.Length / 2; i >= 0; i--)
            {
                HeapifyDown(arr, i, arr.Length);
            }
        }

        private static void HeapSort(T[] arr)
        {
            for (var i = arr.Length - 1; i >= 0; i--)
            {
                Swap(arr, 0, i);
                HeapifyDown(arr, 0, i);
            }
        }

        private static void HeapifyDown(T[] arr, int index, int length)
        {
            var parentIndex = index;

            while (parentIndex < length / 2)
            {
                // Left child
                var childIndex = parentIndex * 2 + 1;

                // Check if there is right child && right child > left Child
                if (childIndex + 1 < length && IsGreater(arr, childIndex, childIndex + 1))
                {
                    // Right child ( left child + 1)
                    childIndex += 1;
                }

                var compare = arr[parentIndex].CompareTo(arr[childIndex]);

                if (compare < 0)
                {
                    Swap(arr, childIndex, parentIndex);
                }

                parentIndex = childIndex;
            }

        }

        private static bool IsGreater(T[] heap, int left, int right)
            => heap[left].CompareTo(heap[right]) < 0;

        private static void Swap(T[] heap, int parent, int index)
        {
            var temp = heap[parent];
            heap[parent] = heap[index];
            heap[index] = temp;
        }
    }
}
