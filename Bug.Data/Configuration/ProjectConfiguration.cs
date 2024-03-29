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
                .WithMany(a => a.CreatedProjects)
                .HasForeignKey(p=>p.CreatorId);
            builder
                .HasOne(p => p.DefaultAssignee)
                .WithMany(a => a.DefaultAssigneeProjects)
                .HasForeignKey(p=>p.DefaultAssigneeId);
            builder
                .HasOne(p => p.DefaultRole)
                .WithMany(r => r.DefaultInProjects)
                .HasForeignKey(p => p.DefaultRoleId);
            builder
                .HasOne(p => p.DefaultStatus)
                .WithMany(r => r.DefaultInProjects)
                .HasForeignKey(p => p.DefaultStatusId);
            builder
                .HasMany(p => p.Statuses)
                .WithMany(p => p.Projects)
                .UsingEntity(e => e.ToTable("ProjectStatus"));
            builder
                .HasMany(p => p.Roles)
                .WithMany(p => p.Projects)
                .UsingEntity(e => e.ToTable("ProjectRole"));
            builder
                .Ignore(p => p.TotalIssues)
                .Ignore(p => p.TotalDoneIssues)
                .Ignore(p => p.TotalOpenIssues);
        }
    }
}
