using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IPlantCategory<T> where T : BaseEntity
    {
        private readonly ProniaDbContext _context;
        //private readonly DbSet<T> _context.Set<T>();

        public GenericRepository(ProniaDbContext context)
        {
            _context = context;
            //_context.Set<T>() = dbSet;
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> expression,params string[] includes)
        {
            IQueryable<T> query = expression is not null ? _context.Set<T>()
                .Where(expression) : _context.Set<T>().AsQueryable();

            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params string[] includes)
        {

           IQueryable<T> queryValue =  _context.Set<T>().AsSingleQuery();
            if (includes.Length != 0)
            {
                foreach (string include in includes)
                {
                    queryValue.Include(include);
                }
            }
            T value = await queryValue.FirstOrDefaultAsync(x => x.Id == id);
            if (value is null) return null;
            return value;
        }
        public async Task AddAsync(T entity)
        {
            //if (_context.Set<T>().Any(x => x == entity)) return;
            await _context.Set<T>().AddAsync(entity);
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
                _context.Set<T>().Attach(entity);
            }
            
        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

      
    }
}
