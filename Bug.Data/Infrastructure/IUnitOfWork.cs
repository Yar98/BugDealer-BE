using Bug.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IAccountRepo Account { get; }
        ICategoryRepo Category { get; }
        ICommentRepo Comment { get; }
        IIssueRepo Issue { get; }
        IIssuelogRepo Issuelog { get; }
        IPermissionRepo Permission { get; }
        IPriorityRepo Priority { get; }
        IProjectRepo Project { get; }
        IRelationRepo Relation { get; }
        IRoleRepo Role { get; }
        IStatusRepo Status { get; }
        ITagRepo Tag { get; }
        ITransitionRepo Transition { get; }
        IVoteRepo Vote { get; }
        IWatcherRepo Watcher { get; }
        IWorkflowRepo Workflow { get; }
        IWorklogRepo Worklog { get; }
        public void Save();
    }
}
