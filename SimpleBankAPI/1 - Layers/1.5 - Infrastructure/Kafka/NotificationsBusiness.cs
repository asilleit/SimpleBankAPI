using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class NotificationsBusiness : INotificationsBusiness
    {
        private INotificationsService _notificationsService;
        private IUsersRepository _usersRepository;
        private IAccountsRepository _accountsDb;
        private IEventProducer _eventProducer;
        public NotificationsBusiness(INotificationsService notificationsService, IUsersRepository usersDb, IAccountsRepository accountsDb, IEventProducer eventProducer)
        {
            _notificationsService = notificationsService;
            _usersRepository = usersDb;
            _accountsDb = accountsDb;
            _eventProducer = eventProducer;
        }
        public async Task TransferNotification(Transfer transfer)
        {
            var account1 = await _accountsDb.GetById(transfer.Fromaccountid);
            var user1 = await _usersRepository.GetById(account1.UserId);
            var account2 = await _accountsDb.GetById(transfer.Toaccountid);
            var user2 = await _usersRepository.GetById(account2.UserId);

            var debitNotification = new Notification
            {
                User = user1,
                Subject = "Transfer from account",
                Body = $"Hello {user1.FullName}, <br /><br /> {transfer.Amount} {account1.Currency} were transfered from your account nr. {account1.Id} to {user2.FullName}, your current balance is: {account1.Balance} {account1.Currency}"
            };
            await _eventProducer.PublishEvent(debitNotification);
            if (user1.Id != user2.Id)
            {
                var creditNotification = new Notification
                {
                    User = user2,
                    Subject = "Transfer to account",
                    Body = $"Hello {user1.FullName}, <br /><br /> {transfer.Amount} {account2.Currency} were transfered to your account nr. {account2.Id} from {user1.FullName}, your current balance is: {account2.Balance} {account2.Currency}"
                };
                await _eventProducer.PublishEvent(creditNotification);
            }
        }
        public async Task SendNotification(Notification notification)
        {
            if (string.IsNullOrEmpty(notification.User.Email)) return;

            await _notificationsService.SendNotification(notification);
        }

    }
}
