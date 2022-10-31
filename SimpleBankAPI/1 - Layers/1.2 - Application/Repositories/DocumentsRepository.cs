using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Application.Repositories
{
    public class DocumentsRepository : BaseRepository<Document>, IDocumentsRepository, IBaseRepository<Document>
    {
        public DocumentsRepository(DbContextOptions<postgresContext> options) : base(options)
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

        public async Task<FileStreamResult> GetDocument(Document document)
        {
            //try
            //{
                var stream = System.IO.File.OpenRead(document.FileName);
                return new FileStreamResult(stream, document.File.ToString());
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
    }
}
