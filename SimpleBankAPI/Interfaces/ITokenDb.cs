using NuGet.Common;
using SimpleBankAPI.Data;
using SimpleBankAPI.Models;
using Token = SimpleBankAPI.Models.Token;

namespace SimpleBankAPI.Interfaces
{
    public interface ITokenDb : IBaseDb<Token>
    {
        Task<Token> Create(Token Token);
        Task<Token> GetTokensByRefreshToken(string refreshToken);
        //Task<Token> GetByToken(string token);

    }
}
