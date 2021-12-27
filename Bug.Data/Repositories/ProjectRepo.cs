﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Core.Utils;

namespace Bug.Data.Repositories
{
    public class ProjectRepo : EntityRepoBase<Project>, IProjectRepo
    {
        public ProjectRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public override IQueryable<Project> SortOrder
            (IQueryable<Project> result, 
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.Name);
                    break;
                case "code":
                    result = result.OrderBy(p => p.Code);
                    break;
                case "startdate":
                    result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    result = result.OrderByDescending(p => p.StartDate);
                    break;
                case "enddate":
                    result = result.OrderBy(p => p.EndDate);
                    break;
                case "enddate_desc":
                    result = result.OrderByDescending(p => p.EndDate);
                    break;
                case "recentdate":
                    result = result.OrderBy(p => p.RecentDate);
                    break;
                case "recentdate_desc":
                    result = result.OrderByDescending(p => p.RecentDate);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }



    }
}
