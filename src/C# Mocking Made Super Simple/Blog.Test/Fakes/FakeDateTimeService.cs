namespace Blog.Test.Fakes
{
    using System;
    using Blog.Services;
    using Blog.Services.Machine;

    public class FakeDateTimeService : IDateTimeService
    {
        public DateTime Now() => new DateTime(2020, 1, 1);
    }
}
