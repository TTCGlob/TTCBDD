using System.Net;
using RestSharp;

namespace TTCBDD.ComponentHelper
{
    public static class GenericExtensions
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
