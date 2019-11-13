using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace TTCBDD.ComponentHelper
{
    public class RestHelper
    {
        private static object rnd;

        public static string RestGet(string Url, string request, string checkstatus)
        {
            //Creating Client connection 
            RestClient restClient = new RestClient(Url);

            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(request, Method.GET);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);
            string response = restResponse.Content;

            // Checking Response on highlevel
            if (!response.Contains(checkstatus))
            {
                AssertHelper.AssertFailMsg("Get Failed for " + Url + request);
                Console.WriteLine("Get Failed for " + Url + request);
                return "Get Failed for " + Url + request;
            }
            else
            {
                Console.WriteLine("Get Response is" + response);
                return response;
            }

        }

        [Obsolete]
        public static string RestPost(string Url, string request, string checkstatus, string BodyMsg)
        {
            //Creating Client connection 
            RestClient restClient = new RestClient(Url);

            //Creating request to post data to server
            RestRequest restRequest = new RestRequest(request, Method.POST);


            //Adding Body
            restRequest.Parameters.Clear();
            restRequest.AddParameter("application/json", BodyMsg, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            string response = restResponse.Content;

            // Checking Response on highlevel
            if (!response.Contains(checkstatus))
            {
                AssertHelper.AssertFailMsg("Post method failed" + response);
                Console.WriteLine("Post Failed " + response);
                return "Post Failed " + response;
            }
            else
            {
                Console.WriteLine("Post is success " + response);
                return response;
            }

        }

        [Obsolete]
        public static string RestPut(string Url, string request, string checkstatus, string BodyMsg)
        {
            //Creating Client connection 
            RestClient restClient = new RestClient(Url);

            //Creating request to post data to server
            RestRequest restRequest = new RestRequest(request, Method.PUT);


            //Adding Body
            restRequest.Parameters.Clear();
            restRequest.AddParameter("application/json", BodyMsg, ParameterType.RequestBody);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            string response = restResponse.Content;

            // Checking Response on highlevel
            if (!response.Contains(checkstatus))
            {
                AssertHelper.AssertFailMsg("Put Failed " + response);
                Console.WriteLine("Put Failed " + response);
                return "Put Failed " + response;
            }
            else
            {
                Console.WriteLine("Put is success " + response);
                return response;
            }

        }

        public static string RestDel(string Url, string request, string checkstatus)
        {
            //Creating Client connection 
            RestClient restClient = new RestClient(Url);

            //Creating request to get data from server
            RestRequest restRequest = new RestRequest(request, Method.DELETE);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);
            string response = restResponse.Content;

            // Checking Response on highlevel
            if (!response.Contains(checkstatus))
            {
                AssertHelper.AssertFailMsg("Del Failed for " + Url + request);
                Console.WriteLine("Del Failed for " + Url + request);
                return "Del Failed for " + Url + request;
            }
            else
            {
                Console.WriteLine("Del Response is" + response);
                return response;
            }

        }

        public static string ParseJasonObj(string response, string TagToRead)
        {
            JObject json = JObject.Parse(response);
            string Id = json.Last.Last.ToString().Replace("{", "").Replace("}", "").Trim();
            return "";


        }

        public static string CreateJsonRequest()
        {
            Random rnd = new Random();
            string name = "test" + rnd.Next(3000,99999);
            return "{\"name\":\"" + name + "\",\"salary\":\""+ rnd.Next(100, 999) + "\",\"age\":\"23\"}";
        }
    }
 }
