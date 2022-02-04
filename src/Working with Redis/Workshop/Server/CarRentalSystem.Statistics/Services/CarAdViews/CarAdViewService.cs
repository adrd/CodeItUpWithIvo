namespace CarRentalSystem.Statistics.Services.CarAdViews
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarRentalSystem.Services.Data;
    using CarRentalSystem.Services.Messages;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.CarAdViews;

    using static StatisticsConstants.MemoryDatabaseKeys;

    public class CarAdViewService : DataService<CarAdView>, ICarAdViewService
    {
        private readonly IMemoryDatabase memoryDatabase;

        public CarAdViewService(
            DbContext db, 
            IPublisher publisher, 
            IMemoryDatabase memoryDatabase)
            : base(db, publisher) 
            => this.memoryDatabase = memoryDatabase;

        public async Task<int> GetTotalViews(int carAdId)
            => await this
                .All()
                .CountAsync(v => v.CarAdId == carAdId);

        public async Task<IEnumerable<CarAdViewOutputModel>> GetTotalViews(
            IEnumerable<int> ids)
            => await this
                .All()
                .Where(v => ids.Contains(v.CarAdId))
                .GroupBy(v => v.CarAdId)
                .Select(gr => new CarAdViewOutputModel
                {
                    CarAdId = gr.Key,
                    TotalViews = gr.Count()
                })
                .ToListAsync();

        public async Task GetMostViewedCarAds()
        {
            var topCarAdIds = await this.memoryDatabase
                .RangeSortedSet<int>(StatisticsCarAdViewsKey, -10, -1);
        }
    }
}
