using System.Collections.Generic;
using System.Net;
using RestSharp;
using TTCBDD.PageObject;

namespace TTCBDD.ComponentHelper
{
    public static class GenericExtensions
    {
        public static T Get<T>(this IDictionary<string, object> dict, string key)
        {
            //typecast dictionary result to T
            //and returning it
            T value = (T) dict[key];
            return value;
        }

        public static void Add<T>(this IDictionary<string, object> dict, string key, T value)
        {
            dict.Add(key, value);
        }
    }
}