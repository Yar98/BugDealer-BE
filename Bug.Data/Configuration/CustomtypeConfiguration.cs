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
    public class CustomtypeConfiguration : IEntityTypeConfiguration<Customtype>
    {
        public void Configure(EntityTypeBuilder<Customtype> builder)
        {
            builder
                .ToTable("Customtype")
                .Property(c => c.Name)
                .IsRequired();
        }
    }
}
