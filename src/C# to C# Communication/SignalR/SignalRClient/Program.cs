namespace SignalRClient
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using SignalRCommon;

    public class Program
    {
        public static async Task Main()
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/Weather")
                .WithAutomaticReconnect()
                .Build();

            await connection.StartAsync();

            // Receiving messages
            connection.On<string>(nameof(IWeatherClient.ReceiveError), Console.WriteLine);

            connection.On<WeatherForecast>(nameof(IWeatherClient.ReceiveData), weatherReply =>
            {
                Console.WriteLine(weatherReply.Summary);
            });

            // Sending message to the server
            await connection.InvokeAsync(
                nameof(IWeatherServer.SaveWeather),
                new WeatherForecast());

            var weather = new WeatherForecast { Summary = "Hot" };
            await connection.InvokeAsync(
                nameof(IWeatherServer.SaveWeather),
                weather);

            var result = await connection.InvokeAsync<WeatherForecast>(nameof(IWeatherServer.GetWeather));

            Console.WriteLine(result.Summary);

            Console.ReadKey();
        }
    }
}
