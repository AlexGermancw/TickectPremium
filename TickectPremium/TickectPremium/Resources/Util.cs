using System;
using System.Collections.Generic;
using System.Text;

namespace TickectPremium.Resources
{
    public static class Util
    {
        public static bool AreStringsEqual(string str1, string str2)
        {
            return string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2) || str1 == str2;
        }
    }
}
