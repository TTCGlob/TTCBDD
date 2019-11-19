@FeatureTag
Feature: ReportingExample
	In order to avoid silly mistakes
	As a math idiot
	I want to be able to make various calculations

@tag1
Scenario Outline: Add two numbers
	In order to check the addition function works

	Given I have entered <firstNumber> into my calculator
	And I have also entered <secondNumber> into my calculator
	When I press the add key
	Then the displayed result should be <expectResult>

	Examples:
	| firstNumber | secondNumber | expectResult |
	| 7           | 10           | 17           |
	| 20          | 50           | 70           |
	| 56          | 100          | 156          |
	
@tag1 @tag2
Scenario: Subtract one number from another number
	In order to check the subtraction function works
	
	Given I have entered 50 into my calculator
	And I have also entered 30 into my calculator
	When I press the subtract key
	Then the displayed result should be 20

Scenario: Multiply two numbers
	In order to check the multiplication function works

	Given I have entered 50 into my calculator
	And I have also entered 70 into my calculator
	When I press the multiply key
	Then the displayed result should be 3500

@tag4
Scenario: Divide one number by another number
	In order to check the division function works

	Given I have entered 29388 into my calculator
	And I have also entered 124 into my calculator
	When I press the divide key
	Then the displayed result should be 236