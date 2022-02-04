namespace Blog.Test.Fakes
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class FakeBlogDbContext
    {
        public FakeBlogDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(name)
                .Options;

            this.Data = new BlogDbContext(options);
        }

        public BlogDbContext Data { get; }

        public async Task Add(params object[] data)
        {
            this.Data.AddRange(data);
            await this.Data.SaveChangesAsync();
        }
    }
}
