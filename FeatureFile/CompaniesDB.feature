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