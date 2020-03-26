Feature: LoginDemoWebShop
this feaure will check login functionality


Background: 

	Given I navigate into the DemoWebShop url
	When I click on the Login link

Scenario: Login into DemoWebShop susccesful
	And I enter valid login details
	| Username | Password  |
	| ketan    | password1 |
	And I Click Login BUtton
	Then Login  should be succesful

Scenario: : Login with invalid username

Scenario: Login with invalid password

#Scenario: Login into DemoWebShop
#	
#	#When I enter the valid username,password and click on Login button
#	#Then the user should be logged in successfully
#	But I enter the Invalid username, password and click on Login button
#	Then the login page should throw an error message
#	When I click on the Logout button
#	Then the user should be logged out successfully



	