﻿using Bug.API.Dto;
using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bug.API.Services
{
    public interface IFieldSerivce
    {
        Task<Field> GetFieldByIdAsync
            (int id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Field>> GetAllFieldsAsync
            (CancellationToken cancellationToken = default);
        Task<IReadOnlyList<FieldNormalDto>> GetFieldsByAccountCustomtypeAsync
            (string accountId,
            int customtypeId,
            CancellationToken cancellationToken = default);
    }
}
