namespace DependencyInjection
{
    using System;

    public interface ICurrentTimeProvider
    {
        DateTime Now();
    }
}
