using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Bug.Data.Specifications;
using System.Threading;
using Bug.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Bug.Core.Utils;
using Microsoft.Data.SqlClient;
using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Amazon;

namespace Bug.Data.Repositories
{
    public class IssueRepo : EntityRepoBase<Issue>, IIssueRepo
    {
        private readonly IConfiguration _config;
        public IssueRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {
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

        public string GeneratePreSignedURL(double duration)
        {
            var s3Client = new AmazonS3Client
                (_config.GetSection("Cognito")["AccessKeyId"],
                _config.GetSection("Cognito")["AccessSecretKey"], 
                RegionEndpoint.GetBySystemName(_config.GetSection("Cognito")["Region"]));
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "bugdealer",
                Key = "",
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(duration)
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }

        public override IQueryable<Issue> SortOrder
            (IQueryable<Issue> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    //result = result.OrderBy(p => p.Name);
                    break;
                case "startdate":
                    //result = result.OrderBy(p => p.StartDate);
                    break;
                case "startdate_desc":
                    //result = result.OrderByDescending(p => p.StartDate);
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
