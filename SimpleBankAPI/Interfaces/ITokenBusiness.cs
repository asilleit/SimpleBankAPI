using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;

namespace SimpleBankAPI.Interfaces
{
    public interface ITokenBusiness
    {
        Task<(User, string, string, DateTime, DateTime)> Revalidate(int userId, RevalidateRequest revalidateRequest);

    }
}
