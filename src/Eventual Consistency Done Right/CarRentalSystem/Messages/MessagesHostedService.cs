namespace CarRentalSystem.Messages
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Models;
    using Hangfire;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class MessagesHostedService : IHostedService
    {
        private readonly IRecurringJobManager jobManager;
        private readonly IServiceProvider serviceProvider;
        private readonly IBus publisher;

        public MessagesHostedService(
            IRecurringJobManager jobManager, 
            IServiceProvider serviceProvider, 
            IBus publisher)
        {
            this.jobManager = jobManager;
            this.serviceProvider = serviceProvider;
            this.publisher = publisher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.jobManager.AddOrUpdate(
                nameof(MessagesHostedService),
                () => this.ProcessMessages(),
                "*/5 * * * * *");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public void ProcessMessages()
        {
            using var data = this.serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetService<DbContext>();

            var messages = data
                .Set<Message>()
                .Where(m => !m.Published)
                .ToList();

            foreach (var message in messages)
            {
                this.publisher
                    .Publish(message.Data, message.Type)
                    .GetAwaiter()
                    .GetResult();

                message.MarkAsPublished();

                data.SaveChanges();
            }
        }
    }
}
