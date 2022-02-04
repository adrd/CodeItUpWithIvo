namespace Blog.Services.Machine
{
    using System;

    public class RandomService : IRandomService
    {
        private static readonly Random Random = new Random();

        public int Next(int min, int max) => Random.Next(min, max);
    }
}
