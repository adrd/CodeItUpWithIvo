namespace Trees
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node root;

        public BinarySearchTree() => this.root = null;

        private BinarySearchTree(Node node) => this.Copy(node);

        public void Insert(T value) => this.root = this.Insert(this.root, value);

        public bool Contains(T value)
        {
            var current = this.root;

            while (current != null)
            {

                var compare = current.Value.CompareTo(value);

                if (compare > 0)
                {
                    current = current.Left;
                }
                else if (compare < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                return;
            }

            if (this.root.Left == null && this.root.Right == null)
            {
                this.root = null;
                return;
            }

            Node parent = null;
            var current = this.root;

            while (current.Left != null)
            {
                parent = current;
                current = current.Left;
            }

            parent.Left = current.Right ?? null;
        }

        public BinarySearchTree<T> Search(T item)
        {
            var current = this.root;

            while (current != null)
            {
                var compare = current.Value.CompareTo(item);

                if (compare > 0)
                {
                    current = current.Left;
                }
                else if (compare < 0)
                {
                    current = current.Right;
                }
                else if (compare == 0)
                {
                    return new BinarySearchTree<T>(current);
                }
            }

            return null;
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var result = new List<T>();

            this.Range(this.root, result, startRange, endRange);

            return result;
        }

        public void EachInOrder(Action<T> action) => this.EachInOrder(this.root, action);

        private void Copy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.Copy(node.Left);
            this.Copy(node.Right);
        }

        private Node Insert(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            var compare = node.Value.CompareTo(value);

            if (compare > 0)
            {
                node.Left = this.Insert(node.Left, value);
            }
            else if (compare < 0)
            {
                node.Right = this.Insert(node.Right, value);
            }

            return node;
        }

        private void Range(Node node, List<T> result, T start, T end)
        {
            if (node == null)
            {
                return;
            }

            var compareLow = node.Value.CompareTo(start);
            var compareHigh = node.Value.CompareTo(end);

            if (compareLow > 0)
            {
                this.Range(node.Left, result, start, end);
            }
            if (compareLow >= 0 && compareHigh <= 0)
            {
                result.Add(node.Value);
            }
            if (compareHigh < 0)
            {
                this.Range(node.Right, result, start, end);
            }
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);

            action(node.Value);

            this.EachInOrder(node.Right, action);
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Left = null;
                this.Right = null;
            }

            public T Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }
        }
    }
}