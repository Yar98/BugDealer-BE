using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Configuration;

namespace Bug.Data
{
    public class BugContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public BugContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<Account>()
                .HasMany(p => p.Projects)
                .WithMany(a => a.Accounts)
                .UsingEntity(a => a.ToTable("AccountProject"));
            builder
                .ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ProviderConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(PermissionConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(WorkflowConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(IssueConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(PriorityConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(IssueLogConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(LabelConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(WatcherConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(VoteConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(WorklogConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(TransitionConfiguration).Assembly);
        }
    }
}
