namespace SignalRCommon
{
    using System.Threading.Tasks;

    public interface IWeatherServer
    {
        WeatherForecast GetWeather();

        Task SaveWeather(WeatherForecast weather);
    }
}
