Feature: CompaniesDB_Companies_CJ
	Create and manipulate companies in the Companies database

Background:
	Given User accesses API at url "http://192.168.2.74:3000"

@ignore
Scenario: Create a new company
	Given I have a company I wish to create
	When I submit this company to the company database
	Then the company is now displayed in the database

@ignore
Scenario: Add a new address for a company
	Given I have a company I wish to manage
	When I add an address to the company database
	Then the company has the new address

@ignore
Scenario: Add a new employee to a company
	Given I have a company I wish to manage
	When I add an employee to the company database
	Then the company has the new employee

@ignore
Scenario: Add a second employee to a company
	Given I have a company I wish to manage
	When I add an employee to the company database
	Then the company has the new employee

@ignore
Scenario: Add a third employee to a company
	Given I have a company I wish to manage
	When I add an employee to the company database
	Then the company has the new employee

@ignore
Scenario: Remove an employee from a company
	Given I have a company with at least one employee
	When I delete an employee from the company database
	Then the company no longer has the employee

@ignore
Scenario: Add a new shareholder to a company
	Given I have a company I wish to manage
	When I add a shareholder to the company database
	Then the company has the new shareholder

@ignore
Scenario: Remove a shareholder from a company
	Given I have a company I wish to manage
	When I delete a shareholder from the company database
	Then the company no longer has the shareholder

@ignore
Scenario: Rename a company
	Given I have a company I wish to manage
	When I rename the company in the database
	Then the company has the new name


