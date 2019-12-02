Feature: Test CustomDB

Background: Pre-Condition
	Given User accesses companies API at "http://192.168.2.72:3000/companies"

	
Scenario: Create an employee who is also a shareholder 
	Given User accesses company with id: "3"
	When User creates a new employee and shareholder
	Then The employee is present in the employee list
		And The employee is present in the shareholder list

Scenario: Increase every employees salary by 15% for all companys
	Given User accesses all companies 
	When User accesses company employees and increases each salary by "15"%
	Then The salary increase will be reflected in the database

Scenario Outline: Change an employee to a shareholder
	Given User accesses company with id: "<companyId>"
		And User accesses employee with id "<employeeId>"
	When User converts the employee to a shareholder
	Then The employee should be present as a shareholder
Examples:
	| companyId | employeeId |
	| 2         | 2          |
	| 3         | 1          |

Scenario Outline: Change a shareholder to an employee
	Given User accesses company with id: "<companyId>"
		And User accesses shareholder with id "<shareholderId>"
	When User converts the shareholder to an employee
	Then The shareholder should be present as an employee
Examples:
	| companyId | shareholderId |
	| 2         | 3             |
	| 3         | 1             |

Scenario Outline: Replace company1 employees with company2 employees
	Given User accesses company with id: "<company1>"
		And User accesses employees of company with id: "<company2>"
	When The User replaces company1 employees with company2 employees
	Then Company1 employees should match company2 employees
Examples:
	| company1 | company2 |
	| 3        | 2        |

Scenario Outline: Delete all companies with a net worth <100
	Given User accesses all companies 
	When User deletes all companies with net worth less than "<netWorthMin>"
	Then All companies in database networth will be greater than "<netWorthMin>"
	Examples: 
	| netWorthMin |
	| 150         |