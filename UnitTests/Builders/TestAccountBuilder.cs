using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class TestAccountBuilder
    {
        private readonly Account _account;
        public string TestId = "account1";
        public string TestUserName = "testname";
        public string TestPassword = "Pass@word123";
        public string TestFirstName = "yarito";
        public string TestLastName = "yarito";
        public string TestEmail = "email@email.com";
        public string TestLanguage = "vn";
        public DateTimeOffset TestCreatedDate = DateTime.Now;
        public string TestImageUri = "uri.uri";
        public string TestTimezone = null;

        public TestAccountBuilder()
        {
            _account = new Account(TestId, TestUserName, TestPassword, TestFirstName, TestLastName, TestEmail, TestCreatedDate, TestLanguage, TestImageUri, TestTimezone);

        }

        public Account Build()
        {
            return _account;
        }
    }
}
