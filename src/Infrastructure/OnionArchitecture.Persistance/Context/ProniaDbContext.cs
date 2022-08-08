using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance.Context
{
    internal class ProniaDbContext:DbContext
    {
        public ProniaDbContext(DbContextOptions<ProniaDbContext> opt):base(opt)
        {
            
        }
    }
}
