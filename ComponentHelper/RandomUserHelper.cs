using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.APIObjects;
using TTCBDD.PageObject;

namespace TTCBDD.ComponentHelper
{
    public static class RandomUserHelper
    {
        public static string url = "https://randomuser.me/api/";
        public static RandomUser GetRandomUser()
        {
            var response = new RestCall<RandomUser>(Method.GET, url, "")
                .Data();
            return response;
        }
    }
}
