using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Entities.Base;

namespace OnionArchitecture.Domain.Entities
{
    public class PlantCategory:BaseEntity
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
