using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TTCBDD.APIObjects
{
    public static class ODataExtensions
    {
        public static RestCall<T> Top<T>(this RestCall<T> call, int num) where T : new()
        {
            call.request.AddQueryParameter("$top", num.ToString(), false);
            return call;
        }
        public static RestCall<T> Skip<T>(this RestCall<T> call, int num) where T : new()
        {
            call.request.AddQueryParameter("$skip", num.ToString(), false);
            return call;
        }
        public static RestCall<T> Equals<T>(this RestCall<T> call, string key, string value) where T : new()
        {
            call.request.AddQueryParameter("$filter", $"{key} eq '{value}'", false);
            return call;
        }
        public static RestCall<T> Contains<T>(this RestCall<T> call, string key, string value) where T : new()
        {
            call.request.AddQueryParameter("$filter", $"contains({key}, '{value}')", false);
            return call;
        }
        public static RestCall<T> Expand<T>(this RestCall<T> call, string key) where T : new()
        {
            call.request.AddQueryParameter("$expand", key, false);
            return call;
        }
        public static RestCall<T> Select<T>(this RestCall<T> call, params string[] fields) where T : new()
        {
            var fieldsString = fields.Aggregate((str, curr) => str + "," + curr);
            call.request.AddQueryParameter("$select", fieldsString, false);
            return call;
        }
        public static RestCall<T> Select<T>(this RestCall<T> call, string fields) where T : new()
        {
            call.request.AddQueryParameter("$select", fields, false);
            return call;
        }
        public static RestCall<T> Select<T>(this RestCall<T> call, List<string> fields) where T : new()
        {
            call.request.AddQueryParameter("$select", fields.Aggregate((str, curr) => str + "," + curr), false);
            return call;
        }
        public static RestCall<T> SelectProperties<T>(this RestCall<T> call) where T : new()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanWrite && p.CanRead)
                .Select(p => p.Name).ToList();
            call.Select(properties);
            return call;
        }

        public static int Count(this RestCall<int> call)
        {
            call.request.AddQueryParameter("$count", "true", false);
            call.Traverse("@odata.count");
            var response = call.Execute();
            return response.Data;
        }
    }
}
