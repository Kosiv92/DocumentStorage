using AutoMapper;
using DocumentStorageMVC.Data;
using MediatR;

namespace DocumentStorageMVC.Core
{
    public class CreateDocumentCommandHandler 
        : IRequestHandler<CreateDocumentCommand, Guid>
    {
        private readonly IWebHostEnvironment _appEnv;
        private readonly IRepository<Document> _repository;
        private readonly IMapper _mapper;

        public CreateDocumentCommandHandler(IWebHostEnvironment appEnv, IRepository<Document> repository, IMapper mapper)
            => (_appEnv, _repository, _mapper) = (appEnv, repository, mapper);

        public async Task<Guid> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            string fileExtension = request.File.FileName.Substring(request.File.FileName.LastIndexOf('.'));                        

            request.Path = "/Files/" + request.Title + fileExtension;

            using (var fs = new FileStream(_appEnv.WebRootPath + request.Path, FileMode.Create))
            {
                await request.File.CopyToAsync(fs);
            }            

            var document = _mapper.Map<Document>(request);
            document.Date = DateTimeOffset.Now;                        

            var result = _repository.Create(document);
            _repository.SaveChangesAsync();

            return result;
        }
    }
}
