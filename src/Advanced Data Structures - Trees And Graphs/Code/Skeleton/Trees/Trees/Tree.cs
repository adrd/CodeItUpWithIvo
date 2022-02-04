namespace Trees
{
    using System;
    using System.Collections.Generic;

    public class Tree<T>
    {
        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>(children);
        }

        public T Value { get; private set; }

        public List<Tree<T>> Children { get; private set; }

        public void Print(int indent = 0)
            => throw new NotImplementedException();

        public void Each(Action<T> action)
            => throw new NotImplementedException();

        public IEnumerable<T> OrderDFS()
            => throw new NotImplementedException();

        public IEnumerable<T> OrderBFS()
            => throw new NotImplementedException();
    }
}
