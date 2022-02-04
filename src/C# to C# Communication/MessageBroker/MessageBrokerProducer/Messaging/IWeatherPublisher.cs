namespace MessageBrokerProducer.Messaging
{
    using MessageBrokerCommon;

    public interface IWeatherPublisher
    {
        void Publish(WeatherForecast weather);
    }
}
