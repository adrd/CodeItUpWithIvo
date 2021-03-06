namespace CarRentalSystem.Dealers.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarRentalSystem.Controllers;
    using CarRentalSystem.Services;
    using CarRentalSystem.Services.FullTextSearch;
    using CarRentalSystem.Services.Identity;
    using Data.Models;
    using Messages.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models.CarAds;
    using Models.Categories;
    using Services.CarAds;
    using Services.Categories;
    using Services.Dealers;
    using Services.Manufacturers;

    public class CarAdsController : ApiController
    {
        private readonly ICarAdService carAds;
        private readonly IDealerService dealers;
        private readonly ICategoryService categories;
        private readonly IManufacturerService manufacturers;
        private readonly IFullTextSearch fullTextSearch;
        private readonly ICurrentUserService currentUser;

        public CarAdsController(
            ICarAdService carAds, 
            IDealerService dealers,
            ICategoryService categories,
            IManufacturerService manufacturers,
            IFullTextSearch fullTextSearch,
            ICurrentUserService currentUser,
            ILogger<CarAdsController> logger)
        {
            this.carAds = carAds;
            this.dealers = dealers;
            this.categories = categories;
            this.manufacturers = manufacturers;
            this.currentUser = currentUser;

            this.fullTextSearch = fullTextSearch;

            logger.LogError("Error!");
        }

        [HttpGet]
        public async Task<ActionResult<SearchCarAdsOutputModel>> Search(
            [FromQuery] CarAdsQuery query)
        {
            var carAdListings = await this.carAds.GetListings(query);

            var totalCarAds = await this.carAds.Total(query);

            return new SearchCarAdsOutputModel(carAdListings, query.Page, totalCarAds);
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<CarAdDetailsOutputModel>> Details(int id)
        {
            var carAd = await this.carAds.GetDetails(id);

            var message = new CarAdViewedMessage
            {
                CarAdId = carAd.Id
            };

            await this.carAds.Save(message);

            return carAd;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateCarAdOutputModel>> Create(CarAdInputModel input)
        {
            var dealer = await this.dealers.FindByUser(this.currentUser.UserId);

            var category = await this.categories.Find(input.Category);

            if (category == null)
            {
                return BadRequest(Result.Failure("Category does not exist."));
            }

            var manufacturer = await this.manufacturers.FindByName(input.Manufacturer);

            manufacturer ??= new Manufacturer
            {
                Name = input.Manufacturer
            };

            var carAd = new CarAd
            {
                Dealer = dealer,
                Manufacturer = manufacturer,
                Model = input.Model,
                Description = input.Description,
                Category = category,
                ImageUrl = input.ImageUrl,
                PricePerDay = input.PricePerDay,
                Options = new Options
                {
                    HasClimateControl = input.HasClimateControl,
                    NumberOfSeats = input.NumberOfSeats,
                    TransmissionType = input.TransmissionType
                }
            };

            this.carAds.Add(carAd);

            var message = new CarAdCreatedMessage
            {
                CarAdId = carAd.Id,
                Manufacturer = carAd.Manufacturer.Name,
                Model = carAd.Model,
                PricePerDay = carAd.PricePerDay,
                ImageUrl = carAd.ImageUrl
            };

            await this.carAds.Save(message);

            await this.fullTextSearch.Index(new CarAdFullTextSearchModel
            {
                CarAdId = carAd.Id,
                Description = carAd.Description
            });

            return new CreateCarAdOutputModel(carAd.Id);
        }

        [HttpPut]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult> Edit(int id, CarAdInputModel input)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            var category = await this.categories.Find(input.Category);

            var manufacturer = await this.manufacturers.FindByName(input.Manufacturer);

            manufacturer ??= new Manufacturer
            {
                Name = input.Manufacturer
            };

            var carAd = await this.carAds.Find(id);

            carAd.Manufacturer = manufacturer;
            carAd.Model = input.Model;
            carAd.Category = category;
            carAd.ImageUrl = input.ImageUrl;
            carAd.PricePerDay = input.PricePerDay;
            carAd.Options = new Options
            {
                HasClimateControl = input.HasClimateControl,
                NumberOfSeats = input.NumberOfSeats,
                TransmissionType = input.TransmissionType
            };

            var message = new CarAdUpdatedMessage
            {
                CarAdId = carAd.Id,
                Manufacturer = carAd.Manufacturer.Name,
                Model = carAd.Model
            };

            await this.carAds.Save(message);

            return Result.Success;
        }

        [HttpDelete]
        [Authorize]
        [Route(Id)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            return await this.carAds.Delete(id);
        }

        [HttpGet]
        [Authorize]
        [Route(nameof(Mine))]
        public async Task<ActionResult<MineCarAdsOutputModel>> Mine(
            [FromQuery] CarAdsQuery query)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var carAdListings = await this.carAds.Mine(dealerId, query);

            var totalCarAds = await this.carAds.Total(query);

            return new MineCarAdsOutputModel(carAdListings, query.Page, totalCarAds);
        }

        [HttpGet]
        [Route(nameof(Categories))]
        public async Task<IEnumerable<CategoryOutputModel>> Categories()
            => await this.categories.GetAll();

        [HttpPut]
        [Authorize]
        [Route(Id + PathSeparator + nameof(ChangeAvailability))]
        public async Task<ActionResult> ChangeAvailability(int id)
        {
            var dealerId = await this.dealers.GetIdByUser(this.currentUser.UserId);

            var dealerHasCar = await this.dealers.HasCarAd(dealerId, id);

            if (!dealerHasCar)
            {
                return BadRequest(Result.Failure("You cannot edit this car ad."));
            }

            var carAd = await this.carAds.Find(id);

            carAd.IsAvailable = !carAd.IsAvailable;

            await this.carAds.Save();

            return Result.Success;
        }
    }
}
