using Confluent.Kafka;
using System.Text.Json;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly string _topic;
        private readonly IConsumer<string, string> _kafkaConsumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaConsumer(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            var consumerConfig = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = configuration["KafkaConsumer:BootStrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest,

            };
            _topic = configuration["KafkaConsumer:TopicName"];
            _kafkaConsumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();

            return Task.CompletedTask;
        }

        private async void StartConsumerLoop(CancellationToken cancellationToken)
        {
            _kafkaConsumer.Subscribe(_topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var record = _kafkaConsumer.Consume(cancellationToken);

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _communicationsBusiness = scope.ServiceProvider.GetService<ICommunicationsBusiness>();

                        var communication = JsonSerializer.Deserialize<Communication>(record.Message.Value);
                        await _communicationsBusiness.SendCommunication(communication);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }
        }

        public override void Dispose()
        {
            _kafkaConsumer.Close();
            _kafkaConsumer.Dispose();

            base.Dispose();
        }
    }
}
