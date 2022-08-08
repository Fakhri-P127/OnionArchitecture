using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities.Base;
using OnionArchitecture.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ProniaDbContext _context;

        public GenericRepository(ProniaDbContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            List<T> values = await _context.Set<T>().ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(int id)
        {
           T value = await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id == id);
            return value;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

    }
}
