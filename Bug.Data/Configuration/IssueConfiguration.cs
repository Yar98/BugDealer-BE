using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Integration;
using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bug.Data.Configuration
{
    class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder
                .ToTable("Issue")
                .Property(b => b.Title)
                .IsRequired();
            builder
                .Property(i => i.Id)
                .HasMaxLength(100);
            builder
                .Ignore(i => i.Code)
                .Ignore(i => i.TotalSpentTime)
                .Ignore(i => i.TotalWatches)
                .Ignore(i => i.TotalVotes)
                .Ignore(i => i.PresignLink);
        }
    }
}
