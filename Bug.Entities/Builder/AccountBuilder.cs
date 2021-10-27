using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug.Entities.Builder
{
    public class AccountBuilder : IAccountBuilder
    {
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string ImageUri { get; private set; }
        public string TimezoneId { get; private set; }
        public IAccountBuilder AddCreatedDate(DateTime date)
        {
            CreatedDate = date;
            return this;
        }

        public IAccountBuilder AddEmail(string email)
        {
            Email = email;
            return this;
        }

        public IAccountBuilder AddFirstName(string fn)
        {
            FirstName = fn;
            return this;
        }

        public IAccountBuilder AddId(string id)
        {
            Id = id;
            return this;
        }

        public IAccountBuilder AddImageUri(string uri)
        {
            ImageUri = uri;
            return this;
        }

        public IAccountBuilder AddLastName(string ln)
        {
            LastName = ln;
            return this;
        }

        public IAccountBuilder AddPassword(string pw)
        {
            Password = pw;
            return this;
        }

        public IAccountBuilder AddTimezoneId(string id)
        {
            TimezoneId = id;
            return this;
        }

        public IAccountBuilder AddUserName(string name)
        {
            UserName = name;
            return this;
        }

        public Account Build()
        {
            return new Account(Id, UserName, Password, FirstName, LastName, Email, CreatedDate, ImageUri, TimezoneId);
        }
    }
}
