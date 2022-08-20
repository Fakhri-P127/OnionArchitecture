using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using OnionArchitecture.Application.Validations.Categories;
using OnionArchitecture.Application.Mapping;
using FluentValidation.AspNetCore;
using OnionArchitecture.Application.Validations.Plants;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.DTOs.Plants;
using System.Reflection;

namespace OnionArchitecture.Application.ServiceRegistration
{
    public static class ServicesRegistration
    {
        public static void AddApplicationRegistrations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(f=>f.DisableDataAnnotationsValidation=true)
                .AddFluentValidationClientsideAdapters().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                //.AddValidatorsFromAssemblyContaining<CategoryPostDtoValidation>()
            //    .AddValidatorsFromAssemblyContaining<PlantPostDtoValidation>();
            //services.AddTransient<IValidator<CategoryPostDto>, CategoryPostDtoValidation>();
            //services.AddTransient<IValidator<PlantPostDto>, PlantPostDtoValidation>();
            services.AddAutoMapper(x => x.AddProfile(new GeneralMap()));

        }
    }
}
