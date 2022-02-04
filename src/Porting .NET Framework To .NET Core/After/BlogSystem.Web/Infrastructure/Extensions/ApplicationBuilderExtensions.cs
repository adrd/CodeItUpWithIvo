namespace BlogSystem.Web.Infrastructure.Extensions
{
    using Data;
    using Data.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            var data = services.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            var seeders = services.GetServices<IDatabaseSeeder>();

            foreach (var seeder in seeders)
            {
                seeder.Seed();
            }

            return app;
        }
    }
}
