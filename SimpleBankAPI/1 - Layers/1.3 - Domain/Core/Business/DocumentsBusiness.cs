using SimpleBankAPI.Interfaces;
using System.Security.Authentication;
using Document = SimpleBankAPI.Models.Document;

namespace SimpleBankAPI.Business
{
    public class DocumentsBusiness : IDocumentsBusiness
    {
       protected IDocumentsRepository _documentsDb;
        protected IAccountsRepository _accountsDb;
        protected IJwtAuth _jwtAuth;
        protected string[] permittedExtensions = { ".png", ".pdf" };
        public DocumentsBusiness(IDocumentsRepository documentsDb, IJwtAuth jwtAuth, IAccountsRepository accountsDb)
        {
            _documentsDb = documentsDb;
            _jwtAuth = jwtAuth;
            _accountsDb = accountsDb;
        }
        public async Task<string> Create(IFormFile file, int accountId, int userId)
        {
            //Validation
            var extension = Path.GetExtension(file.FileName);
            var account = await _accountsDb.GetById(accountId);

            //Validations
            if (!permittedExtensions.Contains(extension)) throw new ArgumentException("Extension invalid");
            if (account.Equals(null)) throw new AuthenticationException("Account not exist");
            if (account.UserId != userId) throw new AuthenticationException("User don't owner account");

            //Convert file to byte
            var document = new Document
            {
                FileName = Path.GetFileName(file.FileName),
                FileType = extension,
                AccountId = accountId,
            };
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                document.File = ms.ToArray();
            }

            //Persist Document
            await _documentsDb.Create(document);

            return "Upload document sucess";
        }

        public async Task<Document> DownloadDocument(int documentId)
        {
            if (await _documentsDb.GetById(documentId) is not null)
            {
                return await _documentsDb.GetById(documentId);
            }
            throw new ArgumentException("Document not found");
        }

        public async Task<List<Document>> GetDocumentsByAccount(int accountId)
        {
            if (await _documentsDb.GetDocumentsByAccount(accountId) is not null)
            {
                return await _documentsDb.GetDocumentsByAccount(accountId);
            }
            throw new ArgumentException("Documents not found");
        }

    }
}
