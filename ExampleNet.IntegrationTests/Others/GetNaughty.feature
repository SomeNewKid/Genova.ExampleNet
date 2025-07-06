Feature: Get Naughty

  Verify that the naughty page (which attempts to show another site's page) does not work

  Scenario: Access the English Naughty page
    Given I am a visitor to "www.example.net"
    When I request "/naughty"
    Then the response status code should be 500

  Scenario: Access the Simplified Chinese Naughty page
    Given I am a visitor to "www.example.net"
    When I request "/zh/naughty"
    Then the response status code should be 404
