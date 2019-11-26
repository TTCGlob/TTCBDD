using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Net.Http;
using TTCBDD.ComponentHelper;
using TTCBDD.Settings;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
using System.Collections.Generic;
using System.Linq;
using TTCBDD.PageObject;
using TTCBDD.APIObjects;
using TTCBDD.Public_Var;
using System.Linq.Expressions;
using FluentAssertions;

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
                .AddUrlParameter("id", "1")
                .Execute();
            Console.WriteLine($"{call.Content}");
            var employees = call.Data;
            Console.WriteLine(employees.id);
            call.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Test]
        public void TestCheck()
        {
            new RestCall<Employee>(Method.HEAD, "http://dummy.restapiexample.com/api/v1", "/employee/{id}")
                .AddUrlParameter("id", "1")
                //.Check(res => res.ResponseOK());
                .Execute()
                .StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void TestRestPost()
        {
            var employee = new Employee(BasicHelperMethods.RandomString(5, 12), "2333", "33");
            var data = new RestCall<Employee>(Method.POST, "http://dummy.restapiexample.com/api/v1", "/{resource}")
                .AddUrlParameter("resource", "create")
                .AddPayload(employee)
                .Execute(e => employee.id = e.Data.id)
                .Data
                .Should().NotBeNull();
        }

        [Test]
        public void TestPostThenGet()
        {
            var employee = new Employee(BasicHelperMethods.RandomString(5, 12), "2333", "33");
            var post = new RestCall<Employee>(Method.POST, "http://dummy.restapiexample.com/api/v1", "/create")
               .AddPayload(employee)
               .Execute(res => employee.id = res.Data.id);
            var get = new RestCall<Employee>(Method.GET, "http://dummy.restapiexample.com/api/v1", "/employee/{id}")
                .AddHeader("Accept", "application/json")
                .AddUrlParameter("id", employee.id)
                .Execute();
            var returnedEmployee = get.Data;
            returnedEmployee.name.Should().Be(employee.name);
        }
        [Test]
        public void TestWhere()
        {
            //uses query strings to only return fresh produce that has no stock
            var products = new RestCall<List<Product>>(Method.GET, "http://192.168.2.73:3000", "/products")
                .Where("product_name like Fresh")
                .Where("stock_level == 0")
                .Execute()
                .Data;
            products.ForEach(product => Console.WriteLine($"Name: {product.product_name} - Stock level: {product.stock_level}"));
            products.ForEach(p => {
                p.product_name.Should().Contain("Fresh");
                p.stock_level.Should().Be(0);
            });
        }
        [Test]
        public void TestProductPost()
        {
            Product braeburn = new Product()
            {
                product_name = "Apple - Braeburn",
                last_restocked = DateTime.Now,
                stock_level = 500
            };
            var post = new RestCall<Product>(Method.POST, "http://192.168.2.73:3000", "/products")
                .AddPayload(braeburn)
                .Execute(res => braeburn.id = res.Data.id);
            Console.WriteLine($"Name: {braeburn.product_name} ID: {braeburn.id}");
        }
        [Test]
        public void TestEmptyResult()
        {
            var response = new RestCall<List<Company>>(Method.GET, "http://192.168.2.73:3000/", "/companies")
                .Where("id >= 2")
                .Where("id <= 4")
                .Execute();
        }

        [Test]
        public void TestDictDeserialize()
        {
            var data = new RestCall<Dictionary<string, object>>(Method.GET, "http://192.168.2.73:3000", "/products/{id}")
                .AddUrlParameter("id", 1)
                .Data();
            data.Should().NotBeEmpty();
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
