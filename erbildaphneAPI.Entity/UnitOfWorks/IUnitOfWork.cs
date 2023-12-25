using erbildaphneAPI.Entity.Repositories;

namespace erbildaphneAPI.Entity.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class, new();

        void Commit();

        Task CommitAsync();
    }
}
