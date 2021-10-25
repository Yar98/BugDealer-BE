﻿using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public interface IProjectBuilder
    {
        IProjectBuilder AddId(string id);
        IProjectBuilder AddName(string name);
        IProjectBuilder AddCode(string code);
        IProjectBuilder AddStartDate(DateTime date);
        IProjectBuilder AddEndDate(DateTime date);
        IProjectBuilder AddRecentDate(DateTime date);
        IProjectBuilder AddDescription(string des);
        IProjectBuilder AddAvatarUri(string url);
        IProjectBuilder AddProjectType(string t);
        IProjectBuilder AddDefaultAssigneeId(string id);
        IProjectBuilder AddCreatorId(string id);
        IProjectBuilder AddWorkflowId(string id);
        Project Build();
    }
}