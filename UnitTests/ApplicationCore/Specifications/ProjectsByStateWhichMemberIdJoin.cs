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
    public class ProjectsByStateWhichMemberIdJoin
    {
        public string TestMemberId = "member-test";
        public string TestBadMemberId = "badmember-test";
        public int TestState = 1;
        public int TestBadState = 0;
        public ProjectsByStateWhichMemberIdJoin() { }

        [Fact]
        public void MatchIfMemberIdAndStateExist()
        {
            var spec =
                new ProjectsByStateWhichMemberIdJoinSpecification(TestMemberId, TestState);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.True(result);
        }

        [Fact]
        public void NotMatchIfWrongState()
        {
            var spec =
                new ProjectsByStateWhichMemberIdJoinSpecification(TestMemberId, TestBadState);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfMemberIdNotExist()
        {
            var spec =
                new ProjectsByStateWhichMemberIdJoinSpecification(TestBadMemberId, TestState);
            var result = GetTestListProjects()
                .AsQueryable()
                .Specify(spec)
                .ToList()
                .Any();
            Assert.False(result);
        }

        private List<Project> GetTestListProjects()
        {
            var projectTest = new TestProjectBuilder().Build();
            projectTest.AccountProjectRoles.Add(new AccountProjectRole(TestMemberId,null,TestState));
            projectTest.UpdateState(TestState);

            var project1 = new TestProjectBuilder().Build();
            project1.AccountProjectRoles.Add(new AccountProjectRole(project1.Id, null, 0));
            var project2 = new TestProjectBuilder().Build();
            project2.AccountProjectRoles.Add(new AccountProjectRole(project1.Id, null, 0));

            return new List<Project>
            {
                project1,
                project2,
                projectTest
            };
        }
    }
}
