namespace CarRentalSystem.Infrastructure
{
    using System;
    using System.Reflection;
    using System.Text;
    using AutoMapper;
    using GreenPipes;
    using Hangfire;
    using MassTransit;
    using Messages;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using Services.Identity;

    public static class ServiceCollectionExtensions
    {
        private const string DefaultConnection = "DefaultConnection";

        public static IServiceCollection AddWebService<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TDbContext : DbContext
        {
            services
                .AddDatabase<TDbContext>(configuration)
                .AddApplicationSettings(configuration)
                .AddTokenAuthentication(configuration)
                .AddAutoMapperProfile(Assembly.GetCallingAssembly())
                .AddControllers();

            return services;
        }

        public static IServiceCollection AddDatabase<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TDbContext : DbContext
            => services
                .AddScoped<DbContext, TDbContext>()
                .AddDbContext<TDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString(DefaultConnection)));

        public static IServiceCollection AddApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<ApplicationSettings>(
                    configuration.GetSection(nameof(ApplicationSettings)), 
                    config => config.BindNonPublicProperties = true);

        public static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }

        public static IServiceCollection AddAutoMapperProfile(
            this IServiceCollection services,
            Assembly assembly)
            => services
                .AddAutoMapper(
                    (_, config) => config
                        .AddProfile(new MappingProfile(assembly)),
                    Array.Empty<Assembly>());

        public static IServiceCollection AddMessages(
            this IServiceCollection services,
            IConfiguration configuration,
            params Type[] consumers)
        {
            services
                .AddMassTransit(mt =>
                {
                    foreach (var consumer in consumers)
                    {
                        mt.AddConsumer(consumer);
                    }

                    mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host("localhost");

                        foreach (var consumer in consumers)
                        {
                            rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                            {
                                endpoint.PrefetchCount = 6;
                                endpoint.UseMessageRetry(x => x.Interval(5, 100));
                                endpoint.ConfigureConsumer(context, consumer);
                            });
                        }
                    }));
                })
                .AddMassTransitHostedService();

            services
                .AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(configuration.GetConnectionString(DefaultConnection)));

            services.AddHangfireServer();

            services.AddHostedService<MessagesHostedService>();

            return services;
        }
    }
}
