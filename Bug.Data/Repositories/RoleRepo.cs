﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bug.Core.Utils;
using Bug.Data.Extensions;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Bug.Data.Repositories
{
    public class RoleRepo : EntityRepoBase<Role>, IRoleRepo
    {
        public RoleRepo(BugContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<List<Role>> GetRolesFromMutiIdsAsync
            (List<int> list,
            CancellationToken cancellationToken = default)
        {
            var result = await _bugContext
                .Roles
                .AsQueryable()
                .Where(p => list.Contains(p.Id))
                .AsTracking()
                .ToListAsync(cancellationToken);
            return result??new List<Role>();
        }

        public async Task<IReadOnlyList<Role>> GetDefaultRolesNoTrackAsync
            (string sortOrder,
            string creatorId = "bts",
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(creatorId);
            return await GetAllEntitiesNoTrackBySpecAsync
                (specificationResult, sortOrder, cancellationToken);

        }

        public async Task<IReadOnlyList<Role>> GetDefaultRolesAsync
            (string sortOrder, 
            string creatorId = "bts",
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new RoleByCreatorIdSpecification(creatorId);
            return await GetAllEntitiesBySpecAsync
                (specificationResult, sortOrder, cancellationToken);

        }

        public override IQueryable<Role> SortOrder
            (IQueryable<Role> result,
            string sortOrder)
        {
            switch (sortOrder)
            {
                case "name":
                    result = result.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    result = result.OrderByDescending(p => p.Name);
                    break;
                case "description_desc":
                    result = result.OrderByDescending(p => p.Description);
                    break;
                case "description":
                    result = result.OrderBy(p => p.Description);
                    break;                
                default:
                    result = result.OrderBy(p => p.Id);
                    break;
            }
            return result;
        }
    }
}
