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
    class IssuelogConfiguration : IEntityTypeConfiguration<Issuelog>
    {
        public void Configure(EntityTypeBuilder<Issuelog> builder)
        {
            builder
                .ToTable("Issuelog")
                .HasIndex(l => l.IssueId);
        }
    }
}
