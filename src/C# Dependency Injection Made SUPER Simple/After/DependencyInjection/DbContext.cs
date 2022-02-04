namespace DependencyInjection
{
    using System;
    using System.Collections.Generic;

    public class DbContext : IDbContext
    {
        private readonly string connection;

        public DbContext(IAppSettings appSettings) 
            => this.connection = appSettings.ConnectionString;

        public List<Cat> GetCats()
            => new List<Cat>
            {
                new Cat { Id = 1, Name = this.connection, AddedOn = DateTime.Now.AddDays(-2) },
                new Cat { Id = 2, Name = this.connection, AddedOn = DateTime.Now.AddDays(-1) },
                new Cat { Id = 3, Name = this.connection, AddedOn = DateTime.Now }
            };
    }
}
