using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IDocumentsBusiness
    {
        Task<DocumentResponse> Create(IFormFile file, int accountId, int userId);
        Task<List<Document>> GetDocumentsByUser(int userId);
        Task<Document> GetById(int documentId);
    }
}
