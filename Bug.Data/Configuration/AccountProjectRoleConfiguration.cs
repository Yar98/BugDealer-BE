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
    public class AccountProjectRoleConfiguration : IEntityTypeConfiguration<AccountProjectRole>
    {
        public void Configure(EntityTypeBuilder<AccountProjectRole> builder)
        {
            builder
                .ToTable("AccountProjectRole")
                .HasKey(a => new
                {
                    a.AccountId,
                    a.ProjectId,
                    a.RoleId
                });
            builder
                .HasOne(e => e.Account)
                .WithMany(e => e.AccountProjectRoles)
                .HasForeignKey(e => e.AccountId);
            builder
                .HasOne(e => e.Project)
                .WithMany(e => e.AccountProjectRoles)
                .HasForeignKey(e => e.ProjectId);
            builder
                .HasOne(e => e.Role)
                .WithMany(e => e.AccountProjectRoles)
                .HasForeignKey(e => e.RoleId);
        }
    }
}
