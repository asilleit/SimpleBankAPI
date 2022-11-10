//using Moq;
//using SimpleBankAPI.Interfaces;
//using SimpleBankAPI.Models;

//namespace SimpleBankAPI.Tests;

//public class AccountTest
//{
//    #region Members
//    private readonly IAccountsBusiness _accountsBusiness;
//    private readonly Mock<IUnitOfWork> _unitOfWork;

//    private Account _account;
//    #endregion

//    #region Constructor
//    public AccountTest()
//    {
//        var userRepositoryMock = new Mock<IUsersRepository>();
//        var accountRepositoryMock = new Mock<IAccountsRepository>();

//        _unitOfWork = new Mock<IUnitOfWork>();
//        //_unitOfWork.Setup(r => r.AccountRepository).Returns(accountRepositoryMock.Object);//not necessary
//        _accountsBusiness = new Account();
//            new Account(_logger.Object, _unitOfWork.Object);

//        Setup();
//    }

//    private void Setup()
//    {
//        _account = new Account { Id = 1, UserId = 1, Balance = 1000, Currency = "EUR", CreatedAt = DateTime.Now };


//        _unitOfWork.Setup(r => r.AccountRepository.Create(_account)).ReturnsAsync(() => (true, _account.Id));

//        var id = 1;
//        var userId = 1;
//        _unitOfWork.Setup(r => r.AccountRepository.ReadById(userId, id)).ReturnsAsync(() => _account);
//    }
//}
