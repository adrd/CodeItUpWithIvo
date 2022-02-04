namespace IntervalTree
{
    using System;
    using System.Collections.Generic;

    public class IntervalTree
    {
        private class Node
        {
            public Node(Interval interval)
            {
                this.Interval = interval;
                this.Max = interval.Hi;
            }

            internal Interval Interval { get; }

            internal double Max { get; set; }

            internal Node Right { get; set; }

            internal Node Left { get; set; }
        }

        private Node root;

        public void Insert(double lo, double hi) 
            => this.root = this.Insert(this.root, lo, hi);

        public void EachInOrder(Action<Interval> action) 
            => this.EachInOrder(this.root, action);

        public Interval SearchAny(double lo, double hi)
        {
            var current = this.root;

            while (current != null && !current.Interval.Intersects(lo, hi))
            {
                if (current.Left != null && current.Left.Max > lo)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return current?.Interval;
        }

        public IEnumerable<Interval> SearchAll(double lo, double hi)
        {
            var result = new List<Interval>();

            if (this.root == null)
            {
                return result;
            }

            this.SearchAll(this.root, result, new Interval(lo, hi));

            return result;
        }

        private void SearchAll(Node node, ICollection<Interval> result, Interval i)
        {
            if (node.Left != null && node.Left.Max > i.Lo)
            {
                this.SearchAll(node.Left, result, i);
            }

            if (node.Interval.Intersects(i))
            {
                result.Add(node.Interval);
            }

            if (node.Right != null && node.Right.Interval.Lo < i.Hi)
            {
                this.SearchAll(node.Right, result, i);
            }
        }

        private void EachInOrder(Node node, Action<Interval> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);

            action(node.Interval);

            this.EachInOrder(node.Right, action);
        }

        private Node Insert(Node node, double lo, double hi)
        {
            if (node == null)
            {
                return new Node(new Interval(lo, hi));
            }

            var cmp = lo.CompareTo(node.Interval.Lo);

            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, lo, hi);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, lo, hi);
            }

            this.UpdateMax(node);

            return node;
        }

        private void UpdateMax(Node node)
        {
            var max = Math.Max(this.GetMax(node.Left), this.GetMax(node.Right));

            node.Max = Math.Max(max, node.Max);
        }

        private double GetMax(Node node) => node?.Max ?? 0d;
    }
}
