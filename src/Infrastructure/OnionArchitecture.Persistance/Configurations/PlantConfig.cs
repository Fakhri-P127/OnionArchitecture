using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Persistance.Configurations
{
    public class PlantConfig : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(6,2)").IsRequired();
            builder.Property(x => x.SKU).IsFixedLength().HasMaxLength(8).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.SKU).IsUnique();

        }
    }
}
