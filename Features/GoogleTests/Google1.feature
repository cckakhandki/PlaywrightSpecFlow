@google
Feature: Feature name

    Feature Description

    Scenario: Test 001
        Given I open the homepage
        When I enter "India" to search
        Then I should see the searched text in the page title

    Scenario Outline: Test outline 002 - <Country>
        Given I open the homepage
        When I enter "<Country>" to search
        Then I should see the searched text in the page title

        Examples:
            | Country |
            | India   |
            | Japan   |