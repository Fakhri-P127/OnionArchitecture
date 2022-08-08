using OnionArchitecture.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        //Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity); 
    }
}
