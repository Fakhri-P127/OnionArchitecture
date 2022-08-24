using FluentValidation;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.Features.Commands.CategoryCommands;
using OnionArchitecture.Application.Interfaces;

namespace OnionArchitecture.Application.Validations.Categories
{
    public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(15);
        }
        
    }
    public class CategoryPostCommandValidation : AbstractValidator<CategoryPostCommand>
    {
      
        public CategoryPostCommandValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(15).WithMessage("xeyoouu bro");

        }
        
    }
}
