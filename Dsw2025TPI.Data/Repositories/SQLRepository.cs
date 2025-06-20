using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Dsw2025TPI.Domain.Interfaces;

namespace Dsw2025TPI.Data.Repositories
{
    public class SQLRepository<T> : IRepository<T> where T : class
    {
        private readonly Dsw2025TpiContext _context;
        private readonly DbSet<T> _dbSet;

        public SQLRepository(Dsw2025TpiContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

