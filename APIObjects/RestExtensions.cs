using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TTCBDD.APIObjects
{
    public static class RestExtensions
    {
        public static bool ContentContains<T>(this IRestResponse<T> response, string text)
        {
            return response.Content.Contains(text);
        }
        public static bool ResponseOK<T>(this IRestResponse<T> response)
        {
            return response.StatusCode == HttpStatusCode.OK;
        }
        public static bool HasData<T>(this IRestResponse<T> response)
        {
            return response.Data != null;
        }
    }
}
