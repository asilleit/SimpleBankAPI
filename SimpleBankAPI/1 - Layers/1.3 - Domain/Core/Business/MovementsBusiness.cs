using SimpleBankAPI.Interfaces;
using System.Security.Authentication;
using SimpleBankAPI.Models;
using SimpleBankAPI.Application.Repositories;

namespace SimpleBankAPI.Business
{
    public class MovementsBusiness : IMovementsBusiness
    {
        protected IMovementsRepository _movementsDb;
        protected ITransfersRepository _transfersDb;
        protected IAccountsRepository _accountsDb;
        public MovementsBusiness(IMovementsRepository movementsDb, IAccountsRepository accountsDb, ITransfersRepository transfersDb)
        {
            _movementsDb = movementsDb;
            _accountsDb = accountsDb;
            _transfersDb = transfersDb;
        }
        public async Task<Movement> Create(int accountId, decimal amount)
        {
            var movement = new Movement
            {
                Accountid = accountId,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
            };
            await _movementsDb.Create(movement);

            //Change Account Balance

            var accountToUpdate = await _accountsDb.GetById(accountId);
            accountToUpdate.Balance += amount;
            await _accountsDb.Update(accountToUpdate);

            return movement;
        }
    }
}
