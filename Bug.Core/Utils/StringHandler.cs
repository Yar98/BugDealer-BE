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
                new Regex(@"^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ\s\d]{1,255}$");
            return regex.IsMatch(username);
        }

        //valid last name and first name
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

        public static bool ValidCode(string code)
        {
            var regex =
                new Regex(@"^[A-Z\d]{2,10}$");
            return regex.IsMatch(code);
        }

        public static bool ValidTimeTracking(string tiime)
        {
            var regex =
                new Regex(@"^(?!.*([wdhm]).*\1)(?!.*m.*d.*h)(?!.*m.*h.*d)(?!.*h.*d.*m)(?!.*h.*m.*d)(?!.*d.*m.*h)(?!.*m.*d)(?!.*m.*h)(?!.*h.*d)\d*\.?\d+[wdhm](?: \d*\.?\d+[dhm])*$");
            return regex.IsMatch(tiime);
        }
    }
}
