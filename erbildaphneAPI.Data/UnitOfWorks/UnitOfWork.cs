using erbildaphneAPI.DataAccess.Data;
using erbildaphneAPI.DataAccess.Repositories;
using erbildaphneAPI.Entity.Repositories;
using erbildaphneAPI.Entity.UnitOfWorks;

namespace erbildaphneAPI.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        //private bool disposed = false;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }

        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);

        //}
    }
}
