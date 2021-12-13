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
            var result = new Worklog(0, worklog.SpentTime, worklog.RemainTime, worklog.LogDate, issueId, worklog.LoggerId);
            return await _unitOfWork
                .Worklog
                .AddAsync(result, cancellationToken);
        }

        public async Task UpdateWorklogAsync
            (WorklogPutDto worklog,
            CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork
                .Worklog
                .GetByIdAsync(worklog.Id, cancellationToken);
            result.UpdateLogDate(worklog.LogDate);
            result.UpdateLoggerId(worklog.LoggerId);
            result.UpdateRemainTime(worklog.RemainTime);
            result.UpdateSpentTime(worklog.SpentTime);

            _unitOfWork.Save();
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
