using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Projectlog : IEntityBase
    {
        public int Id { get; private set; }
        public string ProjectId { get; private set; }
        public Project Project { get; private set; }
        public string ModifierId { get; private set; }
        public Account Modifier { get; private set; }
        public DateTimeOffset LogDate { get; private set; }

        private Projectlog() { }

        public Projectlog(string projectId, string modifierId)
        {
            ProjectId = projectId;
            ModifierId = modifierId;
            LogDate = DateTimeOffset.Now;
        }
    }
}
