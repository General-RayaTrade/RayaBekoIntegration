using Microsoft.EntityFrameworkCore;
using RayaBekoIntegration.EF.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T? Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public async Task<T?> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public IEnumerable<T?> AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T?> FindAll(Expression<Func<T, bool>>? expression, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (expression != null)
            {
                return query.Where(expression);
            }
            return query.ToList();
        }

        public IEnumerable<T?> FindAll(Expression<Func<T, bool>> expression,
            int? skip = null, int? take = null, Expression<Func<T, object>>? orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _dbSet.Where(expression);
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            if (orderBy != null)
            {
                if (orderByDirection == "ASC")
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return query.ToList();
        }

        public async ValueTask<T?> FindAsync(Expression<Func<T, bool>> expression, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(expression) as T;
        }

        public T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public T? Get(Guid Id)
        {
            return _dbSet.Find(Id);
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_context.Entry(entity).Property(x => x.Id).IsModified = false; // Exclude the Id column from modification
            _context.SaveChanges();
            return entity;
        }


    }
}
