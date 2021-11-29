﻿using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.Data.Specifications;
using Bug.Entities.Builder;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class IssuelogService : IIssuelogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IssuelogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Issuelog> GetDetailIssuelogByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuelogSpecification(id);
            return await _unitOfWork
                .Issuelog
                .GetEntityBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdAsync
            (string issueId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuelogsByIssueIdSpecification(issueId);
            return await _unitOfWork
                .Issuelog
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdTagIdAsync
            (string issueId,
            int tagId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuelogsByIssueIdSpecification(issueId, tagId);
            return await _unitOfWork
                .Issuelog
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<IReadOnlyList<Issuelog>> GetIssuelogsByIssueIdCategoryIdAsync
            (string issueId,
            int categoryId,
            CancellationToken cancellationToken = default)
        {
            var specificationResult =
                new IssuelogsByIssueIdCategoryIdSpecification(issueId, categoryId);
            return await _unitOfWork
                .Issuelog
                .GetAllEntitiesBySpecAsync(specificationResult, cancellationToken);
        }

        public async Task<Issuelog> AddIssuelogAsync
            (IssuelogNormalDto ilog,
            CancellationToken cancellationToken = default)
        {
            var result = new IssuelogBuilder()
                .AddId()
                .AddIssueId(ilog.IssueId)
                .AddLogDate(ilog.LogDate)
                //.AddModifierId(ilog.ModifierId)
                .AddModPriorityId(ilog.ModPriorityId)
                .AddModStatusId(ilog.ModStatusId)
                .AddPrePriorityId(ilog.PrePriorityId)
                .AddPreStatusId(ilog.PreStatusId)
                .AddTagId(ilog.TagId)
                .Build();
            var modifier = await _unitOfWork.Account.GetByIdAsync(ilog.ModifierId, cancellationToken);
            result.UpdateModifier(modifier);
            await _unitOfWork.Issuelog.AddAsync(result, cancellationToken);
            _unitOfWork.Save();
            return result;
        }
    }
}
