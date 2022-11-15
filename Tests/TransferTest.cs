using SimpleBankAPI.Application.Repositories;
using SimpleBankAPI.Business;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
namespace SimpleBankAPI.Tests;
public class TransferTests
{
   #region Members
   private Mock<ITransfersRepository> _transfersRepository;
    private Mock<IAccountsRepository> _accountRepository;
   private ITransfersBusiness _transferBusiness;
   private IAccountsBusiness _accountsBusiness;
   private TransferRequest _transferRequest;
   private Transfer _transfer;
   private Account _account1;
   private Account _account2;
   private int userId = 1;

   #endregion

   #region Constructor
   public TransferTests()
    {
        _transfersRepository = new Mock<ITransfersRepository>();
        _accountRepository = new Mock<IAccountsRepository>();
        _transferBusiness = new TransfersBusiness(_transfersRepository.Object, _accountRepository.Object);
        _accountRepository = new Mock<IAccountsRepository>();
        _accountsBusiness = new AccountsBusiness( _accountRepository.Object);
        Setup();
    }

    private void Setup()
    {
      _transfersRepository = new Mock<ITransfersRepository>();

      _transfer = new Transfer { Id = 1, Fromaccountid = 1, Toaccountid = 2, Amount = 100, CreatedAt = DateTime.Now };
      User _user = new User{Id = 1, Username="adrianoleite", Password="123456789", FullName="adrianofullname", Email="adriano@gmail.com", CreatedAt = DateTime.Now};
      _account1 = new Account { Id = 26, UserId = 6, Balance = 1, Currency = "EUR", CreatedAt = DateTime.Now, User = _user };
      _account2 = new Account { Id = 28, UserId = 6, Balance = 1, Currency = "EUR", CreatedAt = DateTime.Now, User = _user };


      _accountRepository.Setup(r => r.Create(_account1));
      _accountRepository.Setup(r => r.GetById(_account1.Id));
      _accountRepository.Setup(r => r.Create(_account2));
      _accountRepository.Setup(r => r.GetById(_account2.Id));

      // _accountRepository.Setup(r => r.Update(It.IsAny<Account>())).Returns(() => (true));
      // _accountRepository.Setup(r => r.Update(It.IsAny<Account>())).Returns(() => (true));

      _transferRequest = new TransferRequest { Amount = 1, FromAccount = 26, ToAccount = 28};


    }
    #endregion

    #region Setup

   #endregion
   #region Tests
   [Fact]
   public async Task CreateTransfer_TestOK()
   {
      //Act  
      _transferBusiness = new TransfersBusiness(_transfersRepository.Object, _accountRepository.Object);      
      // Act
      var result1 = await _transferBusiness.Create(_transferRequest,6);
      // Assert
      Assert.Equal(result1, "Transfer completed successfully");
   }
   #endregion
}
