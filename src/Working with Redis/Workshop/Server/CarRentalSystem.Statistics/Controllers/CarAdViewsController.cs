namespace CarRentalSystem.Statistics.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarRentalSystem.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.CarAdViews;
    using Services.CarAdViews;

    public class CarAdViewsController : ApiController
    {
        private readonly ICarAdViewService carAdViews;

        public CarAdViewsController(ICarAdViewService carAdViews) 
            => this.carAdViews = carAdViews;

        [HttpGet]
        [Route(Id)]
        public async Task<int> TotalViews(int id)
            => await this.carAdViews.GetTotalViews(id);

        [HttpGet]
        public async Task MostViewedCarAds()
            => await this.carAdViews.GetMostViewedCarAds();

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<CarAdViewOutputModel>> TotalViews(
            [FromQuery] IEnumerable<int> ids)
            => await this.carAdViews.GetTotalViews(ids);
    }
}
