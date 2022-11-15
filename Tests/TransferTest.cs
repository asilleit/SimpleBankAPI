using Moq;
using SimpleBankAPI.Business;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;

namespace simplebankapi.tests;

public class transfertest
{
   #region members

    private readonly ITransfersBusiness _transferUseCase;
    private readonly Mock<ITransfersRepository> _unitOfWork;

    #endregion
    private Transfer _transfer;
    private AccountRequest _accountRequest;
    private Account _account1;
    private Account _account2;
        private int _userId;
        private int _accountId;


   private void Setup()
   {
       _transfer = new Transfer { Id = 1, Fromaccountid = 1, Toaccountid = 2, Amount = 100, CreatedAt = DateTime.Now };

       _account1 = new Account { Id = 1, UserId = 1, Balance = 900, Currency = "EUR", CreatedAt = DateTime.Now };
       _account2 = new Account { Id = 2, UserId = 2, Balance = 90, Currency = "EUR", CreatedAt = DateTime.Now };
        _userId = 1;
        _accountRequest = new AccountRequest()
        {
            Amount = 1,
            Currency = "EUR"
        };
        _accountId = 1;

        _unitOfWork.Setup(r => r.GetById(_account1.Id)).Returns(() => (_account1));
        _unitOfWork.Setup(r => r.GetById(_account2.Id)).Returns(() =>  (_account2));

       _unitOfWork.Setup(r => r.GetById( _account1.Id)).Returns(() => (_account1));
       _unitOfWork.Setup(r => r.GetById(_account2.Id)).Returns(() => (_account2));

       //_unitOfWork.Setup(r => r.Update(It.IsAny<Transfer>())).ReturnsAsync(() => (true));
       //_unitOfWork.Setup(r => r.Update(It.IsAny<Transfer>())).ReturnsAsync(() => (true));

   }
   // #region Tests
   // [Fact]
   // public async Task Transfer_TestOK()
   // {
   //     // Arrange
   //     // Act
   //     var result = await _transferUseCase.Create(_transfer, _userId);
   //     // Assert
   //     Assert.Null(result.Item1);
   // }
   // #endregion
}