using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Relation : IEntityBase
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public int TagId { get; private set; }
        public Tag Tag { get; private set; }
        public string FromIssueId { get; private set; }
        public Issue FromIssue { get; private set; }
        public string ToIssueId { get; private set; }
        public Issue ToIssue { get; private set; }

        private Relation() { }
        public Relation
            (int id,
            string description,
            int tagId,
            string fromIssueId,
            string toIssueId)
        {
            Id = id;
            Description = description;
            TagId = tagId;
            FromIssueId = fromIssueId;
            ToIssueId = toIssueId;
        }
        
        public void UpdateFromIssueId(string id)
        {
            FromIssueId = id;
        }
    }
}
