using Bug.API.Services;
using Bug.Data.Infrastructure;
using Bug.Entities.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Services.ProjectServicesTests
{
    public class AddMemberToProject
    {
        public string TestAccountId = "account1";
        public string TestProjectId = "project1";
        private readonly Mock<IUnitOfWork> _mockRepo;
        public AddMemberToProject()
        {
            _mockRepo = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task InvokeSaveOnce()
        {
            _mockRepo
                .Setup(mock => mock.AccountProjectRole.AddAsync(It.IsAny<AccountProjectRole>(), default))
                .ReturnsAsync(It.IsAny<AccountProjectRole>());
            var projectService = new ProjectService(_mockRepo.Object);
            await projectService.AddMemberToProjectAsync(TestAccountId, TestProjectId, default);

            _mockRepo.Verify(mock => mock.Save(), Times.Once);
        }
    }
}
