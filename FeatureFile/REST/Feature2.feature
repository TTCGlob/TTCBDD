﻿Feature: Feature2
Test Rest Feature


Background: 
Given I have a Base URL "http://dummy.restapiexample.com/api/v1"

@RandoTag
Scenario: 1-PostValue Request
	And I Pass request to server "/create"
	Then Verify Post method "name" tag is present in message for Body "MessageBody"

Scenario: 2-GetValue Request
	And I Pass request to server "/employee/"
	Then Verify Get method "employee_name" tag is present in message

Scenario: 3-Put Value Request
	And I Pass request to server "/update/"
	Then Verify Put method "name" tag is present in message for Body "MessageBody"

Scenario: 4-Delete Value  Request
	And I Pass request to server "/delete/"
	Then Verify Del method "success" tag is present in message

