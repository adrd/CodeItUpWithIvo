namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public bool Contains(T item)
            => throw new NotImplementedException();

        public void Insert(T item)
            => throw new NotImplementedException();

        public void EachInOrder(Action<T> action)
            => throw new NotImplementedException();
    }
}
