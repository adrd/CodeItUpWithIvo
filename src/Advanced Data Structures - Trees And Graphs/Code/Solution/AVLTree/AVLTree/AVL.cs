namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public bool Contains(T item)
        {
            var node = this.Search(this.Root, item);
            return node != null;
        }

        public void Insert(T item) => this.Root = this.Insert(this.Root, item);

        public void EachInOrder(Action<T> action) => this.EachInOrder(this.Root, action);

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            var cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
            
            // Balancing the tree
            var balance = this.Height(node.Left) - this.Height(node.Right);

            // Left subtree is bigger
            if (balance > 1)
            {
                var childBalance = this.Height(node.Left.Left) - this.Height(node.Left.Right);
                if (childBalance < 0)
                {
                    node.Left = this.RotateLeft(node.Left);
                }

                node = this.RotateRight(node);
            } 
            // Right subtree is bigger
            else if(balance < -1)
            {
                var childBalance = this.Height(node.Right.Left) - this.Height(node.Right.Right);
                if (childBalance > 0)
                {
                    node.Right = this.RotateRight(node.Right);
                }

                node = this.RotateLeft(node);
            }

            return node;
        }

        private Node<T> RotateRight(Node<T> node)
        {
            var newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;

            node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
            newRoot.Height = 1 + Math.Max(this.Height(newRoot.Left), this.Height(newRoot.Right));

            return newRoot;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;

            node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
            newRoot.Height = 1 + Math.Max(this.Height(newRoot.Left),this.Height(newRoot.Right));

            return newRoot;
        }

        private int Height(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Node<T> Search(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            var cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return this.Search(node.Left, item);
            }
            else if (cmp > 0)
            {
                return this.Search(node.Right, item);
            }

            return node;
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);

            action(node.Value);

            this.EachInOrder(node.Right, action);
        }
    }
}
