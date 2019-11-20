Feature: Scenarios for tesing the products database
	Using dummy rest api running on the local network we can test query strings

@mytag
Scenario: Find all fresh produce
	Given User accesses API at "http://192.168.2.73:3000"
	When User accesses products labeled fresh
		And Selects product names
	Then All the fresh products are displayed
