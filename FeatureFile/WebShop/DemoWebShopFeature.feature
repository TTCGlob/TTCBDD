Feature: DemoWebShopFeature
	

Background: Navigate to url and log in
Given I navigated to the DemoWebShop website
	When I click the Log in link
	And I submit username and password
	Then the DetailPage displays

@mytag
Scenario: navigate to url and login
	Given I navigated to the DemoWebShop website
	When I click the Log in link
	Then the LoginPage displays with title "Welcome, Please Sign In!"

Scenario: navigate to the books page and order a specific book at a specified price
	Given I navigated to the Books page
	When I click the "Computing and Internet" link
	Then I confirm price is "10.00"




