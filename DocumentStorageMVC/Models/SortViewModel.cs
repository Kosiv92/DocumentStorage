using DocumentStorageMVC.Core;

namespace DocumentStorageMVC.Models
{
    public class SortViewModel
    {
        public SortState TitleSort { get; private set; }
        public SortState DateSort { get; private set; }
        public SortState AuthorSort { get; private set; }
        public SortState DocumentTypeSort { get; private set; }
        public SortState Current { get; private set; }



        public SortViewModel(SortState sortOrder)
        {
            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            AuthorSort = sortOrder == SortState.AuthorAsc ? SortState.AuthorDesc : SortState.AuthorAsc;
            DocumentTypeSort = sortOrder == SortState.DocumentTypeAsc ? SortState.DocumentTypeDesc : SortState.DocumentTypeAsc;
            Current = sortOrder;
        }

    }
}
