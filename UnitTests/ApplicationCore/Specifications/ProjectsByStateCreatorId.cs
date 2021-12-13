using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProjectsByStateCreatorId
    {
        public int TestState = 1;
        public string TestCreatorId = "account-test";
        public string TestBadCreatorId = "hahha";
        public ProjectsByStateCreatorId() { }

        [Fact]
        public void NotMatchIfCreatorIdExistAndWrongState()
        {
            var spec =
                new ProjectsByStateCreatorIdSpecification(TestCreatorId, 0);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfCreatorIdNotExist()
        {
            var spec =
                new ProjectsByStateCreatorIdSpecification(TestBadCreatorId, TestState);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void MatchIfCreatorIdAndStateExsit()
        {
            var spec =
                new ProjectsByStateCreatorIdSpecification(TestCreatorId, TestState);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        private List<Project> GetTestListProjects()
        {
            var projectTest = new TestProjectBuilder().Build();
            projectTest.UpdateCreatorId(TestCreatorId);
            projectTest.UpdateState(TestState);

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
