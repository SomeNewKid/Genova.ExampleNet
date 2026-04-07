Feature: Get Example

  Verify that the example page works

  Scenario: Access the Example page
    Given I am a visitor to "www.example.net"
    When I request "/example"
    Then the response status code should be 200
    And I should see "Example Domain" in the response
    And I should see "This domain is for use" in the response
