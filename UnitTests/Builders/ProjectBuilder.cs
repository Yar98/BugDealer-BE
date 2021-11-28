using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class ProjectBuilder
    {
        private Project _project;
        public string TestName = "Test Name";
        public string TestCode = "PJ-131";
        public DateTimeOffset TestStartDate = DateTime.Now;
        public DateTimeOffset TestEndDate = DateTime.Now;
        public DateTimeOffset TestRecentDate = DateTime.Now;
        public string TestDescription = "Test description";
        public string TestUri = "testuri.com";
        public string TestDefaultAssigneeId = null;
        public string TestCreatorId = "account1";
        public int TestTemplateId = 1;
        public int TestStatusId = 1;

        public ProjectBuilder()
        {
            var id = Guid.NewGuid().ToString();
            _project = new Project(id,TestName,TestCode,TestStartDate,TestEndDate,
                TestRecentDate,TestDescription,TestUri,TestDefaultAssigneeId,
                TestCreatorId,TestTemplateId,TestStatusId);
        }

        public Project Build()
        {
            return _project;
        }
    }
}
