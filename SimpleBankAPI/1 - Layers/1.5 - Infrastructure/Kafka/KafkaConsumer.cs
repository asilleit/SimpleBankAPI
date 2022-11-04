using Confluent.Kafka;
using System.Text.Json;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly string _topic;
        private readonly IConsumer<string, string> kafkaConsumer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;


        public KafkaConsumer(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            var consumerConfig = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = _configuration["Kafka:Consumer:BootStrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest,

            };
            _topic = _configuration["Kafka:Consumer:TopicName"];
            kafkaConsumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();

            return Task.CompletedTask;
        }

        private async void StartConsumerLoop(CancellationToken cancellationToken)
        {
            kafkaConsumer.Subscribe(_topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var record = kafkaConsumer.Consume(cancellationToken);

                    // Handle message...
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _notificationsBusiness = scope.ServiceProvider.GetService<INotificationsBusiness>();

                        var notification = JsonSerializer.Deserialize<Notification>(record.Message.Value);
                        await _notificationsBusiness.SendNotification(notification);
                    }
                }
                catch (OperationCanceledException e)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
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
            kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            kafkaConsumer.Dispose();

            base.Dispose();
        }
    }
}
