﻿Feature: TricentisWebShop
	Scenarios on the Tricentis web shop site

@frontend
Scenario: Login to the webshop
	Given I navigate to to the webshop
	And I click the log in link
	When I login as "g@w.yee", "NONONO"
	Then I should be logged in
