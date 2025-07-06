Feature: Get Login

  Verify that the Login page works as expected

  Scenario: Access the English Login page
    Given I am a visitor to "www.example.net"
    When I request "/login"
    Then the response status code should be 200
    And I should see "Please log in" in the response

  Scenario: Access the Simplified Chinese Login page
    Given I am a visitor to "www.example.net"
    When I request "/zh/login"
    Then the response status code should be 404

  Scenario: Access the Traditional Chinese Login page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/login"
    Then the response status code should be 404
