using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.EF.IRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        T? Get(int Id);
        T? Get(Guid Id);
        IEnumerable<T> GetAll();
        ValueTask<T?> FindAsync(Expression<Func<T, bool>> expression, string[]? includes = null);
        IEnumerable<T?> FindAll(Expression<Func<T, bool>>? expression, string[]? includes = null);
        IEnumerable<T?> FindAll(Expression<Func<T, bool>> expression,
            int? skip, int? take, Expression<Func<T, object>>? orderBy = null, string? orderByDirection = "ASC");
        T? Add(T entity);
        Task<T?> AddAsync(T entity);
        IEnumerable<T?> AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        T Update(T entity);
    }
}
