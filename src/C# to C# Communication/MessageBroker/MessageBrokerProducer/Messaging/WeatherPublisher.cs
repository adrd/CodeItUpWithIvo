namespace MessageBrokerProducer.Messaging
{
    using System;
    using System.Text;
    using System.Text.Json;
    using MessageBrokerCommon;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    public class WeatherPublisher : IWeatherPublisher
    {
        private readonly RabbitMqConfiguration rabbitMq;

        public WeatherPublisher(IOptions<RabbitMqConfiguration> rabbitMq) => this.rabbitMq = rabbitMq.Value;

        public void Publish(WeatherForecast weather)
        {
            var body = JsonSerializer.SerializeToUtf8Bytes(weather);

            var factory = new ConnectionFactory
            {
                HostName = this.rabbitMq.Hostname
            };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(this.rabbitMq.QueueName, exclusive: false, autoDelete: false);

            channel.BasicPublish(string.Empty, this.rabbitMq.QueueName, null, body);
        }
    }
}
