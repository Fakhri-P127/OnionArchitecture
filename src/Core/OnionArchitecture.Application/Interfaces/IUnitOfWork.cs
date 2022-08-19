using OnionArchitecture.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IPlantRepository PlantRepository { get;}
        public ICategoryRepository CategoryRepository { get; }
        Task SaveChangesAsync();
    }
}
