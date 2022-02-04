namespace RedBlackTree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private const bool RED = true;
        private const bool BLACK = false;

        private Node root;

        public void Insert(T element)
            => throw new NotImplementedException();

        public bool Contains(T element)
            => throw new NotImplementedException();

        public void EachInOrder(Action<T> action)
            => throw new NotImplementedException();

        public IBinarySearchTree<T> Search(T element)
            => throw new NotImplementedException();

        public void DeleteMin()
            => throw new NotImplementedException();

        public IEnumerable<T> Range(T startRange, T endRange)
            => throw new NotImplementedException();

        public virtual void Delete(T element)
            => throw new NotImplementedException();

        public void DeleteMax()
            => throw new NotImplementedException();

        public int Count()
            => throw new NotImplementedException();

        public int Rank(T element)
            => throw new NotImplementedException();

        public T Select(int rank)
            => throw new NotImplementedException();

        public T Ceiling(T element)
            => throw new NotImplementedException();

        public T Floor(T element)
            => throw new NotImplementedException();

        private class Node
        {
            public Node(T value, bool color = RED)
            {
                this.Value = value;
                this.Color = color;
            }

            public T Value { get; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public bool Color { get; set; }

            public int Count { get; set; }
        }
    }
}