﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Model
{
    public class Project : IEntityBase
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; }
        public string CreatorId { get; private set; }
        public Account Creator { get; private set; }
        public string WorkflowId { get; private set; }
        public Workflow Workflow { get; private set; }
        public ICollection<Tag> Tags { get; private set; }

        private readonly List<Account> _accounts = new List<Account>();
        public ICollection<Account> Accounts => _accounts.AsReadOnly();
        public ICollection<Role> Roles { get; private set; }
        private Project() { }
        public Project(string id,
            string name,
            DateTime startDate,
            DateTime endDate,
            string description,
            string creatorId,
            string workflowId)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            CreatorId = creatorId;
            WorkflowId = workflowId;
        }

        public void AddAccount(Account a,string id,
            string userName,
            string password,
            string firstName,
            string lastName,
            string email,
            DateTime createdDate,
            string imageUri)
        {
            if (!Accounts.Any(i => i.Id.Equals(id)))
            {
                _accounts.Add(new Account(id, userName, password, firstName, lastName, email, createdDate, imageUri));
                //_accounts.Add(a);
                return;
            }
        }
    }
}