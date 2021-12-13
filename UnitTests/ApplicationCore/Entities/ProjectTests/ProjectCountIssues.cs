using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.ProjectTests
{
    public class ProjectCountIssues
    {
        public int TestOpenTagId = 1;
        public int TestInProgressTagId = 2;
        public int TestDoneTagId = 3;
        public int TestOtherTagId = 4;

        public ProjectCountIssues() { }

        [Fact]
        public void CountOnlyOneOpenIssue()
        {
            var project = new TestProjectBuilder().Build();
            project.UpdateIssues(GetTestListIssues());

            Assert.Equal(1, project.TotalOpenIssues);
        }

        [Fact]
        public void CountOnlyOneInProgressIssue()
        {
            var project = new TestProjectBuilder().Build();
            project.UpdateIssues(GetTestListIssues());

            Assert.Equal(1, project.TotalInProgressIssues);
        }

        [Fact]
        public void CountOnlyOneDoneIssue()
        {
            var project = new TestProjectBuilder().Build();
            project.UpdateIssues(GetTestListIssues());

            Assert.Equal(1, project.TotalDoneIssues);
        }

        [Fact]
        public void CountAllIssue()
        {
            var project = new TestProjectBuilder().Build();
            project.UpdateIssues(GetTestListIssues());

            Assert.Equal(4, project.TotalIssues);
        }

        [Fact]
        public void CountZeroIssues()
        {
            var project = new TestProjectBuilder().Build();

            Assert.Equal(0, project.TotalIssues);
        }

        private List<Issue> GetTestListIssues()
        {
            var issue1 = new Issue("issue1", "title1", 0, "des1", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, "environment1", "defaultStatus1", 1, null, "project1", "account1", "account1", null);
            var issue2 = new Issue("issue2", "title1", 0, "des1", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, "environment1", "defaultStatus1", 1, null, "project1", "account1", "account1", null);
            var issue3 = new Issue("issue3", "title1", 0, "des1", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, "environment1", "defaultStatus1", 1, null, "project1", "account1", "account1", null);
            var issue4 = new Issue("issue4", "title1", 0, "des1", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, null, null, "environment1", "defaultStatus1", 1, null, "project1", "account1", "account1", null);

            issue1.UpdateStatus(new Status(null, null, null, 0, null, TestOpenTagId));
            issue2.UpdateStatus(new Status(null, null, null, 0, null, TestInProgressTagId));
            issue3.UpdateStatus(new Status(null, null, null, 0, null, TestDoneTagId));
            issue4.UpdateStatus(new Status(null, null, null, 0, null, TestOtherTagId));

            return new List<Issue>()
            {
                issue1,
                issue2,
                issue3,
                issue4
            };
        }
    }
}
