using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities.Base;
using OnionArchitecture.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ProniaDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ProniaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> expression,params string[] includes)
        {
            IQueryable<T> query = expression is not null ? _dbSet
                .Where(expression) : _dbSet.AsQueryable();

            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, Expression<Func<T, bool>> expression, params string[] includes)
        {
         
            IQueryable<T> queryValue = expression is not null ? _dbSet.Where(expression) 
                : _dbSet.AsQueryable();
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    queryValue = queryValue.Include(include);
                }
            }
            T value = await queryValue.FirstOrDefaultAsync(x => x.Id == id);

            return value;
        }
        public async Task AddAsync(T entity)
        {
            //if (_dbSet.Any(x => x == entity)) return;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public void Update(T entity,bool state)
        {
            if (state)
            {
                _context.Entry(entity).State = EntityState.Unchanged;
            }
            else
            {
                _dbSet.Attach(entity);
            }
            
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


      
    }
}
