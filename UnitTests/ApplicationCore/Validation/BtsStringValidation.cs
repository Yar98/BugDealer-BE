using Bug.API.Utils;
using Bug.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class BtsStringValidation
    {
        public string TestPassword = "Pass@word123";
        public string TestUserName = "yarito123";
        public string TestEmail = "tienfu97@gmail.com";
        public BtsStringValidation() { }

        [Fact]
        public void NotMatchIfPasswordNotContainUperAlpha()
        {
            var result = StringHandler.ValidPassword("pass@word123");
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfPasswordNotContainLowerAlpha()
        {
            var result = StringHandler.ValidPassword("PASS@WORD123");
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfPasswordNotContainNumber()
        {
            var result = StringHandler.ValidPassword("Pass@word");
            Assert.False(result);
        }

        [Fact]
        public void NotMatchIfPasswordNotContainSpecial()
        {
            var result = StringHandler.ValidPassword("Password123");
            Assert.False(result);
        }

        [Fact]
        public void MatchIfPasswordValid()
        {
            var result = StringHandler.ValidPassword(TestPassword);
            Assert.True(result);
        }

        [Fact]
        public void NotMatchIfUserNameContainSpecial()
        {
            var result = StringHandler.ValidUserName("yarito!");
            Assert.False(result);
        }

        [Fact]
        public void MatchIfUserNameContainNumber()
        {
            var result = StringHandler.ValidUserName("yarito123");
            Assert.True(result);
        }

        [Fact]
        public void MatchIfUserNameOnlyContainLowerAlpha()
        {
            var result = StringHandler.ValidUserName("yarito");
            Assert.True(result);
        }

        [Fact]
        public void MatchIfUserNameOnlyContainUperAlpha()
        {
            var result = StringHandler.ValidUserName("YARITO");
            Assert.True(result);
        }

        [Fact]
        public void MatchIfUserNameValid()
        {
            var result = StringHandler.ValidUserName(TestUserName);
            Assert.True(result);
        }

        [Fact]
        public void MatchIfEmailValid()
        {
            var result = StringHandler.ValidEmail(TestEmail);
            Assert.True(result);
        }


    }
}
