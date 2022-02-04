namespace CarRentalSystem.Statistics.Services.Statistics
{
    using Models.Statistics;

    public class StatisticsService : IStatisticsService
    {
        public StatisticsOutputModel Full() 
            => new StatisticsOutputModel
            {
                TotalCarAds = 0,
                TotalRentedCars = 0
            };
    }
}
