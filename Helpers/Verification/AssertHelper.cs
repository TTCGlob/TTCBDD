using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TTCBDD.Context;
using TTCBDD.Settings;

namespace TTCBDD.Helpers.Verification
{
    public class AssertHelper
    {
        [Obsolete]
        public static void AreEqual(string Expected, string Actual)
        {
            try
            {
                Assert.AreEqual(Expected, Actual);
                Console.WriteLine("Assert Equal Success " + Expected);
                PublicVar.StepStatus = "Pass";
                PublicVar.StepMsg = "Assert Equal Success " + Expected;
            }
            catch (Exception e)
            {
                AssertFailMsg("Assert Fail " + e.Message);
                Console.WriteLine("Assert Fail " + e.Message);
                Console.WriteLine(ScenarioContext.Current.ScenarioInfo.Title);
            }
        }

        [Obsolete]
        public static void IsTrue(bool Expected)
        {
            try
            {
                Assert.IsTrue(Expected);
                Console.WriteLine("Assert IsTrue Success " + Expected);
                PublicVar.StepStatus = "Pass";
                PublicVar.StepMsg = "Assert IsTrue Success " + Expected;
            }
            catch (Exception e)
            {
                AssertFailMsg("Assert Fail " + e.Message);
                Console.WriteLine("Assert Fail " + e.Message);
                Console.WriteLine(ScenarioContext.Current.ScenarioInfo.Title);
            }
        }

        public static void AssertFailMsg(string msg)
        {
            try
            {
                Assert.Fail(msg);

            }
            catch
            {
                ObjectRepository.ErrMsg.Add(msg);
                Console.WriteLine("Assert Fail " + msg);
                PublicVar.StepStatus = "Fail";
                PublicVar.StepMsg = msg;
            }

        }
    }
}
