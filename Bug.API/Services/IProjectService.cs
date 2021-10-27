﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.API.Services.DTO;
using Bug.Core.Utility;
using Bug.Entities.Model;

namespace Bug.API.Services
{
    public interface IProjectService
    {
        Task<IReadOnlyList<ProjectLowDto>> GetRecentProjects(
            string accountId, int categoryId, string tagName, int count);
        Task<ProjectNormalDto> AddProject(ProjectNormalDto pro);
        Task<ProjectDetailDto> GetDetailProject(string id);
        Task<ProjectNormalDto> GetNormalProject(string projectId);
        Task<ProjectsPaginatedListDto<ProjectNormalDto>> GetPaginatedProjects(
            string creatorId,
            int pageIndex, int pageSize,
            int categoryId, string tagName,
            string sortOrder,
            int accountType);
        Task<IReadOnlyList<ProjectNormalDto>> GetNextProjectsByOffset(
            string creatorId,
            int offset, int next,
            int categoryId, string tagName,
            string sortOrder,
            int accountType);
        Task UpdateDetailProject(ProjectDetailDto pro);
        Task DeleteProject(string projectId);
    }
}
