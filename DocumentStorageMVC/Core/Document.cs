namespace DocumentStorageMVC.Core
{
    public class Document : BaseEntity
    {

        public string Title { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Author { get; set; }

        public DocumentType DocumentType { get; set; }

        public string Path { get; set; }
    }

    public enum DocumentType
    {
        External,
        Internal
    }

    public enum SortState
    {
        TitleAsc,
        TitleDesc,
        DateAsc,
        DateDesc,
        AuthorAsc,
        AuthorDesc,
        DocumentTypeAsc,
        DocumentTypeDesc
    }
}
