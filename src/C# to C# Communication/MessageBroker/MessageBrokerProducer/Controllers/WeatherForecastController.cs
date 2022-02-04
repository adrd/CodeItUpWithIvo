namespace MessageBrokerProducer.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using MessageBrokerCommon;
    using Messaging;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> Data = new List<WeatherForecast>
        {
            new WeatherForecast { Summary = "Cold" }
        };

        private readonly IWeatherPublisher weatherPublisher;

        public WeatherForecastController(IWeatherPublisher weatherPublisher) 
            => this.weatherPublisher = weatherPublisher;

        [HttpGet]
        public IActionResult Get(string weather)
        {
            if (string.IsNullOrEmpty(weather))
            {
                return this.BadRequest();
            }

            var fullWeather = new WeatherForecast { Summary = weather };

            Data.Add(fullWeather);

            this.weatherPublisher.Publish(fullWeather);

            return this.Ok();
        }
    }
}
