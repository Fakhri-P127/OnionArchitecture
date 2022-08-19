using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Persistance.Context;
using OnionArchitecture.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProniaDbContext _context;
    
        public IPlantRepository PlantRepository { get => new PlantRepository(_context) ?? throw new NotImplementedException();}
        public ICategoryRepository CategoryRepository { get => new CategoryRepository(_context) ??  throw new NotImplementedException();}

        public UnitOfWork(ProniaDbContext context)
        {
            _context = context;
            
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

      
    }
}
