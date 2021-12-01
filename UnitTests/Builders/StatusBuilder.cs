using Bug.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class StatusBuilder
    {
        private readonly Status _status;
        public string TestName = "testname";
        public string TestDescription = "test description";
        public int TestProgress = 100;
        public bool TestDefault = false;
        public string TestCreatorId = "account1";
        public int TestTagId = 1;

        public StatusBuilder()
        {
            var id = Guid.NewGuid().ToString();
            _status = new Status(id, TestName, TestDescription, TestProgress, TestCreatorId, TestTagId);
        }

        public Status Build()
        {
            return _status;
        }
    }
}
