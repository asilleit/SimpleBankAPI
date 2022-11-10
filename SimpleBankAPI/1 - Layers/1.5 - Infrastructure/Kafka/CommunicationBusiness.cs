using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Infrastructure.Kafka
{
    public class NotificationsBusiness : ICommunicationsBusiness
    {
        private ICommunicationsService _communicationsService;
        private IUsersRepository _usersRepository;
        private IAccountsRepository _accountsDb;
        private IEventProducer _eventProducer;
        public NotificationsBusiness(ICommunicationsService communicationsService, IUsersRepository usersDb, IAccountsRepository accountsDb, IEventProducer eventProducer)
        {
            _communicationsService = communicationsService;
            _usersRepository = usersDb;
            _accountsDb = accountsDb;
            _eventProducer = eventProducer;
        }
        public async Task TransferCommunication(Transfer transfer)
        {
            //Get accounts and users
            var account1 = await _accountsDb.GetById(transfer.Fromaccountid);
            var user1 = await _usersRepository.GetById(account1.UserId);
            var account2 = await _accountsDb.GetById(transfer.Toaccountid);
            var user2 = await _usersRepository.GetById(account2.UserId);

            var debitCommunication = new Communication
            {
                User = user1,
                Subject = "Transfer from account",
                Body = $"Hi {user1.FullName}, <br /> " +
                    $"The amount of {transfer.Amount} {account1.Currency} has been transferred to your account number {account1.Id}. <br /> <br /> " +
                    $"The transfer was made by {user2.FullName}." +
                    $"Your current balance is: {account1.Balance} {account1.Currency}, <br/><br/> " +
                    $"<H3>Thanks, <br />Simple Bank API</H3>"
            };
            await _eventProducer.PublishEvent(debitCommunication);

            if (!user1.Id.Equals(user2.Id))
            {
                var creditCommunication = new Communication
                {
                    User = user2,
                    Subject = "Transfer to account",
                    Body = $"Hi {user2.FullName}, <br />, the amount of {transfer.Amount} {account2.Currency} has been transferred to your account number {account2.Id}. <br /> <br /> " +
                    $"The transfer was made by {user1.FullName}." +
                    $"Your current balance is: {account2.Balance} {account2.Currency}, <br/><br/> " +
                    $"<H3>Thanks,<br />Simple Bank API</H3>"
                };
                await _eventProducer.PublishEvent(creditCommunication);
            }
        }
        public async Task SendCommunication(Communication communication)
        {
            await _communicationsService.SendCommunication(communication);
        }

    }
}
