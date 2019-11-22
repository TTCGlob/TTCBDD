Feature: CompaniesDB
	Access the companies database

Background:
	Given User accesses API at "http://192.168.0.114:3000"

@mytag
Scenario: Retrieve a company
	When user accesses endpoint "/companies"
	Then a list of companies is returned

Scenario: Create a new company
	Given User creates a new company record
	When User submits this to the endpoint "/companies"
	Then the company is displayed in the database at "/companies/{id}"

Scenario: Increase a company's value
	Given User accesses a company at "/companies"
	When User sets company value to 500000
	Then The change is reflected at "/companies/{id}"

Scenario: Company changes address
	Given User accesses company with ID 1 at "/companies/{id}"
	When User changes the company 1 address and submits it to "/companies/{id}"
	Then The company 1 should have a new address