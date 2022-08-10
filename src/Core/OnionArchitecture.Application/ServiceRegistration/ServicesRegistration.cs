using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using OnionArchitecture.Application.Validations.Categories;
using OnionArchitecture.Application.Mapping;
using FluentValidation.AspNetCore;
using OnionArchitecture.Application.Validations.Plants;

namespace OnionArchitecture.Application.ServiceRegistration
{
    public static class ServicesRegistration
    {
        public static void AddApplicationRegistrations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<CategoryPostDtoValidation>()
                .AddValidatorsFromAssemblyContaining<PlantPostDtoValidation>();

            services.AddAutoMapper(x => x.AddProfile(new GeneralMap()));

        }
    }
}
