namespace RedBlackTree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private const bool RED = true;
        private const bool BLACK = false;

        private Node root;

        public RedBlackTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
            this.root.Color = BLACK;
        }

        public bool Contains(T element)
        {
            var current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action) => this.EachInOrder(this.root, action);

        public IBinarySearchTree<T> Search(T element)
        {
            var current = this.FindElement(element);

            return new RedBlackTree<T>(current);
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var queue = new Queue<T>();

            this.Range(this.root, queue, startRange, endRange);

            return queue;
        }

        public virtual void Delete(T element)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.Delete(element, this.root);
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMax(this.root);
        }

        public int Count() => this.Count(this.root);

        public int Rank(T element) => this.Rank(element, this.root);

        public T Select(int rank)
        {
            var node = this.Select(rank, this.root);
            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        public T Ceiling(T element) => this.Select(this.Rank(element) + 1);

        public T Floor(T element) => this.Select(this.Rank(element) - 1);

        private Node FindElement(T element)
        {
            var current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            if (this.IsRed(node.Right) && !this.IsRed(node.Left))
            {
                node = this.RotateLeft(node);
            }

            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }

            if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                node = this.FlipColors(node);
            }

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private Node FlipColors(Node node)
        {
            node.Left.Color = BLACK;
            node.Right.Color = BLACK;
            node.Color = RED;

            return node;
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            temp.Color = node.Color;
            node.Color = RED;
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return temp;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            temp.Color = node.Color;
            node.Color = RED;
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return temp;
        }

        private bool IsRed(Node node) => node?.Color == RED;

        private void Range(Node node, Queue<T> queue, T startRange, T endRange)
        {
            if (node == null)
            {
                return;
            }

            var nodeInLowerRange = startRange.CompareTo(node.Value);
            var nodeInHigherRange = endRange.CompareTo(node.Value);

            if (nodeInLowerRange < 0)
            {
                this.Range(node.Left, queue, startRange, endRange);
            }
            if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
            {
                queue.Enqueue(node.Value);
            }
            if (nodeInHigherRange > 0)
            {
                this.Range(node.Right, queue, startRange, endRange);
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

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private RedBlackTree(Node node) => this.PreOrderCopy(node);

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = this.DeleteMin(node.Left);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                node.Left = this.Delete(element, node.Left);
            }
            else if (compare > 0)
            {
                node.Right = this.Delete(element, node.Right);
            }
            else
            {
                if (node.Right == null)
                {
                    return node.Left;
                }
                if (node.Left == null)
                {
                    return node.Right;
                }

                var temp = node;
                node = this.FindMin(temp.Right);
                node.Right = this.DeleteMin(temp.Right);
                node.Left = temp.Left;
            }

            node.Count = this.Count(node.Left) + this.Count(node.Right) + 1;

            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindMin(node.Left);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = this.DeleteMax(node.Right);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            var compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                return this.Rank(element, node.Left);
            }

            if (compare > 0)
            {
                return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
            }

            return this.Count(node.Left);
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            var leftCount = this.Count(node.Left);
            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return this.Select(rank, node.Left);
            }
            else
            {
                return this.Select(rank - (leftCount + 1), node.Right);
            }
        }

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