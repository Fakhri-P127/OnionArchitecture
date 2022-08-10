using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Domain.Entities
{
    public class Category : Base.BaseEntity
    {
        public string Name { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }

    }
}
