using OnionArchitecture.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IPlantCategory<T> where T:BaseEntity
    {
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> expression,params string[] includes);
        Task<T> GetByIdAsync(int id, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity,bool state=true);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
