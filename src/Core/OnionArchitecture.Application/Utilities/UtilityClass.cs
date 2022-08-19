using OnionArchitecture.Application.DTOs.Plants;
using OnionArchitecture.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Utilities
{
    public static class UtilityClass
    {
        public static async Task<PlantPostDto> RemoveDuplicateCategories(PlantPostDto dto,ICategoryRepository _categoryRepository)
        {
            List<PlantCategoryInPlantDto> pcategories = new();
            foreach (var category in dto.PlantCategories)
            {
                if (await _categoryRepository.GetByIdAsync(category.CategoryId, null) is null)
                {
                    pcategories.Add(category);
                }
            }
            dto.PlantCategories.RemoveAll(x => pcategories.Any(c => c.CategoryId == x.CategoryId));

            return dto;
        }
    }
}
