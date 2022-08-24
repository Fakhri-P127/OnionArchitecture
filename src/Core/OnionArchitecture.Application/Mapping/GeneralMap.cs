using AutoMapper;
using OnionArchitecture.Application.DTOs.Categories;
using OnionArchitecture.Application.DTOs.Plants;
using OnionArchitecture.Application.Features.Commands.CategoryCommands;
using OnionArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.Mapping
{
    public class GeneralMap: Profile
    {
        public GeneralMap()
        {
            CreateMap<Category, CategoryItemDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryPostCommand, Category>().ReverseMap();
            
            CreateMap<Plant, PlantItemDto>();
            CreateMap<PlantPostDto, Plant>();
            //CreateMap<Plant, PlantCategoryInPlantPostDto>();
            CreateMap<PlantCategory, PlantCategoryInPlantDto>().ReverseMap();
        }
    }
}
