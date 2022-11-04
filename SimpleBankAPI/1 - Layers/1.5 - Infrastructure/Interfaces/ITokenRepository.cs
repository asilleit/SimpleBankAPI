using Token = SimpleBankAPI.Models.Token;

namespace SimpleBankAPI.Interfaces
{
    public interface ITokenRepository : IBaseRepository<Token>
    {
        Task<Token> Create(Token Token);
        Task<Token> GetTokensByRefreshToken(string refreshToken);
        //Task<Token> GetByToken(string token);
    }
}
