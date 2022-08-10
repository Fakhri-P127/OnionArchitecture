using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Interfaces.Repositories;
using OnionArchitecture.Persistance.Context;
using OnionArchitecture.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance.ServiceRegistration
{
    public static class ServicesRegistration
    {
        public static void AddPersistanceRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProniaDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("default"));
            });
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPlantRepository, PlantRepository>();

        }
    }
}
