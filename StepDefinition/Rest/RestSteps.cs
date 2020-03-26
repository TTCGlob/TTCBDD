using AventStack.ExtentReports.Gherkin.Model;
using TechTalk.SpecFlow;
using TTCBDD.BaseClass.RestObjects;
using TTCBDD.Context;

namespace TTCBDD.StepDefinition.Rest
{
    [Binding]
    public sealed class RestSteps
    {
        #region Given
        [Given(@"I have a Base URL ""(.*)""")]
        public void GivenIHaveABaseURL(string URL)
        {

            PublicVar.BaseUrl = URL;
        }

        #endregion

        #region When
        [When(@"I Pass request to server ""(.*)""")]
        public void WhenIPassRequestToServer(string RequestType)
        {
            PublicVar.RequestType = RequestType;
        }

        #endregion

        #region Then
        [Then(@"Verify Get method ""(.*)"" tag is present in message")]
        public void ThenVerifyGetMethodTagIsPresentInMessage(string TagToVerify)
        {
            RestActions.GetRestMethod(TagToVerify);
        }

        [Then(@"Verify Post method ""(.*)"" tag is present in message for Body ""(.*)""")]
        public void ThenVerifyPostMethodTagIsPresentInMessageForBody(string TagToVerify, string body)
        {
            RestActions.PostRestMethod(TagToVerify, body);


        }

        [Then(@"Verify Put method ""(.*)"" tag is present in message for Body ""(.*)""")]
        public void ThenVerifyPutMethodTagIsPresentInMessageForBody(string TagToVerify, string body)
        {
            RestActions.PutRestMethod(TagToVerify, body);
        }

        [Then(@"Verify Del method ""(.*)"" tag is present in message")]
        public void ThenVerifyDelMethodTagIsPresentInMessage(string TagToVerify)
        {
            RestActions.DelRestMethod(TagToVerify);
        }

        #endregion

        #region And
        [Given(@"I Pass request to server ""(.*)""")]
        public void GivenIPassRequestToServer(string RequestType)
        {
            PublicVar.RequestType = RequestType;
        }


        #endregion





    }
}
