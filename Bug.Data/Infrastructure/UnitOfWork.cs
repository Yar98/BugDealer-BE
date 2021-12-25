using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bug.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _config;
        private readonly BugContext _bugContext;
        private IAccountRepo _account;
        private ICategoryRepo _category;
        private ICommentRepo _comment;
        private IIssueRepo _issue;
        private IIssuelogRepo _issuelog;
        private IPermissionRepo _permission;
        private IPriorityRepo _priority;
        private IProjectRepo _project;
        private IRelationRepo _relation;
        private IRoleRepo _role;
        private IStatusRepo _status;
        private ITagRepo _tag;
        private IWorklogRepo _worklog;
        private IFieldRepo _field;
        private IAttachmentRepo _attachment;
        private ITemplateRepo _template;
        private INotificationRepo _notification;
        private ISeverityRepo _severity;
        private IAccountProjectRoleRepo _accountProjectRole;
        private IProjectlogRepo _projectlog;

        public UnitOfWork(BugContext bugContext, IConfiguration config)
        {
            _bugContext = bugContext;
            _config = config;
        }

        public IProjectlogRepo Projectlog
        {
            get
            {
                if(_projectlog == null)
                {
                    _projectlog = new ProjectlogRepo(_bugContext);
                }
                return _projectlog;
            }
        }

        public ISeverityRepo Severity
        {
            get
            {
                if (_severity == null)
                {
                    _severity = new SeverityRepo(_bugContext);
                }
                return _severity;
            }
        }

        public IAccountProjectRoleRepo AccountProjectRole
        {
            get
            {
                if (_accountProjectRole == null)
                {
                    _accountProjectRole = new AccountProjectRoleRepo(_bugContext);
                }
                return _accountProjectRole;
            }
        }

        public INotificationRepo Notification
        {
            get
            {
                if(_notification == null)
                {
                    _notification = new NotificationRepo(_bugContext);
                }
                return _notification;
            }
        }

        public ITemplateRepo Template
        {
            get
            {
                if (_template == null)
                {
                    _template = new TemplateRepo(_bugContext);
                }
                return _template;
            }
        }
        public IAttachmentRepo Attachment
        {
            get
            {
                if(_attachment == null)
                {
                    _attachment = new AttachmentRepo(_bugContext);
                }
                return _attachment;
            }
        }
        public IFieldRepo Field
        {
            get
            {
                if (_field == null)
                {
                    _field = new FieldRepo(_bugContext);
                }
                return _field;
            }
        }

        public IAccountRepo Account
        {
            get
            {
                if(_account == null)
                {
                    _account = new AccountRepo(_bugContext, _config);
                }
                return _account;
            }
        }
        public ICategoryRepo Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepo(_bugContext);
                }
                return _category;
            }
        }
        public ICommentRepo Comment
        {
            get
            {
                if (_comment == null)
                {
                    _comment = new CommentRepo(_bugContext);
                }
                return _comment;
            }
        }
        public IIssueRepo Issue
        {
            get
            {
                if (_issue == null)
                {
                    _issue = new IssueRepo(_bugContext);
                }
                return _issue;
            }
        }
        public IIssuelogRepo Issuelog
        {
            get
            {
                if (_issuelog == null)
                {
                    _issuelog = new IssuelogRepo(_bugContext);
                }
                return _issuelog;
            }
        }
        public IPermissionRepo Permission
        {
            get
            {
                if (_permission == null)
                {
                    _permission = new PermissionRepo(_bugContext);
                }
                return _permission;
            }
        }
        public IPriorityRepo Priority
        {
            get
            {
                if (_priority == null)
                {
                    _priority = new PriorityRepo(_bugContext);
                }
                return _priority;
            }
        }
        public IProjectRepo Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new ProjectRepo(_bugContext);
                }
                return _project;
            }
        }
        public IRelationRepo Relation
        {
            get
            {
                if (_relation == null)
                {
                    _relation = new RelationRepo(_bugContext);
                }
                return _relation;
            }
        }
        public IRoleRepo Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepo(_bugContext);
                }
                return _role;
            }
        }
        public IStatusRepo Status
        {
            get
            {
                if (_status == null)
                {
                    _status = new StatusRepo(_bugContext);
                }
                return _status;
            }
        }
        public ITagRepo Tag
        {
            get
            {
                if (_tag == null)
                {
                    _tag = new TagRepo(_bugContext);
                }
                return _tag;
            }
        }
        
        public IWorklogRepo Worklog
        {
            get
            {
                if (_worklog == null)
                {
                    _worklog = new WorklogRepo(_bugContext);
                }
                return _worklog;
            }
        }
        public void Save()
        {
            var check = 1;
            while (check == 1)
            {
                try
                {
                    // Attempt to save changes to the database
                    _bugContext.SaveChanges();
                    check = 0;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();
                        entry.OriginalValues.SetValues(databaseValues);
                        if (entry.Entity is Bug.Entities.Model.AccountProjectRole)
                        {
                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues?[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
        }
    }
}
