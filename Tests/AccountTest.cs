using SimpleBankAPI.Business;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using Moq;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Tests;

public class AccountTest
{
    #region Members
    private AccountsBusiness _accountsBusiness;
    private Mock<IAccountsRepository> _accountsDb;
    private int _userId;
    private AccountRequest _accountRequest;
    private Account _account;
    private int _accountId;
    #endregion

    #region Constructor
    public AccountTest()
    {


        Setup();
    }
    #endregion

    #region Setup
    private void Setup()
    {
        var _user = new User{Id = 1, Username="adrianoleite", Password="123456789", FullName="adrianofullname", Email="adriano@gmail.com", CreatedAt = DateTime.Now};
        _account = new Account { Id = 1, UserId = 1, Balance = 1, Currency = "EUR", CreatedAt = DateTime.Now };

        _userId = 1;
        _accountRequest = new AccountRequest()
        {
            Amount = 1,
            Currency = "EUR"
        };
        _accountId = 1;
        _accountsDb = new Mock<IAccountsRepository>();

        _accountsDb.Setup(a => a.Create(It.IsAny<Account>())).ReturnsAsync(new Account());
        _accountsDb.Setup(r => r.GetById(_userId)).ReturnsAsync(() => _account);
       // _accountsDb.Setup(r => r.GetById(_userId)).Returns(ReadByAccountMockOk());

        _accountsBusiness = new AccountsBusiness( _accountsDb.Object);
    }


     private async Task<IEnumerable<Account>?> ReadByAccountMockOk()
    {
        var accounts = new List<Account>();
        accounts.Add(_account);
        return accounts;
    }
    #endregion

    #region Tests
    [Fact]
    public async Task CreateAccount_TestOK()
    {
        // Arrange
        _accountsDb.Setup(a => a.Create(It.IsAny<Account>())).ReturnsAsync(_account);
        // Act
        var result = await _accountsBusiness.Create(_accountRequest, _userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_account.Id , result.Id);
        Assert.Equal(_account.Currency , result.Currency);
        Assert.Equal(_account.Balance.ToString() , result.Balance.ToString());
    }

    [Fact]
    public async Task CreateAccount_TestError()
    {
        // Arrange
        _accountsDb.Setup(a => a.Create(It.IsAny<Account>())).Throws(new ArgumentException());
        var result = _accountsBusiness.GetById(99999);
        // Assert
        Assert.NotNull(result);
        Assert.ThrowsAsync<Exception>(() => result);
        Assert.Equal("One or more errors occurred. (Account not found)", result.Exception.Message.ToString());
    }

    [Fact]
    public async Task GetAllAccounts_TestOK()
    {
        // Arrange
        _accountsDb.Setup(a => a.GetAccountsByUser(_userId)).ReturnsAsync(new List<Account>());
        const int userId = 1;
        // Act
        var result = await _accountsBusiness.GetAccountsByUser(userId);
        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async void GetAllAccounts_TestError()
    {
        // Arrange
        _accountsDb.Setup(a => a.GetAccountsByUser(_userId)).ReturnsAsync(new List<Account>());
        // Act
        const int userId = 4;
        var result = _accountsBusiness.GetAccountsByUser(userId);
        // Assert
        Assert.NotEqual(_accountsDb.GetType, result.GetType);
    }

    [Fact]
    public async Task GetAccountById_TestOK()
    {
        // Arrange
        _accountsDb.Setup(a => a.GetById(_accountId)).ReturnsAsync(_account);
        var result = await _accountsBusiness.GetById(_accountId);
        // Act

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_account.Id , result.Id);
        Assert.Equal(_account.Currency , result.Currency);
        Assert.Equal(_account.Balance.ToString() , result.Balance.ToString());
    }

        [Fact]
    public async Task GetAccountById_TestError()
    {
        // Arrange
        var id = 1;
        var userId = 1;
        _accountsDb.Setup(r => r.GetById( id)).Throws(new ArgumentException());
        // Act
        var result = await _accountsBusiness.GetAccountsByUser(userId);
        // Assert
        Assert.Null(result);
    }
    #endregion
}