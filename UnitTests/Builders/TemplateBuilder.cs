using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class TemplateBuilder
    {
        private readonly Template _template;
        public int TestId = 1;
        public string TestName = "webapp";
        public string TestDescription = "test description";

        public TemplateBuilder()
        {
            _template = new Template(0, TestName, TestDescription);
        }

        public Template Build()
        {
            return _template;
        }
    }
}
