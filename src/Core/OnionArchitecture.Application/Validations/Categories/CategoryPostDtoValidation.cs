using FluentValidation;
using OnionArchitecture.Application.DTOs.Categories;

namespace OnionArchitecture.Application.Validations.Categories
{
    public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(15);
        }
    }
}
