using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IDocumentsBusiness
    {
        Task<string> Create(IFormFile file, int accountId, int userId);
        Task<List<Document>> GetDocumentsByAccount(int userId);
        Task<Document> GetById(int documentId);
    }
}
