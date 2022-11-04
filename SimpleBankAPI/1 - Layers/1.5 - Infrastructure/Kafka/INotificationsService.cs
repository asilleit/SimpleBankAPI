namespace SimpleBankAPI.Infrastructure.Kafka
{
    public interface INotificationsService
    {
        Task SendNotification(Notification notification);
    }
}
