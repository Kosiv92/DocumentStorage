using DocumentStorageMVC.Core;

namespace DocumentStorageMVC.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> GetById(Guid id);

        public IQueryable<T> GetAll();

        public Guid Create(T entity);

        public void Delete(Guid id);

        public Task SaveChangesAsync();
    }
}
