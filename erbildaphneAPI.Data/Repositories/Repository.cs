using erbildaphneAPI.DataAccess.Data;
using erbildaphneAPI.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace erbildaphneAPI.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;           //veritabanı
            _dbSet = _context.Set<T>();   //ilgili tablo

        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
        }


        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            foreach (var table in includes)
            {
                query = query.Include(table);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }




        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;  //tabloyu alır filtreleri uygulayarak filtrelenmiş verileri dödürür
            if (filter != null)  //filtre varsa
            {
                query = query.Where(filter);
            }
            if (orderby != null)  //sıralama istenmişse
            {
                query = orderby(query);
            }
            foreach (var table in includes)  //ilişkili tablolar istenmişse  Eager loadind
            {
                query = query.Include(table);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

    }
}
