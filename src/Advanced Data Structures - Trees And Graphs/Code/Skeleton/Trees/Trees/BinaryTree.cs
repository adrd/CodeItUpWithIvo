namespace Trees
{
    using System;

    public class BinaryTree<T>
    {
        public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
        {
            this.Value = value;
            this.Left = leftChild;
            this.Right = rightChild;
        }

        public T Value { get; set; }

        public BinaryTree<T> Left { get; set; }

        public BinaryTree<T> Right { get; set; }

        public void PrintIndentedPreOrder(int indent = 0)
            => throw new NotImplementedException();

        public void EachInOrder(Action<T> action)
            => throw new NotImplementedException();

        public void EachPostOrder(Action<T> action)
            => throw new NotImplementedException();
    }
}
