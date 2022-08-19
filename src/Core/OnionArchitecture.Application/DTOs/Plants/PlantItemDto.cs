using OnionArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.DTOs.Plants
{
    public class PlantItemDto:BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public DateTime? EnsureDate { get; set; }

        //public List<PlantCategoryInPlantPostDto> PlantCategories { get; set; }

    }
    //public class PlantCategoryInPlantPostDto
    //{
    //    public int PlantId { get; set; }
    //    public Plant Plant { get; set; }
    //    public int CategoryId { get; set; }
    //    public Category Category { get; set; }
    //}
}
