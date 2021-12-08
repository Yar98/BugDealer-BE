using Bug.API.Dto;
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
        private readonly Project _project;
        public string TestId = "project1";
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
        public int TestStatus = 1;

        public ProjectBuilder()
        {
            _project = new Project(TestId,TestName,TestCode,TestStartDate,TestEndDate,
                TestRecentDate,TestDescription,TestUri,TestDefaultAssigneeId,
                TestCreatorId,TestTemplateId,TestStatus);
        }

        public Project Build()
        {
            return _project;
        }

        public ProjectPostDto BuildDto()
        {
            return new ProjectPostDto
            {
                Id = TestId,
                Name = TestName,
                Code = TestCode,
                StartDate = TestStartDate,
                EndDate = TestEndDate,
                RecentDate = TestRecentDate,
                Description = TestDescription,
                AvatarUri = TestUri,
                DefaultAssigneeId = TestDefaultAssigneeId,
                CreatorId = TestCreatorId,
                TemplateId = TestTemplateId,
                State = TestStatus
            };
        }
    }
}
