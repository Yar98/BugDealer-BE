using Bug.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Bug.Data.Infrastructure;
using Bug.Core.Common;

namespace Bug.API.ActionFilter
{
    public class RoleFilter : ActionFilterAttribute
    {
        
    }
}
