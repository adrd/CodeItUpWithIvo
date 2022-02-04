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
            => throw new NotImplementedException();

        public void EachInOrder(Action<Interval> action)
            => throw new NotImplementedException();

        public Interval SearchAny(double lo, double hi)
            => throw new NotImplementedException();

        public IEnumerable<Interval> SearchAll(double lo, double hi)
            => throw new NotImplementedException();
    }
}
