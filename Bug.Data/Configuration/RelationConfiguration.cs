﻿using System;
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
                .ToTable("Relation")
                .HasKey(a => new
                {
                    a.FromIssueId,
                    a.ToIssueId,
                    a.TagId
                });
            builder
                .HasOne(i => i.FromIssue)
                .WithMany(r => r.FromRelations)
                .HasForeignKey(r => r.FromIssueId);
            builder
                .HasOne(i => i.ToIssue)
                .WithMany(r => r.ToRelations)
                .HasForeignKey(r => r.ToIssueId);
        }
    }
}
