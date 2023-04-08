namespace DocumentStorageMVC.Core
{
    public class Document
    {
        public string Title { get; set; }

        public DateTimeOffset Data { get; set; }

        public string Author { get; set; }

        public DocumentType DocumentType { get; set; }

        public string Path { get; set; }
    }

    public enum DocumentType
    {
        External,
        Internal
    }
}
