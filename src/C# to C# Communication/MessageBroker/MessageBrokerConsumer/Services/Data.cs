namespace MessageBrokerConsumer.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using MessageBrokerCommon;

    public class Data : IData
    {
        private static readonly List<WeatherForecast> weatherData = new List<WeatherForecast>();

        public void Add(WeatherForecast weather) => weatherData.Add(weather);

        public List<WeatherForecast> Get() => weatherData.ToList();
    }
}
