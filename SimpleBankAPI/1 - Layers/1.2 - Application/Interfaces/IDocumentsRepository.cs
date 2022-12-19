using Microsoft.AspNetCore.Mvc;
using SimpleBankAPI.Models;

namespace SimpleBankAPI.Interfaces
{
    public interface IDocumentsRepository 
    {
        Task<Document> Create(Document documentCreate);
        Task<List<Document>> GetDocumentsByAccount(int accountId);
        Task<Document> GetById(int documentID);
    }
}