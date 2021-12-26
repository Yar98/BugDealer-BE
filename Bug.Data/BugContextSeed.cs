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

                if (!await bugContext.Accounts.AnyAsync())
                {
                    await bugContext.Accounts.AddRangeAsync(
                        GetPreconfiguredAccount());
                }

                if (!await bugContext.Templates.AnyAsync())
                {
                    GetPreconfiguredTemplate()
                        .ToList()
                        .ForEach(c =>
                        {
                            bugContext.Templates.Add(c);
                            bugContext.SaveChanges();
                        });
                }
                
                if (!await bugContext.Categories.AnyAsync())
                {
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
                    GetPreconfiguredStatus()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Statuses.Add(t);
                            bugContext.SaveChanges();
                        });
                }
                if (!await bugContext.Permissions.AnyAsync())
                {
                    GetPreconfiguredPermission()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Permissions.Add(t);
                            bugContext.SaveChanges();
                        });
                }
                if (!await bugContext.Roles.AnyAsync())
                {
                    GetPreconfiguredRole()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Roles.Add(t);
                            bugContext.SaveChanges();
                        });
                }
                if (!await bugContext.Priorities.AnyAsync())
                {
                    GetPreconfiguredPriority()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Priorities.Add(t);
                            bugContext.SaveChanges();
                        });
                }
                if (!await bugContext.Fields.AnyAsync())
                {
                    GetPreconfiguredField()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Fields.Add(t);
                            bugContext.SaveChanges();
                        });
                }
                if (!await bugContext.Severities.AnyAsync())
                {
                    GetPreconfiguredSeverities()
                        .ToList()
                        .ForEach(t =>
                        {
                            bugContext.Severities.Add(t);
                            bugContext.SaveChanges();
                        });
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

        static IEnumerable<Severity> GetPreconfiguredSeverities()
        {
            return new List<Severity>()
            {
                new Severity(0,"C-121","Highest severity","keyboard_double_arrow_up"),
                new Severity(0,"C-122","High severity","keyboard_arrow_up"),
                new Severity(0,"C-123","Medium severity","drag_handle"),
                new Severity(0,"C-124","Low severity","keyboard_arrow_down"),
                new Severity(0,"C-125","Lowest severity","keyboard_double_arrow_down")
            };
        }

        static IEnumerable<Project> GetPreconfiguredProjects()
        {
            return new List<Project>()
            {
                new Project("project1","name1","code1",DateTime.Now,DateTime.Now,DateTime.Now,"des1",null,null,null,null,"account1",null,1),
                new Project("project2","name2","code2",DateTime.Now,DateTime.Now,DateTime.Now,"des2",null,null,null,null,"account2",null,1),
                new Project("project3","name3","code3",DateTime.Now,DateTime.Now,DateTime.Now,"des3",null,null,null,null,"account3",null,1)
            };
        }
        static IEnumerable<Account> GetPreconfiguredAccount()
        {
            return new List<Account>()
            {
                new Account("bts","bts","bts","bts","bts","bts",DateTime.Now,null,"uri1",null),
                //new Account("account1","name1","pass1","first1","last1","email1",DateTime.Now,null,"uri1",null),
                //new Account("account2","name2","pass2","first2","last2","email2",DateTime.Now,null,"uri2",null),
                //new Account("account3","name3","pass3","first3","last3","email3",DateTime.Now,null,"uri3",null)
            };
        }
        static IEnumerable<Issue> GetPreconfiguredIssue()
        {
            return new List<Issue>()
            {
                new Issue("issue1","title1",0,"des1",DateTime.Now,DateTime.Now,DateTime.Now,DateTime.Now,null,null,"environment1","defaultStatus1",1,null,"project1","account1","account1"),
                new Issue("issue2","title1",0,"des1",DateTime.Now,DateTime.Now,DateTime.Now,DateTime.Now,null,null,"environment1","defaultStatus1",1,null,"project1","account1","account1")
            };
        }
        static IEnumerable<Tag> GetPreconfiguredTag()
        {
            return new List<Tag>()
            {                                       
                new Tag(0,"C-113", null, "black", Bts.DefaultStatusTag),
                new Tag(0,"C-114", null, "blue", Bts.DefaultStatusTag),
                new Tag(0,"C-115", null, "green", Bts.DefaultStatusTag),
                new Tag(0,"C-092", null, "red", Bts.DefaultStatusTag),
                        
                new Tag(0,"C-041", "block", null, Bts.DefaultRelationTag),
                new Tag(0,"C-042", "is blocked by", null, Bts.DefaultRelationTag),
                new Tag(0,"C-043", "clones", null, Bts.DefaultRelationTag),
                new Tag(0,"C-044", "is cloned by", null, Bts.DefaultRelationTag),
                new Tag(0,"C-045", "duplicates",null, Bts.DefaultRelationTag),
                new Tag(0,"C-046", "is duplicates", null, Bts.DefaultRelationTag),
                new Tag(0,"C-047", "relate", null, Bts.DefaultRelationTag),
                new Tag(0,"Create issue", "L23-078", null, Bts.DefaultActionTag),
                new Tag(0,"Update status", "L23-079:L23-024", null, Bts.DefaultActionTag),
                new Tag(0,"Update assignee", "L23-079:L23-025", null, Bts.DefaultActionTag),
                new Tag(0,"Update reporter", "L23-079:L23-026", null, Bts.DefaultActionTag),
                new Tag(0,"Update priority", "L23-079:L23-027", null, Bts.DefaultActionTag),
                new Tag(0,"Update severity", "L23-079:L23-028", null, Bts.DefaultActionTag),
                new Tag(0,"Update label", "L23-079:L23-039", null, Bts.DefaultActionTag),
                new Tag(0,"Update attachment", "L23-079:L23-048", null, Bts.DefaultActionTag),
                new Tag(0,"Update remaining estimate", "L23-079:L23-080", null, Bts.DefaultActionTag),
                new Tag(0,"Update original estimate", "L23-079:L23-043", null, Bts.DefaultActionTag),
                new Tag(0,"Update due date", "L23-079:L23-044", null, Bts.DefaultActionTag),
                new Tag(0,"Update description", "L23-079:L23-007", null, Bts.DefaultActionTag),
                new Tag(0,"Update environment", "L23-079:L23-008", null, Bts.DefaultActionTag),
                new Tag(0,"Update title", "L23-079:L23-006", null, Bts.DefaultActionTag),
                new Tag(0,"Add logwork realtime", "L23-081", null, Bts.DefaultActionTag),
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
                new Status("defaultStatus1","Opened","From Reported status if issue have been reviewed",0,"bts",1),
                new Status("defaultStatus2","Rejected","From Opened status if issue is not a problem\n From Reported status if it is a bad report",0,"bts",4),
                new Status("defaultStatus3","Reported","From Rejected status if issue have been rewritten",10,"bts",2),
                new Status("defaultStatus4","Deferred","From Opened status if issue have been declined for repair",0,"bts",4),
                new Status("defaultStatus5","Assigned","From Opened status if issue have been approved for repair\nFrom Reopened status if issue have been approved for re-repair",50,"bts",2),
                new Status("defaultStatus6","Fixed","From Assigned status if issue have been repaired",75,"bts",3),
                new Status("defaultStatus7","Closed","From Fixed status if issue have been confirmed to be repaired",100,"bts",3),
                new Status("defaultStatus8","Reopened","From Fixed status if issue have been failed confirmation test\nFrom Closed status if problem returned\nFrom Deferred status if issue have been gathered new information",0,"bts",2)
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
                new Role(0, "custom", "default", "bts")
            };
        }

        static IEnumerable<Priority> GetPreconfiguredPriority()
        {
            return new List<Priority>()
            {
                new Priority(0,"C-116","Highest priority","keyboard_double_arrow_up"),
                new Priority(0,"C-117","High priority","keyboard_arrow_up"),
                new Priority(0,"C-118","Medium priority","drag_handle"),
                new Priority(0,"C-119","Low priority","keyboard_arrow_down"),
                new Priority(0,"C-120","Lowest priority","keyboard_double_arrow_down")
            };
        }
        static IEnumerable<Field> GetPreconfiguredField()
        {
            return new List<Field>()
            {
                new Field(0,"C-016", null),
                new Field(0,"C-017", null),
                new Field(0,"C-018", null),
                new Field(0,"C-019", null),
                new Field(0,"C-020", null),               
                new Field(0,"C-021", null),
                new Field(0,"C-022", null),
                new Field(0,"C-023", null),
                new Field(0,"C-024", null),
                new Field(0,"C-025", null),
                new Field(0,"C-026", null)
            };
        }
        static IEnumerable<Template> GetPreconfiguredTemplate()
        {
            return new List<Template>()
            {
                new Template(0, "C-075", null),
                new Template(0, "C-076", null),
                new Template(0, "C-077", null)
            };
        }
    }
}
