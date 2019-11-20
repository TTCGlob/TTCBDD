using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace TTCBDD.StepDefinition
{
    [Binding]
    public sealed class FluentAssertsAndContextInjection
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public FluentAssertsAndContextInjection(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"I have a word ""(.*)""")]
        public void GivenIHaveAWord(string word)
        {
            if (!context.ContainsKey("words"))
            {
                context.Add("words", new List<string>(2));
            }
            ((List<string>)context["words"]).Add(word);
        }

        [When(@"I join the words with a space")]
        public void WhenIJoinTheWordsWithASpace()
        {
            var joinedWords = ((List<string>)context["words"])[0] + " " + ((List<string>)context["words"])[1];
            context.Add("joinedWords", joinedWords);
        }

        [Then(@"I get the string ""(.*)""")]
        public void ThenIGetTheString(string result)
        {
            ((string)context["joinedWords"]).Should().Be(result, "the first string is {0} and the second string is {1}", ((List<string>)context["words"])[0], ((List<string>)context["words"])[1]);
        }
    }
}
