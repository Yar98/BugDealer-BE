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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .ToTable("Account")
                .Property(b => b.UserName)
                .IsRequired();
            builder
                .HasMany(a => a.WatchIssues)
                .WithMany(i => i.Watchers)              
                .UsingEntity(w => w.ToTable("WatcherIssue"));
            builder
                .HasMany(a => a.VoteIssues)
                .WithMany(i => i.Voters)
                .UsingEntity(v => v.ToTable("VoterIssue"));
            builder
                .HasMany(a => a.RelateProjects)
                .WithMany(r => r.Relator)
                .UsingEntity(a => a.ToTable("RelatorProject"));
            builder
                .HasMany(a => a.ReportIssues)
                .WithOne(i => i.Reporter)
                .HasForeignKey(i => i.ReporterId);
            builder
                .HasMany(a => a.AssignIssues)
                .WithOne(i => i.Assignee)
                .HasForeignKey(i => i.AssigneeId);
            builder
                .HasMany(a => a.CreatedRoles)
                .WithOne(r => r.Creator)
                .HasForeignKey(r => r.CreatorId);
            
        }
    }
}
