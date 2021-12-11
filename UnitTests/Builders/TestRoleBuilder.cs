using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class TestRoleBuilder
    {
        private readonly Role _role;
        public int TestId = 1;
        public string TestName = "tester";
        public string TestDescription = "test description";
        public string TestCreatorId = "account1";

        public TestRoleBuilder()
        {
            _role = new Role(0, TestName, TestDescription, TestCreatorId);
        }

        public Role Build()
        {
            return _role;
        }
    }
}
