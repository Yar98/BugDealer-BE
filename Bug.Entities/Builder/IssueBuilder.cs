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
        public int NumberCode { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset? Timelog { get; private set; } // ko can
        public DateTimeOffset? CreatedDate { get; private set; }
        public DateTimeOffset? DueDate { get; private set; }
        public DateTimeOffset? WorklogDate { get; private set; } //ko can
        public string OriginEstimateTime { get; private set; }
        public string RemainEstimateTime { get; private set; }
        public string Environment { get; private set; }
        public string StatusId { get; private set; }
        public int? PriorityId { get; private set; }
        public string ProjectId { get; private set; }
        public string ReporterId { get; private set; }
        public string AssigneeId { get; private set; }
        public int? SeverityId { get; private set; }

        public IIssueBuilder AddSeverityId(string i)
        {
            if (string.IsNullOrEmpty(i))
            {
                SeverityId = null;
                return this;
            }              
            SeverityId = int.Parse(i);
            return this;
        }

        public IIssueBuilder AddNumberCode(int code)
        {
            NumberCode = code;
            return this;
        }

        public IIssueBuilder AddAssigneeId(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                AssigneeId = null;
                return this;
            }                
            AssigneeId = s;
            return this;
        }

        public IIssueBuilder AddCreatedDate(string cd)
        {
            if(string.IsNullOrEmpty(cd))
            {
                CreatedDate = null;
                return this;
            }
            CreatedDate = DateTimeOffset.Parse(cd);
            return this;
        }

        public IIssueBuilder AddDescription(string des)
        {
            Description = des;
            return this;
        }

        public IIssueBuilder AddDueDate(string dd)
        {
            if (string.IsNullOrEmpty(dd))
            {
                DueDate = null;
                return this;
            }
            DueDate = DateTimeOffset.Parse(dd);
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
            if (string.IsNullOrEmpty(oet))
            {
                OriginEstimateTime = null;
                return this;
            }
            OriginEstimateTime = oet;
            return this;
        }

        public IIssueBuilder AddPriorityId(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                PriorityId = null;
                return this;
            }
            PriorityId = int.Parse(p);
            return this;
        }

        public IIssueBuilder AddProjectId(string s)
        {
            ProjectId = s;
            return this;
        }

        public IIssueBuilder AddRemainEstimateTime(string ret)
        {
            if (string.IsNullOrEmpty(ret))
            {
                RemainEstimateTime = null;
                return this;
            }
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
            if (string.IsNullOrEmpty(s))
                s = null;            
            StatusId = s;
            return this;
        }

        public IIssueBuilder AddTitle(string title)
        {
            Title = title;
            return this;
        }

        public Issue Build()
        {
            return new Issue
                (Id,
                Title,
                NumberCode,
                Description,
                Timelog,
                CreatedDate,
                DueDate,
                WorklogDate,
                OriginEstimateTime,
                RemainEstimateTime,
                Environment,
                StatusId,
                PriorityId,
                SeverityId,
                ProjectId,
                ReporterId,
                AssigneeId);
        }
    }
}
