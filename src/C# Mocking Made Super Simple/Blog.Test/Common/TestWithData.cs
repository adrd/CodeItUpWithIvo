namespace Blog.Test.Common
{
    using System.Threading.Tasks;
    using Data;
    using Data.Models;
    using Fakes;

    public abstract class TestWithData
    {
        protected async Task InitializeDatabase(string databaseName)
        {
            var fakeDatabase = new FakeBlogDbContext(databaseName);

            await AddFakeArticles(fakeDatabase);

            this.Database = fakeDatabase.Data;
        }

        protected BlogDbContext Database { get; private set; }

        private static async Task AddFakeArticles(FakeBlogDbContext fakeDb)
            => await fakeDb.Add(new Article
                {
                    Id = 1,
                    UserId = "1"
                },
                new Article
                {
                    Id = 2,
                    UserId = "2",
                    IsPublic = true
                });
    }
}
