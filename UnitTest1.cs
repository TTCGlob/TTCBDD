using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Net.Http;
using TTCBDD.ComponentHelper;
using TTCBDD.Settings;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
using TTCBDD.ComponentHelper;
using System.Collections.Generic;
using System.Linq;
using TTCBDD.PageObject;

namespace TTCBDD
{
    [TestFixture]
    public class UnitTest1
    {
        //private object quot;

        //public string JsonHelper { get; private set; }

        [Test]
        
        public void TestRestGet()
        {
            var call = new RestCall<Employee>(Method.GET, "http://dummy.restapiexample.com/api/v1", "/employee/{id}")
                .AddHeader("Accept", "application/json")
                .AddUrlParameter("id", "1")
                .Execute();
            Console.WriteLine($"{call.Content}");
            var employees = call.Data;
            Console.WriteLine(employees.id);
            Assert.IsTrue(call.StatusDescription.Equals("OK"));
        }
        [Test]
        public void TestRestPost()
        {
            var employee = new Employee("jasonnnnnn", "2333", "33");
            var call = new RestCall<Employee>(Method.POST, "http://dummy.restapiexample.com/api/v1", "/{resource}")
                .AddHeader("Accept", "application/json")
                .AddUrlParameter("resource", "create")
                .AddPayload(employee);
            var response = call.Execute();
            var data = response.Data;
            Assert.IsNotNull(data);
            Console.WriteLine($"{data.id}");
        }

        [Test]
        public void TestPostThenGet()
        {
            var employee = new Employee("dsdsd", "2333", "33");
            var post = new RestCall<Employee>(Method.POST, "http://dummy.restapiexample.com/api/v1", "/create")
               .AddHeader("Accept", "application/json")
               .AddPayload(employee).Execute();
            var id = post.Data.id;
            var get = new RestCall<Employee>(Method.GET, "http://dummy.restapiexample.com/api/v1", "/employee/{id}")
                .AddHeader("Accept", "application/json")
                .AddUrlParameter("id", id)
                .Execute();
            var returnedEmployee = get.Data;
            AssertHelper.Equals(employee.name, returnedEmployee.name);
        }

        //[Test]
        [Obsolete]
        public void TestRestFlow()
        {
            var htmlRepoter = new ExtentHtmlReporter("C:\\Temp\\Sample\\ExtentReport.Html");

            string response = "";
            string BaseUrl = "http://dummy.restapiexample.com/api/v1";
            string requestGet = "/employee/";
            string requestPost = "/create";
            string requestPut = "/update/";
            string requestdel = "/delete/";
            Random rnd = new Random();
            string name = "test" + rnd.Next(3000, 999999);
            string poststr = "{\"name\":\"" + name + "\",\"salary\":\"123\",\"age\":\"23\"}";

            //Send Post
            response = RestHelper.RestPost(BaseUrl, requestPost, "name", poststr);
            

            //Parse response to get Id
            //RestHelper.ParseJasonObj(response, ""); //Code in progress
            JObject json = JObject.Parse(response);
            string Id = json.Last.Last.ToString().Replace("{", "").Replace("}", "").Trim();

            // Modify Get,Put,Del to latest Id
            requestGet = requestGet + Id;
            requestPut = requestPut + Id;
            requestdel = requestdel + Id;

            // Get Latest User
            response = RestHelper.RestGet(BaseUrl, requestGet, "employee_name");

            //Modify Latest User
            //poststr = "{\"name\":\"test" + rnd.Next(3000, 999999) + "\",\"salary\":\"103\",\"age\":\"23\"}";
            poststr = "{\"name\":\"" + name + "\",\"salary\":\"103\",\"age\":\"33\"}";
            response = RestHelper.RestPut(BaseUrl, requestPut, "name", poststr);

            //Delete Latest User
            response = RestHelper.RestDel(BaseUrl, requestdel, "success");

        }

       // [Test]
        public void TestGet()
        {
            string response = "";
            string Url = "http://dummy.restapiexample.com/api/v1/employee";
            string request = "/27224";
            response = RestHelper.RestGet(Url, request, "employee_name");
        }

        //[Test]
        [Obsolete]
        public void TestPost()
        {


            RestClient restClient = new RestClient("http://dummy.restapiexample.com/api/v1/");
            string poststr = "{\"name\":\"test122311\",\"salary\":\"123\",\"age\":\"23\"}";
            Console.WriteLine("Message {0}", poststr);
            Object Ketan = new object();
            RestRequest restRequest = new RestRequest("create", Method.POST);
            restRequest.Parameters.Clear();
            restRequest.AddParameter("application/json", poststr, ParameterType.RequestBody);
            //restRequest.RequestFormat = DataFormat.Json;
            //restRequest.AddBody(new { name = "test749799", salary = 123, age = 23 });
            IRestResponse restResponse = restClient.Execute(restRequest);
            string response = restResponse.Content;
            Console.WriteLine("Post Response is " + response);
        }

       // [Test]
        [Obsolete]
        public void TestPut()
        {


            RestClient restClient = new RestClient("http://dummy.restapiexample.com/api/v1/");
            //string poststr= "{\"name\":\"test\",\"salary\":\"123\",\"age\":\"23\"}";
            //Console.WriteLine("Message {0}", poststr);
            RestRequest restRequest = new RestRequest("update/21", Method.PUT);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddBody(new { name = "testk", salary = 103, age = 33 });
            IRestResponse restResponse = restClient.Execute(restRequest);
            string response = restResponse.Content;
            Console.WriteLine("Put Response is " + response);
        }

        //[Test]
        [Obsolete]
        public void TestDel()
        {


            RestClient restClient = new RestClient("http://dummy.restapiexample.com/api/v1/");
            //string poststr= "{\"name\":\"test\",\"salary\":\"123\",\"age\":\"23\"}";
            //Console.WriteLine("Message {0}", poststr);
            RestRequest restRequest = new RestRequest("delete/169311", Method.DELETE);
            //restRequest.RequestFormat = DataFormat.Json;
            //restRequest.AddBody(new { name = "testk", salary = 103, age = 33 });
            IRestResponse restResponse = restClient.Execute(restRequest);
            string response = restResponse.Content;
            Console.WriteLine("Del Response is " + response);
        }

    }
}
