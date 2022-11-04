using SimpleBankAPI.Models;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public interface INotificationsBusiness
    {
        Task SendNotification(Notification notification);
        Task TransferNotification(Transfer transfer);
    }
}
