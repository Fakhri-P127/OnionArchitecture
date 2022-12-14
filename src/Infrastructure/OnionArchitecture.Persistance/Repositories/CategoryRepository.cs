using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance.Repositories
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ProniaDbContext context):base(context)
        {

        }

    }
}
