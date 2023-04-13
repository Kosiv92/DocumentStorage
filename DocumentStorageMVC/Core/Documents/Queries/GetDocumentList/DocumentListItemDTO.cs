namespace DocumentStorageMVC.Core
{
    public class DocumentListItemDTO : BaseEntity
    {
        public string Title { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Author { get; set; }

        public DocumentType DocumentType { get; set; }
    }
}
