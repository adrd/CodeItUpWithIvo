namespace Blog.Tests.Infrastructure
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public abstract class TestsWithData
    {
        protected TestsWithData()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;

            this.Data = new BlogDbContext(options);
        }

        public BlogDbContext Data { get; }

        public void AddData(params object[] data)
        {
            this.Data.AddRange(data);
            this.Data.SaveChangesAsync();
        }
    }
}
