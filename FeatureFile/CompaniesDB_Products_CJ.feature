Feature: CompaniesDB_Products_CJ
	Create and manipulate products in the Companies database

Background:
	Given User accesses API at url "http://192.168.2.74:3000"

Scenario: Create a new product
	Given I have a product type I wish to create
	When I submit this product to the company database
	Then the product is now available in the company database

Scenario: I wish to purchase a product
	Given I have a product I wish to purchase one of
	When I submit my purchase request to the company database
	Then the purchase was successfully registered

Scenario: I wish to register new stock a product
	Given I have a product I wish to register new stock for
	When I submit a stock increase to the company database
	Then the new stock was successfully registered