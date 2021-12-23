using Bug.API.Services;
using Bug.Data;
using Bug.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.ProjectRepoTests
{
    public class ProjectAddRole
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BugContext _bugContext;
        private readonly ITestOutputHelper _output;

        public ProjectAddRole(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<BugContext>()
                .UseInMemoryDatabase(databaseName: "TestBugDb")
                .Options;
            _bugContext = new BugContext(dbOptions);
            _unitOfWork = new UnitOfWork(_bugContext, null);
        }

        [Fact]
        public async Task AddRole()
        {
            // seed data
            await _unitOfWork.Account.AddAsync(new TestAccountBuilder().Build());
            await _unitOfWork.Template.AddAsync(new TestTemplateBuilder().Build());

            // start test
            var role = new TestRoleBuilder().Build();
            await _unitOfWork.Role.AddAsync(role);
            _output.WriteLine($"RoleId: {role.Id}");
            _unitOfWork.Save();

            var projectService = new ProjectService(_unitOfWork);
            var project = await projectService
                .AddProjectAsync(new TestProjectBuilder().BuildPostDto());
            
            await projectService.AddRoleToProjectAsync(project.Id, role.Id);
            var resultProject = await projectService
                .GetDetailProjectAsync(project.Id);

            Assert.NotEqual(0, role.Id);
            Assert.True(resultProject.Roles.Contains(role));
            
        }
    }
}
