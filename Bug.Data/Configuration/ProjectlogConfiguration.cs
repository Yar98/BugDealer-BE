using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Configuration
{
    public class ProjectlogConfiguration : IEntityTypeConfiguration<Projectlog>
    {
        public void Configure(EntityTypeBuilder<Projectlog> builder)
        {
            builder
                .ToTable("Projectlog")
                .HasIndex(p => p.LogDate);
        }
    }
}
