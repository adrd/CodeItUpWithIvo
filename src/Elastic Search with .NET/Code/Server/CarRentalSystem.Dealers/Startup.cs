namespace CarRentalSystem.Dealers
{
    using Infrastructure;
    using CarRentalSystem.Services.Data;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Services.CarAds;
    using Services.Categories;
    using Services.Dealers;
    using Services.Manufacturers;
    using CarRentalSystem.Dealers.Models.CarAds;

    public class Startup
    {
        public Startup(IConfiguration configuration) 
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddWebService<DealersDbContext>(this.Configuration)
                .AddTransient<IDataSeeder, DealersDataSeeder>()
                .AddTransient<IDealerService, DealerService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<ICarAdService, CarAdService>()
                .AddTransient<IManufacturerService, ManufacturerService>()
                .AddMessaging(this.Configuration)
                .AddFullTextSearch<CarAdFullTextSearchModel>(this.Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseSerilogRequestLogging()
                .UseWebService(env)
                .Initialize();
    }
}
