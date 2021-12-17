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
    public class UpdateBasicToProject
    {
        private readonly Mock<IUnitOfWork> _mockRepo;
        public UpdateBasicToProject()
        {
            _mockRepo = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task InvokeSaveOnce()
        {
            _mockRepo
                .Setup(mock => mock.Project.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(new TestProjectBuilder().Build());
            _mockRepo
                .Setup(mock => mock.Project.Update(It.IsAny<Project>()));

            var projectService = new ProjectService(_mockRepo.Object);
            await projectService
                .UpdateBasicProjectAsync(new TestProjectBuilder().BuildPutDto(), default);

            _mockRepo.Verify(mock => mock.Save(), Times.Once);
        }
    }
}
