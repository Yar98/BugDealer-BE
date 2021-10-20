using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;

namespace Bug.Core.Specifications
{
    class ProjectRecentSpecification : BaseSpecification<Project>
    {
        public ProjectRecentSpecification(string projectId)
            : base(b => b.Id == projectId)
        {
            
        }
    }
}
