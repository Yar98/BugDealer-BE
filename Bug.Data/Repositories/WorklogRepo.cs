﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Data.Repositories
{
    public class WorklogRepo : EntityRepoBase<Worklog>, IWorklogRepo
    {
        public WorklogRepo(BugContext repositoryContext) 
            : base(repositoryContext)
        {

        }

        public override IQueryable<Worklog> SortOrder
            (IQueryable<Worklog> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "logdate":
                    result = result.OrderBy(p => p.LogDate);
                    break;
                case "logdate_desc":
                    result = result.OrderByDescending(p => p.LogDate);
                    break;
                case "enddate":
                    //result = result.OrderBy(p => p.EndDate);
                    break;
                case "enddate_desc":
                    //result = result.OrderByDescending(p => p.EndDate);
                    break;
                case "recentdate":
                    //result = result.OrderBy(p => p.RecentDate);
                    break;
                case "recentdate_desc":
                    //result = result.OrderByDescending(p => p.RecentDate);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }
    }
}
