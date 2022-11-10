//using SimpleBankAPI.Interfaces;
//using SimpleBankAPI.Models;

//namespace SimpleBankAPI.Tests;

//public class TransferTest
//{
//    #region Members

//    private readonly ITransfersBusiness _transfersBusiness;
//    private readonly Mock<IUnitOfWork> _unitOfWork;

//    private Transfer _transfer;
//    private Account _account1;
//    private Account _account2;



//    private void Setup()
//    {
//        _transfer = new Transfer { Id = 1, Fromaccountid = 1, Toaccountid = 2, Amount = 100,  = 1, CreatedAt = DateTime.Now };

//        _account1 = new Account { Id = 1, UserId = 1, Balance = 900, Currency = "EUR", CreatedAt = DateTime.Now };
//        _account2 = new Account { Id = 2, UserId = 2, Balance = 90, Currency = "EUR", CreatedAt = DateTime.Now };

//        var userId = 1;
//        _unitOfWork.Setup(r => r.AccountRepository.Create(_account1)).Returns(CreateMockOK(_account1));
//        _unitOfWork.Setup(r => r.AccountRepository.Create(_account2)).Returns(CreateMockOK(_account2));

//        _unitOfWork.Setup(r => r.AccountRepository.ReadById(_account1.UserId, _account1.Id)).ReturnsAsync(() => (_account1));
//        _unitOfWork.Setup(r => r.AccountRepository.ReadById(_account2.Id)).ReturnsAsync(() => (_account2));

//        _unitOfWork.Setup(r => r.AccountRepository.Update(It.IsAny<Account>())).ReturnsAsync(() => (true));
//        _unitOfWork.Setup(r => r.AccountRepository.Update(It.IsAny<Account>())).ReturnsAsync(() => (true));

//    }
//    #region Tests
//    [Fact]
//    public async Task Transfer_TestOK()
//    {
//        // Arrange
//        // Act
//        var result = await _transferUseCase.Transfer(_transfer);
//        // Assert
//        Assert.Null(result.Item1);
//    }
//    #endregion
//}