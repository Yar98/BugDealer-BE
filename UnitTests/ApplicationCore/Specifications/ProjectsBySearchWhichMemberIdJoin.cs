using Bug.Entities.Model;
using Bug.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;
using Bug.Data.Extensions;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProjectsBySearchWhichMemberIdJoin
    {
        public string TestAccountId = "account-test";
        public string TestName = "name-test";
        public string TestDescription = "description-test";
        public string TestCode = "code-test";
        public string TestTemplateName = "template-test";
        public string TestBadAccountId = "hahaha";
        public string TestBadSearch = "#@!@*#(*#)$";
        public int TestState = 1;

        [Fact]
        public void NotMatchIfAccountIdNotExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestBadAccountId, TestState, "t");
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfAccountIdAndCodeNotExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestBadAccountId, TestState, TestBadSearch);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfCodeNotExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestAccountId, TestState, TestBadSearch);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void MatchIfAccountIdAndTemplateSearchExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestAccountId, TestState, "template-");
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void MatchIfAccountIdAndNameSearchExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestAccountId, TestState, "name-");
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void MatchIfAccountIdAndCodeSearchExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestAccountId, TestState, "code-");
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void MatchIfAccountIdAndDescriptionSearchExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestAccountId, TestState, "description-");
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void MatchIfAccountIdAndSearchExist()
        {
            var spec =
                new ProjectsBySearchWhichMemberIdJoinSpecification(TestAccountId, TestState, "test");
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
            projectTest.AccountProjectRoles.Add(new AccountProjectRole(TestAccountId,null,0));
            projectTest.UpdateCode(TestCode);
            projectTest.UpdateName(TestName);
            projectTest.UpdateDescription(TestDescription);
            projectTest.UpdateTemplate(new Template(0, TestTemplateName, null));

            var project1 = new TestProjectBuilder().Build();
            project1.AccountProjectRoles.Add(new AccountProjectRole(TestAccountId, null, 0));
            project1.UpdateTemplate(new Template(0, "temp1", null));
            var project2 = new TestProjectBuilder().Build();
            project2.AccountProjectRoles.Add(new AccountProjectRole(TestAccountId, null, 0));
            project2.UpdateTemplate(new Template(0, "temp2", null));

            return new List<Project>
            {
                project1,
                project2,
                projectTest
            };
        }
    }
}
