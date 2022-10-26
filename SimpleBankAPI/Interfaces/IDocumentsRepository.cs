using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IDocumentsRepository : IBaseRepository<Document>
    {
        Task<Document> Create(Document documentCreate);
        Task<List<Document>> GetDocumentsByAccount(int accountId);
        Task<Document> GetById(int userId);
    }
}
