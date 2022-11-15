
using SimpleBankAPI.Business;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Infrastructure.Kafka;
namespace SimpleBankAPI.Tests;
public class TransferTests
{
    #region Members
    private Mock<ITransfersRepository> _transfersRepository;
    private Mock<IAccountsRepository> _accountRepository;
    private Mock<ICommunicationsBusiness> _communicationsBusiness;
    private TransfersBusiness _transferBusiness;
    private AccountsBusiness _accountsBusiness;
    private TransferRequest _transferRequest;
    private Transfer _transfer;
    private Account _account1;
    private Account _account2;
    private int _userId = 1;

    #endregion

    #region Constructor
    public TransferTests()
    {
        Setup();
    }

    private void Setup()
    {
        _transfersRepository = new Mock<ITransfersRepository>();
        _accountRepository = new Mock<IAccountsRepository>();
        _communicationsBusiness = new Mock<ICommunicationsBusiness>();
        _transferBusiness = new TransfersBusiness(_transfersRepository.Object, _accountRepository.Object, _communicationsBusiness.Object);

        var _user = new User { Id = 1, Username = "adrianoleite", Password = "123456789", FullName = "adrianofullname", Email = "adriano@gmail.com", CreatedAt = DateTime.Now };
        _account1 = new Account { Id = 1, UserId = 1, Balance = 100, Currency = "EUR", CreatedAt = DateTime.Now };
        _account2 = new Account { Id = 2, UserId = 1, Balance = 100, Currency = "EUR", CreatedAt = DateTime.Now };
        // Transfer and userId
        _transfer = new Transfer { Amount = 1, Fromaccountid = 1, Toaccountid = 2 };

        _transferRequest = new TransferRequest { Amount = 1, FromAccount = 1, ToAccount = 2 };

        _accountRepository.Setup(a => a.GetById(_transfer.Fromaccountid)).ReturnsAsync(_account1);
        _accountRepository.Setup(a => a.GetById(_transfer.Toaccountid)).ReturnsAsync(_account2);
        _accountRepository.Setup(a => a.Create(It.IsAny<Account>())).ReturnsAsync(_account1);

        _accountRepository.Setup(a => a.Create(It.IsAny<Account>())).ReturnsAsync(_account2);
        _transfersRepository.Setup(a => a.Create(It.IsAny<Transfer>())).ReturnsAsync(_transfer);
    }
    #endregion

    #region Setup

    #endregion
    #region Tests
    [Fact]
    public async Task CreateTransfer_TestOK()
    {
        //Act
        var result = await _transferBusiness.Create(_transferRequest, _userId);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.ToString(), "Transfer completed");
    }
    #endregion
    [Fact]
    //"Currency isn't the same"
    public async Task Transfer_TestCurrencyAccountError()
    {
        _accountRepository.Setup(a => a.Create(It.IsAny<Account>())).Throws(new ArgumentException());
        // Arrange
        _account2.Currency = "USD";
        // Act
        var result = _transferBusiness.Create(_transferRequest, _userId);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("One or more errors occurred. (Currency isn't the same)", result.Exception.Message.ToString());
    }
    [Fact]
    //Insufficient funds from your account
    public async Task Transfer_TestBalanceAccountError()
    {
        _transfersRepository.Setup(a => a.Create(It.IsAny<Transfer>())).Throws(new ArgumentException());
        // Arrange
        _account1.Balance = _transfer.Amount - 1000;
        // Act
        var result = _transferBusiness.Create(_transferRequest, _userId);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("One or more errors occurred. (Insufficient funds from your account)", result.Exception.Message.ToString());
    }

    [Fact]
    //User don't owner account
    public async Task Transfer_TestUserError()
    {
        _transfersRepository.Setup(a => a.Create(It.IsAny<Transfer>())).Throws(new ArgumentException());
        // Arrange
      _account1.UserId=10;
        // Act
        var result = _transferBusiness.Create(_transferRequest, _userId);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("One or more errors occurred. (User don't owner account)", result.Exception.Message.ToString());
    }
    [Fact]
    //The accounts are the same.
    public async Task Transfer_TestSameAccountsError()
    {
        _transfersRepository.Setup(a => a.Create(It.IsAny<Transfer>())).Throws(new ArgumentException());
        // Arrange
        _transferRequest.FromAccount = _transferRequest.ToAccount;
        // Act
        var result = _transferBusiness.Create(_transferRequest, _userId);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("One or more errors occurred. (Accounts not valid, because is the same)", result.Exception.Message.ToString(), true);
    }
}
