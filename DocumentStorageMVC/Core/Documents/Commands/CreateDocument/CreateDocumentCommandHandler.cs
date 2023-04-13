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
            request.Path = "/Files/" + request.File.FileName;

            using (var fs = new FileStream(_appEnv.WebRootPath + request.Path, FileMode.Create))
            {
                await request.File.CopyToAsync(fs);
            }            

            var document = _mapper.Map<Document>(request);            

            var result = _repository.Create(document);
            _repository.SaveChangesAsync();

            return result;
        }
    }
}
