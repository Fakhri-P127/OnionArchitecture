using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using OnionArchitecture.Application.Validations.Categories;
using OnionArchitecture.Application.Mapping;
using FluentValidation.AspNetCore;
using OnionArchitecture.Application.Validations.Plants;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.DTOs.Plants;
using System.Reflection;
using MediatR;

namespace OnionArchitecture.Application.ServiceRegistration
{
    public static class ServicesRegistration
    {
        public static void AddApplicationRegistrations(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services/*.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()*/.AddValidatorsFromAssembly(assembly);
                //.AddValidatorsFromAssemblyContaining<CategoryPostDtoValidation>()
            //    .AddValidatorsFromAssemblyContaining<PlantPostDtoValidation>();
            //services.AddTransient<IValidator<CategoryPostDto>, CategoryPostDtoValidation>();
            //services.AddTransient<IValidator<PlantPostDto>, PlantPostDtoValidation>();
            services.AddAutoMapper(x => x.AddProfile(new GeneralMap()));
            services.AddMediatR(assembly);
        }
    }
}
