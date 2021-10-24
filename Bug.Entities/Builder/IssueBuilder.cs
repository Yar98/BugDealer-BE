using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public class IssueBuilder : IIssueBuilder
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Timelog { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public string OriginEstimateTime { get; private set; }
        public string RemainEstimateTime { get; private set; }
        public string Environment { get; private set; }
        public string StatusId { get; private set; }
        public int PriorityId { get; private set; }
        public string ProjectId { get; private set; }
        public string ReporterId { get; private set; }
        public string AssigneeId { get; private set; }

        public IIssueBuilder AddAssigneeId(string s)
        {
            AssigneeId = s;
            return this;
        }

        public IIssueBuilder AddCreatedDate(DateTime cd)
        {
            CreatedDate = cd;
            return this;
        }

        public IIssueBuilder AddDescription(string des)
        {
            Description = des;
            return this;
        }

        public IIssueBuilder AddDueDate(DateTime dd)
        {
            DueDate = dd;
            return this;
        }

        public IIssueBuilder AddEnvironment(string e)
        {
            Environment = e;
            return this;
        }

        public IIssueBuilder AddId(string id)
        {
            Id = id;
            return this;
        }

        public IIssueBuilder AddOriginEstimateTime(string oet)
        {
            OriginEstimateTime = oet;
            return this;
        }

        public IIssueBuilder AddPriorityId(int p)
        {
            PriorityId = p;
            return this;
        }

        public IIssueBuilder AddProjectId(string s)
        {
            ProjectId = s;
            return this;
        }

        public IIssueBuilder AddRemainEstimateTime(string ret)
        {
            RemainEstimateTime = ret;
            return this;
        }

        public IIssueBuilder AddReporterId(string s)
        {
            ReporterId = s;
            return this;
        }

        public IIssueBuilder AddStatusId(string s)
        {
            StatusId = s;
            return this;
        }

        public IIssueBuilder AddTimelog(DateTime tl)
        {
            Timelog = tl;
            return this;
        }

        public IIssueBuilder AddTitle(string title)
        {
            Title = title;
            return this;
        }

        public Issue Build()
        {
            return new Issue(Id,
                Title,
                Description,
                Timelog,
                CreatedDate,
                DueDate,
                OriginEstimateTime,
                RemainEstimateTime,
                Environment,
                StatusId,
                PriorityId,
                ProjectId,
                ReporterId,
                AssigneeId);
        }
    }
}
