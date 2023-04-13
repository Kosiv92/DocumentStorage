using DocumentStorageMVC.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DocumentStorageMVC.Data
{
    public class DocumentRepository : IRepository<Document>
    {
        public ApplicationDbContext _context;
        public DbSet<Document> _documents;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
            _documents = _context.Documents;
        } 
        
        public Guid Create(Document entity)
        {
            _documents.Add(entity);
            return entity.Id;
        }

        public async void Delete(Guid id)
        {
            var entity = await GetById(id);
            if (GetById(id) != null)
            {
                _documents.Remove(entity);
            }
            else
            {
                throw new Exception($"Entity \"{nameof(entity)}\" ({id} not found).");
            }
        }

        public IQueryable<Document> GetAll()
            => _documents;

        public Task<Document> GetById(Guid id)
            => _documents.FirstOrDefaultAsync(d => d.Id == id);
        
        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
        
    }
}
