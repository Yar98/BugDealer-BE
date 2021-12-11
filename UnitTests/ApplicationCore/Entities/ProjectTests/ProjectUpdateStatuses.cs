using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bug.Entities.Model;
using Moq;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.ProjectTests
{
    public class ProjectUpdateStatuses
    {
        public List<Status> TestStatuses = new();

        public ProjectUpdateStatuses() { }

        [Fact]
        public void UpdateAllStatuses()
        {
            var project = new TestProjectBuilder().Build();
            Assert.Equal(0, project.Statuses.Count);

            TestStatuses.Add(new TestStatusBuilder().Build());
            TestStatuses.Add(new TestStatusBuilder().Build());
            project.UpdateStatuses(TestStatuses);

            Assert.Equal(2, project.Statuses.Count);
            Assert.DoesNotContain(project.Statuses, s => !TestStatuses.Contains(s));
        }

        [Fact]
        public void AddDefaultOnlyIfEmpty()
        {
            var project = new TestProjectBuilder().Build();
            TestStatuses.Add(new TestStatusBuilder().Build());
            project.UpdateStatuses(TestStatuses);
            Assert.Equal(1, project.Statuses.Count);

            project.AddDefaultStatuses(TestStatuses);

            Assert.Equal(1, project.Statuses.Count);
        }
    }
}
