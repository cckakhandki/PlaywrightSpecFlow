@login
Feature: Login Feature
    Feature Description
    
    Scenario: Login with valid creds
        Given I navigate to the homepage
        When I go to the login page
        And I enter username "username"
        And I enter password "password"
        And I click login button
        Then I should land on my profile page