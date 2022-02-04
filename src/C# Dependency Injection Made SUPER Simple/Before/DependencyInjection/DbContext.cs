namespace DependencyInjection
{
    using System;
    using System.Collections.Generic;

    public class DbContext
    {
        private readonly string connection;

        public DbContext()
        {
            var settings = new AppSettings();

            this.connection = settings.ConnectionString;
        }

        public List<Cat> GetCats() 
            => new List<Cat>
            {
                new Cat { Id = 1, Name = this.connection, AddedOn = DateTime.Now.AddDays(-2) },
                new Cat { Id = 2, Name = this.connection, AddedOn = DateTime.Now.AddDays(-1) },
                new Cat { Id = 3, Name = this.connection, AddedOn = DateTime.Now }
    };
    }
}
