using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Entities.Model;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Bug.Core.Utils;
using System.Data.Common;
using System.Data;
using System;

namespace Bug.Data.Repositories
{
    public class IssueRepo : EntityRepoBase<Issue>, IIssueRepo
    {
        private string sql = "SELECT i.*,p.Code AS ProjectCode " +
            "FROM [Issue] as i JOIN[Project] as p " +
            "ON i.ProjectId = p.Id AND p.Id = @project " +
            "WHERE(p.Code + CAST(i.NumberCode AS NVARCHAR) LIKE @search OR i.Title Like @search)";
        public IssueRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task DeleteIssueById(string id, CancellationToken cancellationToken = default)
        {
            await _bugContext
                .Database
                .ExecuteSqlRawAsync("DELETE FROM Issue WHERE Id = '" + id + "'", cancellationToken);
        }

        public async Task<PaginatedList<Issue>> GetPaginatedByFilter
            (string projectId,
            int pageIndex,
            int pageSize,
            string sortOrder,
            string search,
            string statuses,
            string assignees,
            string reporters,
            string priorities,
            string severities,
            CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrEmpty(assignees))
            {
                sql += " AND i.AssigneeId IN " +
                    "(SELECT VALUE FROM STRING_SPLIT('"+ assignees + "', ','))";
            }
            if (!string.IsNullOrEmpty(statuses))
            {
                sql += " AND i.StatusId IN " +
                    " (SELECT VALUE FROM STRING_SPLIT('"+ statuses + "', ','))";
            }
            if (!string.IsNullOrEmpty(reporters))
            {
                sql += " AND i.ReporterId IN " +
                    " (SELECT VALUE FROM STRING_SPLIT('"+ reporters + "', ',')) ";
            }
            if (!string.IsNullOrEmpty(priorities))
            {
                sql += " AND i.PriorityId IN " +
                    " (SELECT VALUE FROM STRING_SPLIT('"+ priorities + "', ',')) ";
            }
            if (!string.IsNullOrEmpty(severities))
            {
                sql += " AND i.SeverityId IN " +
                    " (SELECT VALUE FROM STRING_SPLIT('"+ severities + "', ',')) ";
            }
            //SortOrder(sql, sortOrder);
            var projectSql = new SqlParameter("project", projectId);
            var sortOrderSql = new SqlParameter("sortOrder", sortOrder);
            var searchSql = new SqlParameter("search", search);           

            var result = _bugContext
                .Issues
                .FromSqlRaw(sql, projectSql, sortOrderSql, searchSql)
                .Include(i => i.Project)
                .Include(i => i.Assignee)
                .Include(i => i.Status)
                .ThenInclude(s=>s.Tag)
                .Include(i => i.Reporter)
                .Include(i => i.Severity)
                .Include(i => i.Priority);
            var qr = SortOrder(result, sortOrder);
            return await PaginatedList<Issue>.CreateListAsync
                (result.AsNoTracking(), pageIndex, pageSize, cancellationToken);
        }

        public async Task UpdateTagsOfIssueAsync
            (string issueId,
            List<Tag> tags,
            CancellationToken cancellationToken = default)
        {
            var listStr = tags
                .Select(t => t.Id.ToString())
                .ToList()
                .Aggregate<string>((x, y) => x + "," + y);
            var issue = new SqlParameter("@issue", issueId);
            var list = new SqlParameter("@list", listStr);
            await _bugContext
                .Database
                .ExecuteSqlInterpolatedAsync($"EXECUTE dbo.UpdateTagsOfIssue {issue}, {list}", cancellationToken);
        }

        public async Task UpdateAttachmentsOfIssueAsync
            (string issueId,
            List<Attachment> attachments,
            CancellationToken cancellationToken = default)
        {
            var listStr = attachments
                .Select(t => t.Id.ToString())
                .ToList()
                .Aggregate((x, y) => x + "," + y);
            var uris = attachments
                .Where(t => t.Id == 0)
                .Select(t => t.Uri)
                .ToList();
            string listUrisStr = "";
            if (uris.Count != 0)
                listUrisStr = uris.Aggregate((x, y) => x + "," + y);
            var issue = new SqlParameter("@issue", issueId);
            var listIds = new SqlParameter("@listId", listStr);
            var listUris = new SqlParameter("@listUris", listUrisStr);
            await _bugContext
                .Database
                .ExecuteSqlInterpolatedAsync($"EXECUTE dbo.UpdateAttachmentsOfIssue {issue}, {listIds}, {listUris}", cancellationToken);
        }

        public async Task<List<Issue>> GetSuggestIssuesAsync
            (string projectId, 
            string search,
            string sortOrder,
            CancellationToken cancellationToken = default)
        {
            var s = new SqlParameter("@search", "%" + search + "%");
            var s1 = new SqlParameter("@search1", "%" + search + "%");
            var project = new SqlParameter("@project", projectId);
            var order = new SqlParameter("order", sortOrder);

            return await _bugContext
                .Issues
                .FromSqlInterpolated($"SELECT i.* FROM [Issue] as i JOIN [Project] as p ON i.ProjectId = p.Id AND p.Id = {project} WHERE p.Code + CAST(i.NumberCode AS NVARCHAR) LIKE {s} OR i.Title Like {s1}")
                .Include(i=>i.Project)
                .ToListAsync(cancellationToken);
        }

        public void UpdateIssuesHaveDumbStatus(List<Status> statuses)
        {
            if (statuses == null)
                return;
            var statusIds = statuses
                .Select(s => s.Id)
                .ToList()
                .Aggregate((x, y) => x + "," + y);
            var list = new SqlParameter("list", statusIds);
            _bugContext
                .Database
                .ExecuteSqlRaw("EXECUTE dbo.UpdateIssuesHaveDumbStatus @list", list);
        }

        public override IQueryable<Issue> SortOrder
            (IQueryable<Issue> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "title":
                    result = result.OrderBy(p => p.Title);
                    break;
                case "title_desc":
                    result = result.OrderByDescending(p => p.Title);
                    break;
                case "code":
                    result = result.OrderBy(p => p.NumberCode);
                    break;
                case "code_desc":
                    result = result.OrderByDescending(p => p.NumberCode);
                    break;
                case "due":
                    result = result.OrderBy(p => p.DueDate);
                    break;
                case "due_desc":
                    result = result.OrderByDescending(p => p.DueDate);
                    break;
                case "created":
                    result = result.OrderBy(p => p.CreatedDate);
                    break;
                case "created_desc":
                    result = result.OrderByDescending(p => p.CreatedDate);
                    break;
                case "assignee":
                    result = result.OrderBy(p => p.Assignee.FirstName);
                    break;
                case "assignee_desc":
                    result = result.OrderByDescending(p => p.Assignee.FirstName);
                    break;
                case "reporter":
                    result = result.OrderBy(p => p.Reporter.FirstName);
                    break;
                case "reporter_desc":
                    result = result.OrderByDescending(p => p.Reporter.FirstName);
                    break;
                case "status":
                    result = result.OrderBy(p => p.Status.Name);
                    break;
                case "status_desc":
                    result = result.OrderByDescending(p => p.Status.Name);
                    break;
                case "priority":
                    result = result.OrderBy(p => p.Priority.Name);
                    break;
                case "priority_desc":
                    result = result.OrderByDescending(p => p.Priority.Name);
                    break;
                case "severity":
                    result = result.OrderBy(p => p.Severity.Name);
                    break;
                case "severity_desc":
                    result = result.OrderByDescending(p => p.Severity.Name);
                    break;
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }

        

    }
}
