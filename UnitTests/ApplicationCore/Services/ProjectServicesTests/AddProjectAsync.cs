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
using Bug.Entities.Builder;
using CoreProjectBuilder = Bug.Entities.Builder.ProjectBuilder;
using TestProjectBuilder = UnitTests.Builders.TestProjectBuilder;
using TestAccountBuilder = UnitTests.Builders.TestAccountBuilder;

namespace UnitTests.ApplicationCore.Services.ProjectServicesTests
{
    public class AddProjectAsync
    {
        private readonly Mock<IUnitOfWork> _mockRepo;
        public List<Role> TestRoles = new();
        public List<Status> TestStatuses = new();

        public AddProjectAsync()
        {
            _mockRepo = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task InvokeGetByIdOnce()
        {
            var acc = new TestAccountBuilder().Build();
            
            var proDto = new TestProjectBuilder().BuildPostDto();
            _mockRepo
                .Setup(mock => mock.Account.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(acc);
            _mockRepo
                .Setup(mock => mock.Role.GetDefaultRolesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestRoles);
            _mockRepo
                .Setup(mock => mock.Status.GetDefaultStatusesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestStatuses);
            _mockRepo
                .Setup(mock => mock.Project.AddAsync(It.IsAny<Project>(), default))
                .ReturnsAsync(It.IsAny<Project>());

            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.AddProjectAsync(proDto);

            _mockRepo
                .Verify(mock => mock.Account.GetByIdAsync(acc.Id, default), Times.Never);
        }

        [Fact]
        public async Task InvokeGetDefaultRolesOnce()
        {
            var acc = new TestAccountBuilder().Build();

            var proDto = new TestProjectBuilder().BuildPostDto();
            _mockRepo
                .Setup(mock => mock.Account.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(acc);
            _mockRepo
                .Setup(mock => mock.Role.GetDefaultRolesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestRoles);
            _mockRepo
                .Setup(mock => mock.Status.GetDefaultStatusesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestStatuses);
            _mockRepo
                .Setup(mock => mock.Project.AddAsync(It.IsAny<Project>(), default))
                .ReturnsAsync(It.IsAny<Project>());
            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.AddProjectAsync(proDto);

            _mockRepo
                .Verify(mock => mock.Role.GetDefaultRolesAsync("bts", default), Times.Once);
        }

        [Fact]
        public async Task InvokeGetDefaultStatusesOnce()
        {
            var acc = new TestAccountBuilder().Build();

            var proDto = new TestProjectBuilder().BuildPostDto();
            _mockRepo
                .Setup(mock => mock.Account.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(acc);
            _mockRepo
                .Setup(mock => mock.Role.GetDefaultRolesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestRoles);
            _mockRepo
                .Setup(mock => mock.Status.GetDefaultStatusesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestStatuses);
            _mockRepo
                .Setup(mock => mock.Project.AddAsync(It.IsAny<Project>(), default))
                .ReturnsAsync(It.IsAny<Project>());
            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.AddProjectAsync(proDto);

            _mockRepo
                .Verify(mock => mock.Status.GetDefaultStatusesAsync("bts", default), Times.Once);
        }

        [Fact]
        public async Task InvokeSaveOnce()
        {
            var acc = new TestAccountBuilder().Build();

            var proDto = new TestProjectBuilder().BuildPostDto();
            _mockRepo
                .Setup(mock => mock.Account.GetByIdAsync(It.IsAny<string>(), default))
                .ReturnsAsync(acc);
            _mockRepo
                .Setup(mock => mock.Role.GetDefaultRolesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestRoles);
            _mockRepo
                .Setup(mock => mock.Status.GetDefaultStatusesAsync(It.IsAny<string>(), default))
                .ReturnsAsync(TestStatuses);
            _mockRepo
                .Setup(mock => mock.Project.AddAsync(It.IsAny<Project>(), default))
                .ReturnsAsync(It.IsAny<Project>());
            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.AddProjectAsync(proDto);

            _mockRepo
                .Verify(mock => mock.Save(), Times.Once);
        }
    }
}
