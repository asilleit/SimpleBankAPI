namespace SimpleBankAPI.Infrastructure.Kafka
{
    public interface IEventProducer
    {
        Task PublishEvent(Notification notification);
    }
}
