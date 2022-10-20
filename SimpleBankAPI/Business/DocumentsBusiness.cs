using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using SimpleBankAPI.Data;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.JWT;
using SimpleBankAPI.Models;
using SimpleBankAPI.Models.Request;
using SimpleBankAPI.Models.Response;
using System.Reflection.Metadata;
using Document = SimpleBankAPI.Models.Document;

namespace SimpleBankAPI.Business
{
    public class DocumentsBusiness : IDocumentsBusiness
    {
        protected IDocumentsDb _documentsDb;
        protected IJwtAuth _jwtAuth;
        public DocumentsBusiness(IDocumentsDb documentsDb, IJwtAuth jwtAuth)
        {
            _documentsDb = documentsDb;
            _jwtAuth = jwtAuth;
        }
        public async Task<DocumentResponse> Create(IFormFile file, int accountId)
        {
            //valid extension

            var extension = Path.GetExtension(file.FileName);
            string[] permittedExtensions = { ".png", ".pdf" };
            if (!permittedExtensions.Contains(extension))
            {
                throw new ArgumentException("Extension invalid");
            }

            //Strip out any path specifiers (ex: /../)
            var originalFileName = Path.GetFileName(file.FileName);
            Document document = new Document();
            //Convert file to byte
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                document.File  = ms.ToArray();
                //string s = Convert.ToBase64String(fileBytes);cc
                // act on the Base64 data
            }
            document.FileName= Path.GetFileName(file.FileName); 
            document.FileType = extension;
            document.AccountId = accountId;

            var CreatedDocument = await _documentsDb.Create(document);
            //DocumentRequest 
            var createDocumentResponse = DocumentResponse.ToCreateDocumentResponse(CreatedDocument);
            return createDocumentResponse;
        }

        public Task<Document> GetById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Document>> GetDocumentsByUser(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
