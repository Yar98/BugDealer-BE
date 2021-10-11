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
    class WorklogConfiguration : IEntityTypeConfiguration<Worklog>
    {
        public void Configure(EntityTypeBuilder<Worklog> builder)
        {
            builder
                .ToTable("Worklog")
                .Property(b=>b.SpentTime)
                .IsRequired();
        }
    }
}
