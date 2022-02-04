namespace MessageBrokerConsumer.Services
{
    using System.Collections.Generic;
    using MessageBrokerCommon;

    public interface IData
    {
        void Add(WeatherForecast weather);

        List<WeatherForecast> Get();
    }
}
