using DocumentStorageMVC.Core;

namespace DocumentStorageMVC.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            CreateDocumentCommand = new CreateDocumentCommand();
        }
        public IEnumerable<DocumentListItemDTO> Documents { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public CreateDocumentCommand CreateDocumentCommand { get; set; }
    }
}
