using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Data
{
    public class DocumentsDb : BaseDb<Document>, IDocumentsDb, IBaseDb<Document>
    {
        public DocumentsDb(DbContextOptions<postgresContext> options) : base(options)
        {
        }

        public async override Task<Document> Create(Document document)
        {
            await _db.AddAsync(document);
            await _db.SaveChangesAsync();
            return document;
        }

        public async Task<Document> GetById(int documentId)
        {
            return await _db.Documents.FirstOrDefaultAsync(a => a.Id == documentId);
        }

        public Task<List<Document>> GetDocumentsByAccount(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
