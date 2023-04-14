using DocumentStorageMVC.Data;
using DocumentStorageMVC.Models;
using MediatR;

namespace DocumentStorageMVC.Core
{
    public class UploadDocumentQueryHandler
        : IRequestHandler<UploadDocumentQuery, UploadDocumentDTO>
    {
        private readonly IRepository<Document> _repository;
        private readonly IWebHostEnvironment _appEnv;

        public UploadDocumentQueryHandler(IRepository<Document> repository, IWebHostEnvironment appEnv)
        {
            _repository = repository;
            _appEnv = appEnv;
        }

        public async Task<UploadDocumentDTO> Handle
            (UploadDocumentQuery request, CancellationToken cancellationToken)
        {
            var document = await _repository.GetById(request.Id);

            var dto = new UploadDocumentDTO();

            dto.FilePath = _appEnv.WebRootPath + document.Path;
            
            dto.FileName = dto.FilePath.Substring(dto.FilePath.LastIndexOf('/') + 1);

            return dto;
        }
    }
}
