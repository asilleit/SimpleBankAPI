using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Data
{
    public class TokenDb : BaseDb<Token>, ITokenDb, IBaseDb<Token>
    {
        public TokenDb(DbContextOptions<postgresContext> options) : base(options)
        {
        }


        public async override Task<Token> Create(Token token)
        {
            await _db.AddAsync(token);
            await _db.SaveChangesAsync();
            return token;
        }

        public async Task<Token> GetTokensByRefreshToken(string refreshToken)
        {
            return await _db.Tokens.FirstOrDefaultAsync(a => a.Refresh_token == refreshToken);
        }

    }
}
