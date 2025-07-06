Feature: Get Missing

  Verify that a missing page works as expected

  Scenario: Access the Missing page
    Given I am a visitor to "www.example.net"
    When I request "/missing"
    Then the response status code should be 404
