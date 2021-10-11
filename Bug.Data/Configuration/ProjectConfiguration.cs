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
    class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .ToTable("Project")
                .Property(p => p.Name)
                .IsRequired();
            builder
                .HasOne(p => p.Creator)
                .WithOne(a => a.CreatedProject)
                .HasForeignKey<Project>(b => b.CreatorId);
        }
    }
}
