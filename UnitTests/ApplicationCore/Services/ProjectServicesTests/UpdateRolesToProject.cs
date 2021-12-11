using Bug.API.Dto;
using Bug.API.Services;
using Bug.Data.Infrastructure;
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

namespace UnitTests.ApplicationCore.Services.ProjectServicesTests
{
    public class UpdateRolesToProject
    {
        private readonly Mock<IUnitOfWork> _mockRepo;
        public UpdateRolesToProject()
        {
            _mockRepo = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task InvokeSaveOnce()
        {
            _mockRepo
                .Setup(mock => mock.Project.GetEntityBySpecAsync(It.IsAny<ProjectSpecification>(), default))
                .ReturnsAsync(It.IsAny<Project>());
            _mockRepo
                .Setup(mock => mock.Role.GetRolesFromMutiIdsAsync(It.IsAny<List<int>>(), default))
                .ReturnsAsync(It.IsAny<List<Role>>());
            _mockRepo
                .Setup(mock => mock.Role.GetDefaultRolesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(It.IsAny<List<Role>>());
            _mockRepo
                .Setup(mock => mock.AccountProjectRole.UpdateMultiByRoleIdProjectId(It.IsAny<string>(), It.IsAny<List<Role>>()));

            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.UpdateRolesOfProjectAsync(new TestProjectBuilder().BuildPutDto(), default);

            _mockRepo.Verify(mock => mock.Save(), Times.Once);
        }
    }
}
