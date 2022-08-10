using OnionArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Application.DTOs.Plants
{
    public class PlantPostDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        //public List<int> CategoryId { get; set; }

        //public List<Category> PlantCategories { get; set; }
    }

  
}
