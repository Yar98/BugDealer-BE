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
    class RelationConfiguration : IEntityTypeConfiguration<Relation>
    {
        public void Configure(EntityTypeBuilder<Relation> builder)
        {
            builder
                .ToTable("Relation");
            builder
                .HasOne(r => r.FromIssue)
                .WithMany(i => i.FromRelations)
                .HasForeignKey(r => r.FromIssueId);
            builder
                .HasOne(r => r.ToIssue)
                .WithMany(i => i.ToRelations)
                .HasForeignKey(r => r.ToIssueId);
        }
    }
}
