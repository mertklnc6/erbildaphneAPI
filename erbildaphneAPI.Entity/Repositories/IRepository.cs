﻿using System.Linq.Expressions;

namespace erbildaphneAPI.Entity.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetById(int id);

        Task<T> Get(Expression<Func<T, bool>> predicate);

        Task Create(T entity);

        void Update(T entity);


        void Delete(T entity);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    }
}
