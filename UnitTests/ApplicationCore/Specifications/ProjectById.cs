using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProjectById
    {
        public string TestProjectId = "project-test";
        public string TestBadProjectId = "badbad";
        public ProjectById() { }

        [Fact]
        public void MatchIfIdExist()
        {
            var spec =
                new ProjectSpecification(TestProjectId);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void NotMatchIfIdNotExist()
        {
            var spec =
                new ProjectSpecification(TestBadProjectId);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        private List<Project> GetTestListProjects()
        {
            var projectTest =
                new Project(TestProjectId, "name1", "code1", DateTime.Now, DateTime.Now, DateTime.Now, "des1", null, null, "defaultStatus1", 1, "account1", 1, 1);

            var project1 = new TestProjectBuilder().Build();
            var project2 = new TestProjectBuilder().Build();
            return new List<Project>
            {
                project1,
                project2,
                projectTest
            };
        }
    }
}
