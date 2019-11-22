Feature: LoggingExample
	In order to do something
	As a someone
	I want to do a thing

	# Demonstrates logging features (go to step definitions)

@mytag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 130 on the screen
