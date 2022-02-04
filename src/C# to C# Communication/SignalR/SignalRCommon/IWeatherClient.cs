namespace SignalRCommon
{
    using System.Threading.Tasks;

    public interface IWeatherClient
    {
        Task ReceiveError(string error);

        Task ReceiveData(WeatherForecast data);
    }
}
