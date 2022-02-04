namespace CarRentalSystem.Data
{
    using System.Reflection;
    using Configurations;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected abstract Assembly ConfigurationsAssembly { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfigurationsFromAssembly(this.ConfigurationsAssembly);

            base.OnModelCreating(builder);
        }
    }
}
