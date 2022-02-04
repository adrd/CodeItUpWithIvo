namespace CarRentalSystem.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class CarRentalHost
    {
        public static void Start<TStartup>(string[] args = null)
            where TStartup : class
        {
            args ??= Array.Empty<string>();

            var logConnection = Environment.GetEnvironmentVariable("LogConnection");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Http(logConnection)
                .CreateLogger();

            Host
                .CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder
                    .UseStartup<TStartup>())
                .Build()
                .Run();
        }
    }
}
