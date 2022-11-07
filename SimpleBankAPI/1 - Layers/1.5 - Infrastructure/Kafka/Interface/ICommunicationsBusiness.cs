using SimpleBankAPI.Models;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public interface ICommunicationsBusiness
    {
        Task SendCommunication(Communication communication);
        Task TransferNotification(Transfer transfer);
    }
}
