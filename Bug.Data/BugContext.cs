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
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Issuelog> Issuelogs { get; set; }
        public DbSet<Worklog> Worklogs { get; set; }
        public DbSet<Transition> Transitions { get; set; }
        public DbSet<Timezone> Timezones { get; set; }
        public DbSet<Status> Statuses { get; set; }
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
                .ApplyConfigurationsFromAssembly(typeof(AttachmentConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(PermissionConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(WorkflowConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(IssueConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(PriorityConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(IssuelogConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(WorklogConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(TransitionConfiguration).Assembly)
                .ApplyConfigurationsFromAssembly(typeof(TimezoneConfiguration).Assembly);
        }
    }
}
