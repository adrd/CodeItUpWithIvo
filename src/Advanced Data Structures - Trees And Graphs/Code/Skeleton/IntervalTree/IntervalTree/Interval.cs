namespace IntervalTree
{
    using System;

    public class Interval
    {
        public Interval(double lo, double hi)
        {
            ValidateInterval(lo, hi);

            this.Lo = lo;
            this.Hi = hi;
        }

        public double Lo { get; set; }

        public double Hi { get; set; }

        public bool Intersects(double lo, double hi)
        {
            ValidateInterval(lo, hi);

            return this.Lo < hi && this.Hi > lo;
        }

        public bool Intersects(Interval other) => this.Intersects(other.Lo, other.Hi);

        public override bool Equals(object obj)
        {
            var other = (Interval)obj;
            return this.Lo == other.Lo && this.Hi == other.Hi;
        }

        public override string ToString() => $"({this.Lo}, {this.Hi})";

        private static void ValidateInterval(double lo, double hi)
        {
            if (hi < lo)
            {
                throw new ArgumentException();
            }
        }
    }
}