using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Comment : IEntityBase
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public DateTimeOffset TimeLog { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }
        public string AccountId { get; private set; }
        public Account Account { get; private set; }
        private Comment() { }
        public Comment
            (int id,
            string content,
            DateTimeOffset time,
            string issueId,
            string accountId)
        {
            Id = id;
            TimeLog = time;
            IssueId = issueId;
            AccountId = accountId;
            Content = content;
        }

        public void UpdateContent(string s)
        {
            Content = s;
        }
        public void UpdateTimeLog(DateTimeOffset s)
        {
            TimeLog = s;
        }
        public void UpdateAccountId(string id)
        {
            AccountId = id;
        }
        public void UpdateIssueId(string id)
        {
            IssueId = id;
        }
    }
}
