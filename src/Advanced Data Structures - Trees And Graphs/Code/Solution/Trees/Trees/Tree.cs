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

        public void Print(int indent = 0) => this.Print(this, indent);

        public void Each(Action<T> action)
        {
            action(this.Value);
            foreach (var child in this.Children)
            {
                child.Each(action);
            }
        }

        public IEnumerable<T> OrderDFS()
        {
            var result = new List<T>();

            this.DFS(this, result);

            return result;
        }
    
        public IEnumerable<T> OrderBFS()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current.Value);

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        private void DFS(Tree<T> node, List<T> result)
        {
            foreach (var child in node.Children)
            {
                this.DFS(child, result);
            }

            result.Add(node.Value);
        }

        private void Print(Tree<T> node, int indent)
        {
            Console.WriteLine($"{new string(' ', indent)}{node.Value}");

            foreach (var child in node.Children)
            {
                child.Print(indent + 2);
            }
        }
    }
}
