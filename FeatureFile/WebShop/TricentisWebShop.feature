Feature: TricentisWebShop
	Scenarios on the Tricentis web shop site

@frontend
Scenario: Login to the webshop
	Given I navigate to to the webshop
	And I click the log in link
	When I login as "g@w.yee", "NONONO"
	Then I should be logged in

@frontend
@cart
Scenario: Add product to cart
	Given I navigate to to the webshop
	And I click the log in link
	And I login as "g@w.yee", "NONONO"
	And I navigate to the "BOOKS" category
	When I add "Computing and Internet" to my cart
	Then A "success" notification should appear
	And It should say "The product has been added to your shopping cart"
	And The shopping cart should indicate it has 1 item in it