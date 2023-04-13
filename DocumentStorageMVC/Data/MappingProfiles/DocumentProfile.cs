using AutoMapper;
using DocumentStorageMVC.Core;

namespace DocumentStorageMVC.Data.MappingProfiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentListItemDTO>();
            CreateMap<CreateDocumentCommand, Document>();
        }
    }
}
