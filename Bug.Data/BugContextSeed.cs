using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Core.Common;

namespace Bug.Data
{
    public class BugContextSeed
    {
        public static async Task SeedAsync(BugContext bugContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            try
            {
                if(!await bugContext.Projects.AnyAsync())
                {
                    await bugContext.Projects.AddRangeAsync(
                        GetPreconfiguredProjects());                   
                }
                if(!await bugContext.Accounts.AnyAsync())
                {
                    await bugContext.Accounts.AddRangeAsync(
                        GetPreconfiguredAccount());
                }
                
                if (!await bugContext.Categories.AnyAsync())
                {
                    await bugContext.Categories.AddRangeAsync(
                    GetPreconfiguredCategory());
                }
                if (!await bugContext.Tags.AnyAsync())
                {
                    await bugContext.Tags.AddRangeAsync(
                    GetPreconfiguredTag());
                }
                
                if (!await bugContext.Statuses.AnyAsync())
                {
                    await bugContext.Statuses.AddRangeAsync(
                    GetPreconfiguredStatus());
                }
                if (!await bugContext.Permissions.AnyAsync())
                {
                    await bugContext.Permissions.AddRangeAsync(
                    GetPreconfiguredPermission());
                }
                if (!await bugContext.Roles.AnyAsync())
                {
                    await bugContext.Roles.AddRangeAsync(
                    GetPreconfiguredRole());
                }
                if (!await bugContext.Priorities.AnyAsync())
                {
                    await bugContext.Priorities.AddRangeAsync(
                        GetPreconfiguredPriority());
                }
                if(!await bugContext.Issues.AnyAsync())
                {
                    await bugContext.Issues.AddRangeAsync(
                        GetPreconfiguredIssue());
                }

                await bugContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var log = loggerFactory.CreateLogger<BugContextSeed>();
                log.LogError(ex.Message);
                throw;
            }
        }
        static IEnumerable<Project> GetPreconfiguredProjects()
        {
            return new List<Project>()
            {
                new Project("project1","name1","code1","type1",DateTime.Now,DateTime.Now,DateTime.Now,"des1",null,null,"account1"),
                new Project("project2","name2","code2","type2",DateTime.Now,DateTime.Now,DateTime.Now,"des2",null,null,"account2"),
                new Project("project3","name3","code3","type3",DateTime.Now,DateTime.Now,DateTime.Now,"des3",null,null,"account3")
            };
        }
        static IEnumerable<Account> GetPreconfiguredAccount()
        {
            return new List<Account>()
            {
                new Account("bts","bts","bts","first1","last1","bts",DateTime.Now,null,"uri1",null),
                new Account("account1","name1","pass1","first1","last1","email1",DateTime.Now,null,"uri1",null),
                new Account("account2","name2","pass2","first2","last2","email2",DateTime.Now,null,"uri2",null),
                new Account("account3","name3","pass3","first3","last3","email3",DateTime.Now,null,"uri3",null)
            };
        }
        static IEnumerable<Tag> GetPreconfiguredTag()
        {
            return new List<Tag>()
            {               
                new Tag("Open",null,Bts.IssueTag),
                new Tag("Close",null,Bts.IssueTag),
                new Tag("Open",null,Bts.ProjectTag)
            };
        }
        static IEnumerable<Category> GetPreconfiguredCategory()
        {
            return new List<Category>()
            {
                new Category("Issue",null),
                new Category("Project",null)               
            };
        }
        static IEnumerable<Status> GetPreconfiguredStatus()
        {
            return new List<Status>()
            {
                new Status("BTS-status1","name1","des1",1,"bts"),
                new Status("status1","name1","des1",1,"account1"),
                new Status("status2","name2","des2",13,"account2")
            };
        }
        static IEnumerable<Permission> GetPreconfiguredPermission()
        {
            return new List<Permission>()
            {
                new Permission("View Issue"),
                new Permission("Create Issue"),
                new Permission("Edit Issue"),
                new Permission("Delete Issue")
            };
        }
        static IEnumerable<Role> GetPreconfiguredRole()
        {
            return new List<Role>()
            {
                new Role("BTS-Dev",null,"bts")
            };
        }

        static IEnumerable<Priority> GetPreconfiguredPriority()
        {
            return new List<Priority>()
            {
                new Priority("pri1","des1")
            };
        }

        static IEnumerable<Issue> GetPreconfiguredIssue()
        {
            return new List<Issue>()
            {
                new Issue("issue1","title1","des1",DateTime.Now,DateTime.Now,DateTime.Now,null,null,"environment1","status1",1,"project1","account1","account1")
            };
        }
    }
}
