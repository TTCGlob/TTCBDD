﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.ComponentHelper
{
    class StefRestHelper
    {
        public static RestClient client;
        public static RestRequest request;
        //private static string fullPath;
        public static string url;
        private static string baseUrl = "http://dummy.restapiexample.com/";

        public static RestClient SetURL(string endpoint)
        {
            url = baseUrl + endpoint;
            return client = new RestClient(url);
        }

        public static RestRequest CreateRequest()
        {
            request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            return request;
        }
        public static RestRequest CreateRequest(string id)
        {
            var resource = url + "/{id}";

            request = new RestRequest(resource, Method.GET);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddHeader("Accept", "application/json");
            return request;
        }

        public static IRestResponse GetResponse()
        {
            return client.Execute(request);

        }

    }
}
