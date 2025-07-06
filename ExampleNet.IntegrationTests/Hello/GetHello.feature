Feature: Get Hello

  Verify that the Hello page works

  Scenario: Access the English Hello page
    Given I am a visitor to "www.example.net"
    When I request "/hello"
    Then the response status code should be 200
    And I should see "Hello" in the response

  Scenario: Access the Chinese Hello page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello"
    Then the response status code should be 404
