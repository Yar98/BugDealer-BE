using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Provider:IEntityBase
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

    }
}
