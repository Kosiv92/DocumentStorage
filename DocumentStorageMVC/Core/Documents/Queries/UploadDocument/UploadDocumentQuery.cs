using DocumentStorageMVC.Models;
using MediatR;

namespace DocumentStorageMVC.Core
{
    public class UploadDocumentQuery : IRequest<UploadDocumentDTO>
    {
        public UploadDocumentQuery(Guid id) => Id = id;
        
        public Guid Id { get; set; }
    }
}
