using Bug.Data.Infrastructure;
using Bug.Entities.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IIssueBuilder _issueBuilder;
        public IssueService(IUnitOfWork uow, IIssueBuilder pd)
        {
            _unitOfWork = uow;
            _issueBuilder = pd;
        }


    }
}
