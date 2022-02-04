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
            => throw new NotImplementedException();

        private static void HeapSort(T[] arr)
            => throw new NotImplementedException();
    }
}
