Feature: Get About

  Verify that the About page works as expected

  Scenario: Access the English Example page
    Given I am a visitor to "www.example.net"
    When I request "/about"
    Then the response status code should be 200
    And I should see "This is an article about the ExampleNet website." in the response
    And I should see "Footer links for the ExampleNet website" in the response
