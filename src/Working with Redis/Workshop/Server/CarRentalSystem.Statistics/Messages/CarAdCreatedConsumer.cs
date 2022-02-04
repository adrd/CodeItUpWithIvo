namespace CarRentalSystem.Statistics.Messages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarRentalSystem.Data.Models;
    using CarRentalSystem.Messages.Dealers;
    using CarRentalSystem.Services.Data;
    using CarRentalSystem.Services.Messages;
    using Data;
    using MassTransit;

    using static StatisticsConstants.MemoryDatabaseKeys;

    public class CarAdCreatedConsumer : IConsumer<CarAdCreatedMessage>
    {
        private readonly StatisticsDbContext data;
        private readonly IMemoryDatabase memoryDatabase;
        private readonly IMessageService messages;

        public CarAdCreatedConsumer(
            StatisticsDbContext data,
            IMemoryDatabase memoryDatabase,
            IMessageService messages)
        {
            this.data = data;
            this.memoryDatabase = memoryDatabase;
            this.messages = messages;
        }

        public async Task Consume(ConsumeContext<CarAdCreatedMessage> context)
        {
            var message = context.Message;

            var isDuplicated = await this.messages.IsDuplicated(
                message,
                nameof(CarAdCreatedMessage.CarAdId),
                message.CarAdId);

            if (isDuplicated)
            {
                return;
            }

            await this.memoryDatabase.Increment(StatisticsTotalCarAdsKey);

            var carAdValues = new Dictionary<string, object>
            {
                [nameof(context.Message.Manufacturer)] = context.Message.Manufacturer,
                [nameof(context.Message.Model)] = context.Message.Model,
                [nameof(context.Message.PricePerDay)] = context.Message.PricePerDay,
                [nameof(context.Message.ImageUrl)] = context.Message.ImageUrl
            };

            await this.memoryDatabase.SetHash($"{CarAdDetailsKey}:{context.Message.CarAdId}", carAdValues);

            var dataMessage = new Message(message);

            this.data.Messages.Add(dataMessage);

            await this.data.SaveChangesAsync();
        }
    }
}
