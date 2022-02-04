namespace CarRentalSystem.Statistics.Services.Statistics
{
    using System.Threading.Tasks;
    using CarRentalSystem.Services.Data;
    using Models.Statistics;

    using static StatisticsConstants.MemoryDatabaseKeys;

    public class StatisticsService : IStatisticsService
    {
        private readonly IMemoryDatabase memoryDatabase;

        public StatisticsService(IMemoryDatabase memoryDatabase) 
            => this.memoryDatabase = memoryDatabase;

        public async Task<StatisticsOutputModel> Full() 
            => new StatisticsOutputModel
            {
                TotalCarAds = await this.memoryDatabase.Get<int>(StatisticsRentedCarAdsKey),
                TotalRentedCars = await this.memoryDatabase.Get<int>(StatisticsRentedCarAdsKey)
            };
    }
}
