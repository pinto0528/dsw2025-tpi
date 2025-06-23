using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Dsw2025TPI.Domain.Interfaces;
using Dsw2025TPI.Domain.Entities;

namespace Dsw2025TPI.Data.Repositories
{
    public class SQLRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly Dsw2025TpiContext _context;

        public SQLRepository(Dsw2025TpiContext context)
        {
            _context = context;
        }

        private static IQueryable<T> Include<T>(IQueryable<T> query, string[] includes) where T : EntityBase
        {
            var includedQuery = query;

            foreach (var include in includes)
            {
                includedQuery = includedQuery.Include(include);
            }
            return includedQuery;
        }

        public async Task<T?> FirstAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = Include(_context.Set<T>(), includes);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
        {
            var query = Include(_context.Set<T>(), includes);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = Include(_context.Set<T>(), includes);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, params string[] include)
            => await Include(_context.Set<T>(), include).FirstOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Set<T>()
                .Where(e => ids.Contains(EF.Property<Guid>(e, "Id")))
                .ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
    }
}

