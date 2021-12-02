using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bug.Core.Utils
{
    public static class StringHandler
    {
        public static bool ValidPassword(string pass)
        {
            Regex regex = 
                new Regex(@"(?=.*[\^\$\*.\[\]\{\}\(\)\?\-\x22\!\@#%&/\\,<>':;|_~`+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z])");
            return regex.IsMatch(pass);
        }

        public static bool ValidUserName(string username)
        {
            var regex =
                new Regex(@"^[^\^\$\*.\[\]\{\}\(\)\?\-\x22\!\@#%&/\\,<>':;|_~`+=]+$");
            return regex.IsMatch(username);
        }

        public static bool ValidName(string name)
        {
            var regex =
                new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(name);
        }

        public static bool ValidEmail(string email)
        {
            var regex =
                new Regex(@"^[a-zA-Z0-9]+\@([a-z]+\.[a-z]+)+$");
            return regex.IsMatch(email);
        }
    }
}
