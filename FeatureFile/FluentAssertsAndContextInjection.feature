Feature: FluentAssertsAndContextInjection
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Concatenate Two Words
	Given I have a word "All"
	And I have a word "Blacks"
	When I join the words with a space
	Then I get the string "All Blacks"
