Feature: DemoWebShopFeature
	

@mytag
Scenario: Navigate to url and Login
	Given I navigated to the DemoWebShop website
	When I click the Log in link
	Then the login page displays


