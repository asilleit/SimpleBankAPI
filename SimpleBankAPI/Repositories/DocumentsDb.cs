using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Data
{
    public class DocumentsDb : BaseRepository<Document>, IDocumentsRepository, IBaseRepository<Document>
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

        public async Task<List<Document>> GetDocumentsByAccount(int accountId)
        {
            return await _db.Documents.Where(a => a.AccountId == accountId).ToListAsync();
        }
    }
}
