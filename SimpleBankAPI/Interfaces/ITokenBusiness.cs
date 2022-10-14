using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface ITokenBusiness
    {
        Task<(User, string, string, DateTime, DateTime)> Revalidate(int userId, RevalidateRequest revalidateRequest);

    }
}
