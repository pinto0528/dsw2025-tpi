using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dsw2025TPI.Domain.Entities;

namespace Dsw2025TPI.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> FirstAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<IEnumerable<T>> GetAllAsync(params string[] includes);
        Task<T?> GetByIdAsync(Guid id, params string[] include);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
