using DocumentStorageMVC.Models;
using MediatR;

namespace DocumentStorageMVC.Core
{
    public class GetDocumentListQuery
        : IRequest<IndexViewModel>
    {
        public SortState SortOrder;

        public string? TitleFilter { get; set; }
    }
}
