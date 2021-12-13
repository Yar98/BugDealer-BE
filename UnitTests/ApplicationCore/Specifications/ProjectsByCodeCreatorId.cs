using Bug.Entities.Model;
using Bug.Data.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bug.Data.Specifications;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProjectsByCodeCreatorId
    {
        public string TestAccountId = "account-test";
        public string TestBadAccountId = "hihihi";
        public string TestBadCode = "badbad";
        public string TestCode = "BTS";
        public ProjectsByCodeCreatorId()
        {
        }

        [Fact]
        public void MatchIfCreatorIdAndCodeExist()
        {
            var spec = 
                new ProjectsByCodeCreatorIdSpecification(TestAccountId, TestCode);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void NotMatchIfCreatorIdNotExist()
        {
            var spec =
                new ProjectsByCodeCreatorIdSpecification(TestBadAccountId, TestCode);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfCodeNotExist()
        {
            var spec =
                new ProjectsByCodeCreatorIdSpecification(TestAccountId, TestBadCode);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfCreatorIdAndCodeNotExist()
        {
            var spec =
                new ProjectsByCodeCreatorIdSpecification(TestBadAccountId, TestBadCode);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .Any();
            Assert.False(result);
        }

        private List<Project> GetTestListProjects()
        {
            var projectTest = new TestProjectBuilder().Build();
            projectTest.UpdateCreatorId(TestAccountId);
            projectTest.UpdateCode(TestCode);

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
