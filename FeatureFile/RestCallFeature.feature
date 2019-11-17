Feature: Rest Call Class Test

@Read
Scenario: Get an employee
	Given User accesses employees API at "http://dummy.restapiexample.com/api/v1"
	When User accesses employee "1"
	Then The employee record is displayed
@Create
@Read
@Delete
Scenario Outline: Add new employee and then delete them
	Given User accesses employees API at "http://dummy.restapiexample.com/api/v1"
	When User creates new employee with name: "<name>", age: "<age>", and salary "<salary>"
	Then The employee is present in the employees list
		And The new employee is successfully deleted from the database
Examples: 
	| name        | age | salary |
	| stovetop    | 69  | 300    |
	| stovetop    | 12  | 500000 |
@Read
@Update
Scenario Outline: Updating employee salary
	Given User accesses employees API at "http://dummy.restapiexample.com/api/v1"
	When User accesses employee "<id>"
		And User updates employee "<id>" with new salary "<salary>"
	Then The new salary "<salary>" is reflected in the database
Examples:
	| id    | salary |
	| 1     | 300000 |
	| 97469 | 202020 |