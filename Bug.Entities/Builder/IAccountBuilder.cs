using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public interface IAccountBuilder
    {
        IAccountBuilder AddId(string id);
        IAccountBuilder AddUserName(string name);
        IAccountBuilder AddPassword(string pw);
        IAccountBuilder AddFirstName(string fn);
        IAccountBuilder AddLastName(string ln);
        IAccountBuilder AddEmail(string email);
        IAccountBuilder AddCreatedDate(DateTime date);
        IAccountBuilder AddImageUri(string uri);
        IAccountBuilder AddLanguage(string lan);
        IAccountBuilder AddTimezoneId(string id);
        Account Build();
    }
}
