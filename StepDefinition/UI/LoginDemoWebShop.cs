using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TTCBDD.Helpers.UI;

namespace TTCBDD.StepDefinition.UI
{
    [Binding]
    public sealed class LoginDemoWebShop
    {
        // Create Object here

        // Create constructor
        // Driver code
        [Given(@"I navigate into the DemoWebShop url")]
        public void GivenINavigateIntoTheDemoWebShopUrl(string url)
        {
            // Stat Driver
            // Nvaigate
            // Input sdf
            NavigationHelper.NavigateToUrl("http://demowebshop.tricentis.com/");
        }

        [Given(@"I click on the Login link")]
        public void GivenIClickOnTheLoginLink()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I enter valid login details")]
        public void GivenIEnterValidLoginDetails(Table table)
        {
            //var userdetails = table<UserDetails>;
            ScenarioContext.Current.Pending();
        }

        [Given(@"I Click Login BUtton")]
        public void GivenIClickLoginBUtton()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Login  should be succesful")]
        public void ThenLoginShouldBeSuccesful()
        {
            ScenarioContext.Current.Pending();
        }

        internal class UserDetails
        {
            string Username;
            string password;
        }

    }
}
