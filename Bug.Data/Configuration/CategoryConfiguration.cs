using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bug.Data.Configuration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Entities.Model.Category>
    {
        public void Configure(EntityTypeBuilder<Entities.Model.Category> builder)
        {
            builder
                .ToTable("Category")
                .Property(b => b.Name)
                .IsRequired();
        }
    }
}
