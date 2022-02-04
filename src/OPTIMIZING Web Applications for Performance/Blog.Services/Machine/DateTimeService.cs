namespace Blog.Services.Machine
{
    using System;

    public class DateTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
