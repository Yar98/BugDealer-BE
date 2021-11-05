using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Comment : IEntityBase
    {
        public string Id { get; private set; }
        public string Content { get; private set; }
        public string TimeLog { get; private set; }
        public string IssueId { get; private set; }
        public Issue Issue { get; private set; }
        public string AccountId { get; private set; }
        public Account Account { get; private set; }
        private Comment() { }
        public Comment(string id,
            string content,
            string time,
            string issueId,
            string accountId)
        {
            Id = id;
            TimeLog = time;
            IssueId = issueId;
            AccountId = accountId;
        }
    }
}
