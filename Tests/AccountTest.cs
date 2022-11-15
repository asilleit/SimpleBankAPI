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
    private IAccountsBusiness _accountsBusiness;
    private Mock<IAccountsRepository> _accountsDb;
    private int _userId;
    private AccountRequest _accountRequest;
    private int _accountId;
    private Account _account;
    #endregion

    #region Constructor
    public AccountTest()
    {
        _accountsDb = new Mock<IAccountsRepository>();
        _accountsBusiness = new AccountsBusiness( _accountsDb.Object);

        Setup();
    }
    #endregion

    #region Setup
    private void Setup()
    {

        User _user = new User{Id = 1, Username="adrianoleite", Password="123456789", FullName="adrianofullname", Email="adriano@gmail.com", CreatedAt = DateTime.Now};
        _account = new Account { Id = 1, UserId = 1, Balance = 1, Currency = "EUR", CreatedAt = DateTime.Now, User = _user };

        _userId = 1;
        _accountRequest = new AccountRequest()
        {
            Amount = 1,
            Currency = "EUR"
        };
        _accountId = 1;
        _accountsDb.Setup(r => r.Create(_account));
        _accountsDb.Setup(r => r.GetById(_accountId));
    }
    #endregion

    #region Tests
    [Fact]
    public async Task CreateAccount_TestOK()
    {
        // Arrange
        _accountsDb.Setup(a => a.Create(It.IsAny<Account>())).ReturnsAsync(_account);
        // Act
        var result = _accountsBusiness.Create(_accountRequest, _userId);
        // Assert
        //Assert.True(result.GetType() == typeof(AccountResponse));
        Assert.NotNull(result);

    }

    [Fact]
    public async Task CreateAccount_TestError()
    {
        // Arrange
        _accountsDb.Setup(a => a.Create(It.IsAny<Account>()).Result).Throws(new ArgumentException());
        //Act
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
        var userId = 1;
        // Act
        var result = _accountsBusiness.GetAccountsByUser(userId);
        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async void GetAllAccounts_TestError()
    {
        // Arrange
        _accountsDb.Setup(r => r.GetAccountsByUser(4)).ReturnsAsync(new List<Account>());
        _accountsBusiness = new AccountsBusiness(_accountsDb.Object);
        // Act
        var result = _accountsBusiness.GetAccountsByUser(4);
        // Assert
        Assert.NotNull(result);  
        Assert.ThrowsAsync<Exception>(() => result);
        }

    #endregion
}