Feature: Northwind Feature
	Access the Northwind Database

@mytag
Scenario: Get all orders shipped to Brazil
	Given User accesses API at url "http://services.odata.org/V4/Northwind/Northwind.svc/"
	When User accesses orders going to "Brazil"
	Then Orders are displayed