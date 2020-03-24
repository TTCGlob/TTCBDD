using System.Collections.Generic;
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
            ((List<string>)context["words"])
                .Add(word);
        }

        [When(@"I join the words with a space")]
        public void WhenIJoinTheWordsWithASpace()
        {
            var words = (List<string>)context["words"];
            var joinedWords = words[0] + " " + words[1];
            context.Add("joinedWords", joinedWords);
            var nums = new List<string>() { "1", "2", "3" };
        }

        [Then(@"I get the string ""(.*)""")]
        public void ThenIGetTheString(string result)
        {
            var words = context["words"] as List<string>;
            var joinedWord = context["joinedWords"] as string;
            joinedWord.Should().Be(result, "the first string is {0} and the second string is {1}", words[0], words[1]);
        }
    }
}
