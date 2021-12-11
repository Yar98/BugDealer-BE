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
    public class ProjectAddRole
    {
        private readonly int _testRoleId = 1;
        private readonly string _testName = "Tester";
        private readonly string _testDescription = "Test Role Description";
        private readonly string _testCreatorId = "account1";

        [Fact]
        public void AddRoleIfNotExist()
        {
            var project = new TestProjectBuilder().Build();
            var role = new Role(1, _testName, _testDescription, _testCreatorId);
            project.AddExistRole(role);

            var result = project.Roles.Single();
            Assert.Equal(_testRoleId, result.Id);
            Assert.Equal(_testName, result.Name);
            Assert.Equal(_testDescription, result.Description);
            Assert.Equal(_testCreatorId, result.CreatorId);
        }

        
    }
}
