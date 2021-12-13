using Bug.API.Dto;
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
    public class UpdateStatusesToProject
    {
        private readonly Mock<IUnitOfWork> _mockRepo;
        public UpdateStatusesToProject()
        {
            _mockRepo = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task InvokeSaveOnce()
        {
            _mockRepo
                .Setup(mock => mock.Project.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(It.IsAny<Project>());
            _mockRepo
                .Setup(mock => mock.Status.GetStatusesFromMutiIdsAsync(It.IsAny<List<string>>(), default))
                .ReturnsAsync(It.IsAny<List<Status>>());
            _mockRepo
                .Setup(mock => mock.Issue.UpdateIssuesHaveDumbStatus(It.IsAny<List<Status>>()));
            _mockRepo
                .Setup(mock => mock.Status.GetDefaultStatusesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(It.IsAny<List<Status>>());
            _mockRepo
                .Setup(mock => mock.Project.Update(It.IsAny<Project>()));
            
            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.UpdateStatusesOfProjectAsync(It.IsAny<ProjectPutStatusesDto>(), default);

            _mockRepo.Verify(mock => mock.Save(), Times.Once);
        }
    }
}
