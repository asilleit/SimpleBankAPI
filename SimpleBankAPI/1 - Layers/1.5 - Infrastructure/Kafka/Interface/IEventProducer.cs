namespace SimpleBankAPI.Infrastructure.Kafka
{
    public interface IEventProducer
    {
        Task PublishEvent(Communication communication);
    }
}
