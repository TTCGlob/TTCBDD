using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TTCBDD.ComponentHelper;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public sealed class StefEmployee1Feature
    {


        [Given(@"I have reached the endpoint: ""(.*)""")]
        public void GivenIHaveReachedTheEndpoint(string endpoint)
        {
            StefRestHelper.SetURL(endpoint);
        }


        [When(@"I call get method to get user information using id (.*)")]
        public void WhenICallGetMethodToGetUserInformationUsingId(string id)
        {
            StefRestHelper.CreateRequest(id);
        }


        [Then(@"I can confirm name is ""(.*)""")]
        public void ThenICanConfirmNameIs(string name)
        {
            var response = StefRestHelper.GetResponse();
            Console.WriteLine(response.Content);
            //Console.WriteLine(response.ResponseStatus);
            //Console.WriteLine(response.Server);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.Contains("employee_name" + ":" + "Scott"));
            //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    Assert.That(response.Content.Contains("Scott"), Is.EqualTo(employee_name), "The name is not Scott");
            //}

        }
    }
}
