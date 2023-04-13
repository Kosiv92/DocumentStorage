using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentStorageMVC.Data;
using DocumentStorageMVC.Models;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DocumentStorageMVC.Core
{
    public class GetDocumentListQueryHandler
        : IRequestHandler<GetDocumentListQuery, IndexViewModel>
    {
        private readonly IRepository<Document> _repository;

        private readonly IMapper _mapper;

        public GetDocumentListQueryHandler(IRepository<Document> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IndexViewModel> Handle(GetDocumentListQuery request, CancellationToken cancellationToken)
        {
            var documents = _repository.GetAll()
                .ProjectTo<DocumentListItemDTO>(_mapper.ConfigurationProvider);

            #region Filter
            if (!String.IsNullOrEmpty(request.TitleFilter))
            {
                documents = documents.Where(d => d.Title.Contains(request.TitleFilter));
            }
            #endregion

            #region Sort

            switch (request.SortOrder)
            {
                case SortState.TitleDesc:
                    documents = documents.OrderByDescending(s => s.Title);
                    break;
                case SortState.DateAsc:
                    documents = documents.OrderBy(s => s.Date);
                    break;
                case SortState.DateDesc:
                    documents = documents.OrderByDescending(s => s.Date);
                    break;
                case SortState.AuthorAsc:
                    documents = documents.OrderBy(s => s.Author);
                    break;
                case SortState.AuthorDesc:
                    documents = documents.OrderByDescending(s => s.Author);
                    break;
                case SortState.DocumentTypeAsc:
                    documents = documents.OrderBy(s => s.DocumentType);
                    break;
                case SortState.DocumentTypeDesc:
                    documents = documents.OrderByDescending(s => s.DocumentType);
                    break;
                default:
                    documents = documents.OrderBy(s => s.Title);
                    break;
            }

            #endregion            

            var viewModel = new IndexViewModel
            {
                SortViewModel = new SortViewModel(request.SortOrder),
                FilterViewModel = new FilterViewModel(request.TitleFilter),
                Documents = documents
            };

            return viewModel;
        }
    }
}
