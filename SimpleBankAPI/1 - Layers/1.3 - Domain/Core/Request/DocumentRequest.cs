namespace SimpleBankAPI.Models.Request
{
    public class DocumentRequest
    {
        public int AccountId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] File { get; set; }
        public Document RequestToDocument(DocumentRequest request)
        {
            var document = new Document()
            {
                File = request.File,
                AccountId = request.AccountId,
                FileName = request.FileName,
                FileType = request.FileType
            };
            return document;

        }
    }
}
