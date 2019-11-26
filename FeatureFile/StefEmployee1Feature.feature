Feature: StefEmployee1Feature
	Test RESTAPI using example website and targetting employee 1

@mytag
Scenario: Confirm name of employee 1
	Given I have reached the endpoint: "api/v1/employee"
	When I call get method to get user information using id 1
# Then I get API response confirming name in Json format
	Then I can confirm name is "Scott"
