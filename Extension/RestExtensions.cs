using System.Collections.Generic;

namespace TTCBDD.Extension
{
    public static class GenericExtensions
    {
        public static T Get<T>(this IDictionary<string, object> dict, string key)
        {
            //typecast dictionary result to T
            //and returning it
            T value = (T)dict[key];
            return value;
        }

        public static void StoreUrl(this IDictionary<string, object> dict, string url)
        {
            dict["url"] = url;
        }

        public static string GetUrl(this IDictionary<string, object> dict)
        {
            string url = (string)dict["url"];
            return url;
        }
    }
}