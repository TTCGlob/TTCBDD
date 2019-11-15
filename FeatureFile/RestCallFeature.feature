Feature: Rest Call Class Test

@mytag
Scenario: Get an employee
	Given User accesses employees API at "http://dummy.restapiexample.com/api/v1"
	When User accesses employee "1"
	Then The employee record is displayed
Scenario Outline: Add new employee
	Given User accesses employees API at "http://dummy.restapiexample.com/api/v1"
	When User creates new employee with name: "<name>", age: "<age>", and salary "<salary>"
	Then The employee is present in the employees list
		And The new employee is successfully deleted from the database
Examples: 
	| name        | age | salary |
	| stovetop    | 69  | 300    |
	| stovetop    | 12  | 500000 |