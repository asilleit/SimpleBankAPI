using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Response;

namespace SimpleBankAPI.Interfaces
{
    public interface IDocumentsDb : IBaseDb<Document>
    {
        Task<Document> Create(Document documentCreate);
        Task<List<Document>> GetDocumentsByUser(int userId);
        Task<Document> GetById(int userId);
    }
}
