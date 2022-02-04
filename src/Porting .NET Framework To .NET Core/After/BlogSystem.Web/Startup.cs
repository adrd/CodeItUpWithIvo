namespace BlogSystem.Web
{
    using System.Reflection;
    using AutoMapper;
    using Data;
    using Data.Contracts;
    using Data.Models;
    using Data.Repositories;
    using Data.Seed;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<IApplicationDbContext, ApplicationDbContext>();
            
            services
                .AddIdentity<ApplicationUser, IdentityRole>(options => options
                    .SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IDatabaseSeeder, SettingsSeeder>();
            services.AddTransient<IDatabaseSeeder, AdministratorSeeder>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            app.Initialize();
        }
    }
}
