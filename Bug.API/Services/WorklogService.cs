using Bug.API.Dto;
using Bug.Data.Infrastructure;
using Bug.Data.Specifications;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class WorklogService : IWorklogService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorklogService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<Worklog> GetDetailWorklogByIdAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork
                .Worklog
                .GetByIdAsync(id, cancellationToken);
        }

        public async Task<Worklog> AddNewWorklogToIssueAsync
            (string issueId,
            WorklogPostDto worklog,
            CancellationToken cancellationToken = default)
        {
            var result = new Worklog(0, worklog.SpentTime, worklog.LogDate, issueId, worklog.LoggerId);
            await _unitOfWork
                .Worklog
                .AddAsync(result, cancellationToken);
            var issue = await _unitOfWork
                .Issue
                .GetByIdAsync(issueId, cancellationToken);
            issue
                .UpdateRemainEstimateTime(worklog.RemainTime, worklog.LoggerId, async log=> { await _unitOfWork.Issuelog.AddAsync(log, cancellationToken); });
            _unitOfWork.Save();
            return result;
        }

        public async Task DeleteWorklogAsync
            (int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Worklog
                .GetByIdAsync(id, cancellationToken);
            _unitOfWork.Worklog.Delete(result);

            _unitOfWork.Save();
        }
    }
}
