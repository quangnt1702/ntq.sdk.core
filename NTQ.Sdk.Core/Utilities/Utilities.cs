using System;
using System.Text.RegularExpressions;

namespace NTQ.Sdk.Core.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Get the Vietnam current time
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrenDateTime()
        {
            return DateTime.UtcNow.AddHours(7);
        }

        /// <summary>
        /// Check Vietnam phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool CheckVnPhoneNumber(this string phoneNumber)
        {
            string strRegex = @"(^84|(0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$)";
            Regex regex = new Regex(strRegex);
            if (regex.IsMatch(phoneNumber))
                return true;
            return false;
        }
    }
}