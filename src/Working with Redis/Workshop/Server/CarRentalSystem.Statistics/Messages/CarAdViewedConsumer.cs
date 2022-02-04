namespace CarRentalSystem.Statistics.Messages
{
    using System.Threading.Tasks;
    using CarRentalSystem.Messages.Dealers;
    using CarRentalSystem.Services.Data;
    using MassTransit;

    using static StatisticsConstants.MemoryDatabaseKeys;

    public class CarAdViewedConsumer : IConsumer<CarAdViewedMessage>
    {
        private readonly IMemoryDatabase memoryDatabase;

        public CarAdViewedConsumer(IMemoryDatabase memoryDatabase) 
            => this.memoryDatabase = memoryDatabase;

        public async Task Consume(ConsumeContext<CarAdViewedMessage> context)
            => await this.memoryDatabase
                .IncrementSortedSet(StatisticsCarAdViewsKey, context.Message.CarAdId);
    }
}
