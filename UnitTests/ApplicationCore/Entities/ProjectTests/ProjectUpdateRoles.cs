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
    public class ProjectUpdateRoles
    {
        public List<Role> TestRoles = new();

        public ProjectUpdateRoles()
        {

        }

        [Fact]
        public void UpdateAllRoles()
        {
            var project = new TestProjectBuilder().Build();
            Assert.Equal(0, project.Roles.Count);

            TestRoles.Add(new TestRoleBuilder().Build());
            TestRoles.Add(new TestRoleBuilder().Build());
            project.UpdateRoles(TestRoles);

            Assert.Equal(2, project.Roles.Count);
            Assert.DoesNotContain(project.Roles, r => !TestRoles.Contains(r));
        }

        [Fact]
        public void AddDefaultOnlyIfEmpty()
        {
            var project = new TestProjectBuilder().Build();
            TestRoles.Add(new TestRoleBuilder().Build());
            project.UpdateRoles(TestRoles);
            Assert.Equal(1, project.Roles.Count);

            project.AddDefaultRoles(TestRoles);

            Assert.Equal(1, project.Roles.Count);
        }
    }
}
