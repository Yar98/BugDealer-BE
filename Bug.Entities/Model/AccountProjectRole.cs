﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class AccountProjectRole : IEntityBase
    {
        public string AccountId { get; set; }
        public Account Account { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        [ConcurrencyCheck]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        private AccountProjectRole() { }

        public AccountProjectRole
            (string accountId,
            string projectId,
            int roleId)
        {
            AccountId = accountId;
            ProjectId = projectId;
            RoleId = roleId;
        }
    }
}
