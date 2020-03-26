using System;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using TTCBDD.ComponentHelper;
using NUnit.Framework;
using AventStack.ExtentReports.Reporter;
using System.Collections.Generic;
using System.Linq;
using TTCBDD.PageObject;
using FluentAssertions;
using TTCBDD.StepDefinition;

namespace TTCBDD
{
    [TestFixture]
    public class UnitTest2
    {
        [Test]
        public void Login()
        {
            var variable = new DemoWebShopFeatureSteps();
            variable.GivenIHaveNavigatedToTheDemoWebShopWebsite();
        }
    }
}
