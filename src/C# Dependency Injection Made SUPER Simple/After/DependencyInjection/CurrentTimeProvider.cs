namespace DependencyInjection
{
    using System;

    public class CurrentTimeProvider : ICurrentTimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}
