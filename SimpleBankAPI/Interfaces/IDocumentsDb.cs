using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IDocumentsDb : IBaseDb<Document>
    {
        Task<Document> Create(Document documentCreate);
        Task<List<Document>> GetDocumentsByAccount(int accountId);
        Task<Document> GetById(int userId);
    }
}
