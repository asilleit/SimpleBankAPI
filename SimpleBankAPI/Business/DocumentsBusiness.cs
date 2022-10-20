using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using SimpleBankAPI.Data;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Authentication;
using System.Security.Cryptography.Xml;
using Document = SimpleBankAPI.Models.Document;

namespace SimpleBankAPI.Business
{
    public class DocumentsBusiness : IDocumentsBusiness
    {
        protected IDocumentsDb _documentsDb;
        protected IAccountsDb _accountsDb;
        protected IJwtAuth _jwtAuth;
        private string[] permittedExtensions = { ".png", ".pdf" };
        public DocumentsBusiness(IDocumentsDb documentsDb, IJwtAuth jwtAuth, IAccountsDb accountsDb)
        {
            _documentsDb = documentsDb;
            _jwtAuth = jwtAuth;
            _accountsDb = accountsDb;
        }
        public async Task<DocumentResponse> Create(IFormFile file, int accountId, int userId)
        {
            //Validation
            var extension = Path.GetExtension(file.FileName);
            var account = await _accountsDb.GetById(accountId);
            //var documents = await _documentsDb.GetDocumentsByAccount(userId);

            if (!permittedExtensions.Contains(extension)) throw new ArgumentException("Extension invalid");
            if (account.Equals(null)) throw new AuthenticationException("Account not exist");
            if (account.UserId != userId) throw new AuthenticationException("User don't owner account");
            //if (file.Length > (2 * 1024)) throw new AuthenticationException("Document over 2MB");

            Document document = new Document();

                //Convert file to byte
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    document.File = ms.ToArray();
                }
                document.FileName = Path.GetFileName(file.FileName);
                document.FileType = extension;
                document.AccountId = accountId;

                var CreatedDocument = await _documentsDb.Create(document);
                var createDocumentResponse = DocumentResponse.ToCreateDocumentResponse(CreatedDocument);

                return createDocumentResponse;
            
        }

        public async Task<Document> GetById(int documentId)
        {
            if (await _documentsDb.GetById(documentId) is not null)
            {
                return await _documentsDb.GetById(documentId);
            }
            throw new ArgumentException("Account not found");
        }

        public async Task<List<Document>> GetDocumentsByAccount(int userId)
        {
            //  if (await _documentsDb.GetDocumentsByAccount(userId) is not null)
            //  {
            return await _documentsDb.GetDocumentsByAccount(userId);
            //    }
            throw new ArgumentException("Account not found");
        }

    }
}
