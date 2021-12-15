using Bug.Entities.Model;
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
        IProjectBuilder AddStartDate(string date);
        IProjectBuilder AddEndDate(string date);
        IProjectBuilder AddDescription(string des);
        IProjectBuilder AddAvatarUri(string url);
        IProjectBuilder AddDefaultAssigneeId(string id);
        IProjectBuilder AddCreatorId(string id);
        IProjectBuilder AddState();
        IProjectBuilder AddTemplateId(string id);
        IProjectBuilder AddDefaultStatusId(string id);
        IProjectBuilder AddDefaultRoleId(string id);
        Project Build();
    }
}
