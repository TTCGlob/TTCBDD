using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTCBDD.UtilityClasses
{
    public class StringUtils
    {
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        public static string GetRandomNameStringTitleCase()
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            string path = myTI.ToTitleCase(Regex.Replace(Path.GetRandomFileName(), @"[\d-]", string.Empty));
            int index = path.IndexOf(".");
            if (index > 0)
                path = path.Substring(0, index);

            return path;
        }

        public static string GetRandomBusinessType()
        {
            string[] types = {"Inc", "Co", "Ltd", "Limited", "Tapui (Limited)", "Limited Partnership", "LP", "LLC", "Trust"};
            return types[new Random().Next(0, types.Count())];
        }
    }
}
