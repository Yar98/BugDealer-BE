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
                if (!await bugContext.Templates.AnyAsync())
                {
                    await bugContext.Templates.AddRangeAsync(
                        GetPreconfiguredTemplate());
                }
                if (!await bugContext.Projects.AnyAsync())
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
                    //await bugContext.Categories.AddRangeAsync(
                    //GetPreconfiguredCategory());
                    GetPreconfiguredCategory()
                        .ToList()
                        .ForEach(c =>
                        {
                            bugContext.Categories.Add(c);
                            bugContext.SaveChanges();
                        });
                }

                if (!await bugContext.Tags.AnyAsync())
                {
                    //await bugContext.Tags.AddRangeAsync(
                    //GetPreconfiguredTag());
                    GetPreconfiguredTag()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Tags.Add(t);
                            bugContext.SaveChanges();
                        });
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
                if (!await bugContext.Fields.AnyAsync())
                {
                    await bugContext.Fields.AddRangeAsync(
                        GetPreconfiguredField());
                }
                if (!await bugContext.Customtypes.AnyAsync())
                {
                    await bugContext.Customtypes.AddRangeAsync(
                        GetPreconfiguredCustomtype());
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
                new Project("project1","name1","code1",DateTime.Now,DateTime.Now,DateTime.Now,"des1",null,null,"account1",1,1),
                new Project("project2","name2","code2",DateTime.Now,DateTime.Now,DateTime.Now,"des2",null,null,"account2",1,1),
                new Project("project3","name3","code3",DateTime.Now,DateTime.Now,DateTime.Now,"des3",null,null,"account3",1,1)
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
        static IEnumerable<Issue> GetPreconfiguredIssue()
        {
            return new List<Issue>()
            {
                new Issue("issue1","title1",0,"des1",DateTime.Now,DateTime.Now,DateTime.Now,DateTime.Now,null,null,"environment1","defaultStatus1",1,null,"project1","account1","account1",null)
            };
        }
        static IEnumerable<Tag> GetPreconfiguredTag()
        {
            return new List<Tag>()
            {                                       
                new Tag(0,"Open", null, null, Bts.DefaultStatusTag),
                new Tag(0,"InProgress", null, null, Bts.DefaultStatusTag),
                new Tag(0,"Done", null, null, Bts.DefaultStatusTag),
                        
                new Tag(0,"Blocks", null, null, Bts.DefaultRelationTag),
                new Tag(0,"Is Blocks By", null, null, Bts.DefaultRelationTag),
                new Tag(0,"Clones", null, null, Bts.DefaultRelationTag),
                new Tag(0,"Is Cloned By", null, null, Bts.DefaultRelationTag),
                new Tag(0,"Duplicates", null,null, Bts.DefaultRelationTag),
                new Tag(0,"Is Duplicated By", null, null, Bts.DefaultRelationTag),
                new Tag(0,"Relates To", null, null, Bts.DefaultRelationTag),

                new Tag(0,"Create", null, null, Bts.DefaultActionTag),
                new Tag(0,"Edit", null, null, Bts.DefaultActionTag),
                new Tag(0,"Comment", null, null, Bts.DefaultActionTag),
                new Tag(0,"Worklog", null, null, Bts.DefaultActionTag),
            };
        }
        static IEnumerable<Category> GetPreconfiguredCategory()
        {
            return new List<Category>()
            {
                new Category(0,"DefaultStatusTag",null),
                new Category(0,"DefaultRelationTag",null),
                new Category(0,"DefaultActionTag",null),
                new Category(0,"DefaultWorkLogTag",null),
                new Category(0,"DefaultProjectPermission",null),
                new Category(0,"DefaultIssuePermission",null),
                new Category(0,"CustomLabelTag",null)               
            };
        }
        static IEnumerable<Status> GetPreconfiguredStatus()
        {
            return new List<Status>()
            {
                new Status("defaultStatus1","Opened","From Reported status if issue have been reviewed",0,"bts",7),
                new Status("defaultStatus2","Rejected","From Opened status if issue is not a problem\n From Reported status if it is a bad report",0,"bts",7),
                new Status("defaultStatus3","Reported","From Rejected status if issue have been rewritten",0,"bts",7),
                new Status("defaultStatus4","Deferred","From Opened status if issue have been declined for repair",0,"bts",7),
                new Status("defaultStatus5","Assigned","From Opened status if issue have been approved for repair\nFrom Reopened status if issue have been approved for re-repair",50,"bts",7),
                new Status("defaultStatus6","Fixed","From Assigned status if issue have been repaired",75,"bts",7),
                new Status("defaultStatus7","Closed","From Fixed status if issue have been confirmed to be repaired",100,"bts",7),
                new Status("defaultStatus8","Reopened","From Fixed status if issue have been failed confirmation test\nFrom Closed status if problem returned\nFrom Deferred status if issue have been gathered new information",0,"bts",7)
            };
        }
        static IEnumerable<Permission> GetPreconfiguredPermission()
        {
            return new List<Permission>()
            {
                new Permission(0,"Edit details",5),
                new Permission(0,"Manage roles",5),
                new Permission(0,"Manage members",5),
                new Permission(0,"Manage statuses",5),
                new Permission(0,"Create issues",6),
                new Permission(0,"Edit issues",6),
                new Permission(0,"Delete issues",6),
                new Permission(0,"Add comments",6),
                new Permission(0,"Edit own comments",6),
                new Permission(0,"Delete own comments",6),
                new Permission(0,"Delete other comments",6),
                new Permission(0,"Add watchers",6),
                new Permission(0,"Delete watchers",6)
            };
        }
        static IEnumerable<Role> GetPreconfiguredRole()
        {
            return new List<Role>()
            {
                new Role(0,"BTS-Dev",null,"bts"),
                new Role(0, "custom", "not default", "account1")
            };
        }

        static IEnumerable<Priority> GetPreconfiguredPriority()
        {
            return new List<Priority>()
            {
                new Priority(0,"Lowest","des1","keyboard_double_arrow_down"),
                new Priority(0,"Low","des1","keyboard_arrow_down"),
                new Priority(0,"Medium","des1","drag_handle"),
                new Priority(0,"High","des1","keyboard_arrow_up"),
                new Priority(0,"Highest","des1","keyboard_double_arrow_up")          
            };
        }
        static IEnumerable<Field> GetPreconfiguredField()
        {
            return new List<Field>()
            {
                new Field(0,"Description", null),
                new Field(0,"Reporter", null),
                new Field(0,"Priority", null),
                new Field(0,"Labels", null),
                new Field(0,"Time tracking", null),
                new Field(0,"Attachment", null),
                new Field(0,"Due date", null),
                new Field(0,"Linked issues", null),
                new Field(0,"Assignee", null),
                new Field(0,"Environment", null),
                new Field(0,"Severity", null)
            };
        }
        static IEnumerable<Customtype> GetPreconfiguredCustomtype()
        {
            return new List<Customtype>()
            {
                new Customtype(0, "IssueField",null)
            };
        }
        static IEnumerable<Template> GetPreconfiguredTemplate()
        {
            return new List<Template>()
            {
                new Template(0, "IssueField",null)
            };
        }
    }
}
