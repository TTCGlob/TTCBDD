Feature: Rest Call Class Test

Background:
	Given User accesses employees API at "http://dummy.restapiexample.com/api/v1"

@Read
Scenario: Get an employee
	When User accesses employee "1"
	Then The employee record is displayed

@Create
@Read
Scenario: Add a new random employee
	Given User creates a new employee
	When User adds the employee to the database
	Then The employee is present in the employees list

@Create
@Read
@Delete
Scenario Outline: Add new employee and then delete them
	When User creates new employee with name: "<name>", age: "<age>", and salary "<salary>"
	Then The employee is present in the employees list
		And The new employee is successfully deleted from the database
Examples: 
	| name     | age | salary |
	| stovetop | 69  | 300    |
	| stovetop | 12  | 500000 |

@Read
@Update
Scenario Outline: Updating employee salary
	When User accesses employee "<id>"
		And User updates employee "<id>" with new salary "<salary>"
	Then The new salary "<salary>" is reflected in the database
Examples:
	| id    | salary |
	| 1     | 999    |
	| 98370 | 999    |

@Read
@Update
Scenario: Giving employees a percentage raise
	#Caution: Involves many REST calls, can take a while
	When User retrieves 15 employees
		And User raises all their salaries by 15%
	Then This change is reflected in the database