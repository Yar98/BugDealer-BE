using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Data.Specifications
{
    public class CommentSpecification : BaseSpecification<Comment>
    {
        public CommentSpecification(int id)
            : base(c=>c.Id == id)
        {
            AddInclude(c => c.Issue);
            AddInclude(c => c.Account);
        }
    }
}
