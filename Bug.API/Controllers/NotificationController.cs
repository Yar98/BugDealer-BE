using Bug.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug.Core.Common;
using Bug.API.Dto;

namespace Bug.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("paging/account/{accountId}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedNotificationByAccountId
            (string accountId,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _notificationService
                .GetPaginatedByByAccountIdAsync(accountId, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/account/{accountId}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetNotificationByAccountId
            (string accountId,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _notificationService
                .GetNextByOffsetByAccountIdAsync(accountId, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("paging/account/{accountId}/seen/{seen:bool}/{pageIndex:int}/{pageSize:int}/{sortOrder}")]
        public async Task<IActionResult> GetPaginatedNotificationByAccountIdSeen
            (string accountId,
            bool seen,
            int pageIndex,
            int pageSize,
            string sortOrder)
        {
            var result = await _notificationService
                .GetPaginatedByByAccountIdSeenAsync(accountId, seen, pageIndex, pageSize, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpGet("offset/account/{accountId}/seen/{seen:bool}/{offset:int}/{next:int}/{sortOrder}")]
        public async Task<IActionResult> GetNextByOffsetNotificationByAccountIdSeen
            (string accountId,
            bool seen,
            int offset,
            int next,
            string sortOrder)
        {
            var result = await _notificationService
                .GetNextByOffsetByAccountIdSeenAsync(accountId, seen, offset, next, sortOrder);
            return Ok(Bts.ConvertJson(result));
        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> PutUpdateNotification
            (int notificationId,
            [FromBody] NotificationNormalDto noti)
        {
            if (noti.Id != notificationId)
                return BadRequest();
            await _notificationService.UpdateNotificationByIdAsync(noti);
            return NoContent();
        }
    }
}
