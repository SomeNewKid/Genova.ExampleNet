Feature: Get Home

  Verify that the the home page works

  Scenario: Access the Home page
    Given I am a visitor to "www.example.net"
    When I request the home page
    Then the response status code should be 200
    And I should see "Welcome to ExampleNet!" in the response
    And I should see "Footer links for the ExampleNet website" in the response
