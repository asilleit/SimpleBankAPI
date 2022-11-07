namespace SimpleBankAPI.Infrastructure.Kafka
{
    public interface ICommunicationsService
    {
        Task SendCommunication(Communication communication);
    }
}
