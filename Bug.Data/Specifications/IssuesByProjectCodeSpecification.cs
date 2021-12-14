﻿using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class IssuesByProjectCodeSpecification : BaseSpecification<Issue>
    {
        public IssuesByProjectCodeSpecification(int code, string projectCode, string accountId)
            : base(i=>i.NumberCode.ToString().Contains(code.ToString()) &&
            i.Project.Code.Contains(projectCode) && 
            (i.Reporter.Id == accountId || i.Assignee.Id == accountId))
        {
            AddInclude(i => i.Severity);
            AddInclude(i => i.Status);
            AddInclude(i => i.Priority);
            AddInclude(i => i.Project);
            AddInclude(i => i.Reporter);
            AddInclude(i => i.Assignee);

        }
    }
}
