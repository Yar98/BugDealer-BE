using Bug.API.Dto;
using Bug.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Validation
{
    public class BtsToken
    {
        public string TestId = "id-test";
        public string TestName = "name-test";
        public string TestEmail = "email-test";
        public BtsToken() { }

        [Fact]
        public void ValidToken()
        {
            var token = new JwtUtils().GenerateToken(TestId, TestName, TestEmail);

            var result = new JwtUtils().ValidateToken(token);

            Assert.True(result != null);
        }

        [Fact]
        public void ShowRightDescirption()
        {
            var token = new JwtUtils().GenerateToken(TestId, TestName, TestEmail);

            var result = new JwtUtils().ValidateToken(token);

            Assert.Equal(TestId, result.Id);
            Assert.Equal(TestName, result.Name);
            Assert.Equal(TestEmail, result.Email);
        }
    }
}
