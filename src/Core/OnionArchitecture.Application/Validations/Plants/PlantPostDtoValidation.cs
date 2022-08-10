using FluentValidation;
using OnionArchitecture.Application.DTOs.Plants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Validations.Plants
{
    public class PlantPostDtoValidation:AbstractValidator<PlantPostDto>
    {
        public PlantPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Price).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(9999.99M);
            RuleFor(x => x.SKU).NotEmpty().MaximumLength(8).MinimumLength(8);

        }
    }
}
