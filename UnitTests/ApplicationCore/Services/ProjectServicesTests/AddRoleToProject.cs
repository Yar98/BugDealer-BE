using Bug.API.Services;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Services.ProjectServicesTests
{
    public class AddRoleToProject
    {
        private readonly int _testRoleId = 1;
        private readonly string _testName = "Tester";
        private readonly string _testDescription = "Test Role Description";
        private readonly string _testCreatorId = "account1";
        private readonly Mock<IUnitOfWork> _mockRepo;

        public AddRoleToProject()
        {
            _mockRepo = new Mock<IUnitOfWork>();           
        }

        [Fact]
        public async Task InvokeSaveOnlyOnce()
        {
            var project = new ProjectBuilder().Build();
            var role = new Role(_testRoleId, _testName, _testDescription, _testCreatorId);
            _mockRepo
                .Setup(mock => mock.Project.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(project);
            _mockRepo
                .Setup(mock => mock.Role.GetByIdAsync(It.IsAny<int>(), default))
                .ReturnsAsync(role);

            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.AddRoleToProjectAsync(project.Id, role.Id, default);

            _mockRepo.Verify(mock=>mock.Save(),Times.Once);
        }
    }
}
