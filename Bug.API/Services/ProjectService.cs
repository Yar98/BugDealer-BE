using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Infrastructure;
using Bug.API.Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using Bug.Core.Common;

namespace Bug.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<IReadOnlyList<ProjectRecentDto>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count)
        {
            Func<ICollection<Issue>,int,string, int> dumbTest =
                (p,m,n)=> p.Select(
                    i => i.Tags.Where(
                        t => t.CategoryId == m && t.Name == n))
                .Count();
            return await _unitOfWork
                .Project
                .GetRecentProject(accountId, categoryId, tagName, count)
                .Include(p => p.Issues).ThenInclude(i => i.Tags)
                .Select(p => new ProjectRecentDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    TotalIssues = p.Issues.Count,
                    NumberDoneIssues = dumbTest(p.Issues,Bts.IssueCategory,"Done"),
                    NumberOpenIssues = dumbTest(p.Issues,Bts.IssueCategory,"Open")
                })
                .ToListAsync();
        }
    }
}
