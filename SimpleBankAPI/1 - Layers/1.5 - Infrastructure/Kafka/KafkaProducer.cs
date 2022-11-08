using Confluent.Kafka;
using System.Text.Json;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class KafkaProducer : IEventProducer
    {
        private readonly IProducer<int, string> _producerBuilder;
        private readonly string _topic;

        public KafkaProducer(IConfiguration configuration)
        {
            var producer = configuration.GetSection("KafkaProducer").GetChildren().ToDictionary(x => x.Key, x => x.Value).ToList();

            if (producer.Any()){
                var producerConfig = new ProducerConfig()
                {
                    BootstrapServers = producer.FirstOrDefault(x => x.Key.Equals("BootstrapServers")).Value
                };
                _producerBuilder = new ProducerBuilder<int, string>(producerConfig).Build();

                _topic = producer.FirstOrDefault(x => x.Key.Equals("TopicName")).Value;
            }
        }
        public async Task PublishEvent(Communication communication)
        {
            var message = new Message<int, string>
            {
                Key = communication.User.Id,
                Value = JsonSerializer.Serialize(communication)
            };

            await Publish(message);
        }
        private async Task<(DeliveryResult<int, string>, string)> Publish(Message<int, string> message)
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
