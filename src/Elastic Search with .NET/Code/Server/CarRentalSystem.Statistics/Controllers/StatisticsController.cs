namespace CarRentalSystem.Statistics.Controllers
{
    using System.Threading.Tasks;
    using CarRentalSystem.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Models.Statistics;
    using Services.Statistics;

    public class StatisticsController : ApiController
    {
        private readonly IStatisticsService statistics;

        public StatisticsController(IStatisticsService statistics) 
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsOutputModel Full()
            => this.statistics.Full();
    }
}
