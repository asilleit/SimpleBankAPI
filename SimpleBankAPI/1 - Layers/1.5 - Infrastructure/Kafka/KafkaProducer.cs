using Confluent.Kafka;
using System.Configuration;
using System.Text.Json;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class KafkaProducer : IEventProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IProducer<int, string> _producerBuilder;
        private readonly string _topic;

        public KafkaProducer(IConfiguration configuration)
        {
            _configuration = configuration;

            var producer = _configuration.GetSection("Kafka:Producer").GetChildren().ToDictionary(x => x.Key, x => x.Value).ToList();
            if (producer != null && producer.Count() > 0)
            {
                var producerConfig = new ProducerConfig()
                {
                    BootstrapServers = producer.FirstOrDefault(x => x.Key.Equals("BootstrapServers")).Value
                };
                _producerBuilder = new ProducerBuilder<int, string>(producerConfig).Build();
            }
            _topic = producer.FirstOrDefault(x => x.Key.Equals("TopicName")).Value;
        }
        public async Task PublishEvent(Notification notification)
        {
            var key = notification.User.Id;
            var data = JsonSerializer.Serialize(notification);

            var message = new Message<int, string>
            {
                Key = key,
                Value = data
            };

            await Publish(message);
        }
        public async Task<(DeliveryResult<int, string>, string)> Publish(Message<int, string> message)
        {
            try
            {
                return (await _producerBuilder.ProduceAsync(_topic, message), string.Empty);
            }
            catch (ProduceException<int, string> ex)
            {
                return (null, ex.Error.Reason);
            }
        }

    }
}
