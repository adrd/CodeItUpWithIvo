namespace MessageBrokerConsumer.Messaging
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using MessageBrokerCommon;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using Services;

    public class WeatherReceiver : BackgroundService
    {
        private readonly IData data;
        private readonly RabbitMqConfiguration rabbitMq;
        private readonly IModel channel;
        private readonly IConnection connection;

        public WeatherReceiver(IData data, IOptions<RabbitMqConfiguration> rabbitMq)
        {
            this.data = data;
            this.rabbitMq = rabbitMq.Value;

            var factory = new ConnectionFactory
            {
                HostName = this.rabbitMq.Hostname,
            };

            this.connection = factory.CreateConnection();

            this.channel = this.connection.CreateModel();

            this.channel.QueueDeclare(this.rabbitMq.QueueName, exclusive: false, autoDelete: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(this.channel);

            consumer.Received += (ch, ea) =>
            {
                var weather = JsonSerializer.Deserialize<WeatherForecast>(ea.Body.ToArray());

                this.data.Add(weather);

                this.channel.BasicAck(ea.DeliveryTag, false);
            };

            this.channel.BasicConsume(this.rabbitMq.QueueName, false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            this.connection.Dispose();
            this.channel.Dispose();
            base.Dispose();
        }
    }
}
