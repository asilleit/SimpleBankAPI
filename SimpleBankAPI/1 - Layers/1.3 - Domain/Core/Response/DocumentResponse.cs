namespace SimpleBankAPI.Models.Response
{
    public class DocumentResponse
    {
        public int AccountId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }
        public static DocumentResponse ToCreateDocumentResponse(Document document)
        {
            var documentResponse = new DocumentResponse
            {
                FileName = document.FileName,
                AccountId = document.AccountId,
                FileType = document.FileType,
                File = document.File,
            };
            return documentResponse;
        }

        public static List<DocumentResponse> FromListDocumentAccount(List<Document> documents)
        {
            var documentResponseList = new List<DocumentResponse>();
            foreach (var document in documents)
            {
                var accountResponse = DocumentResponse.ToCreateDocumentResponse(document);
                documentResponseList.Add(accountResponse);
            }
            return documentResponseList;
        }
    }

}
