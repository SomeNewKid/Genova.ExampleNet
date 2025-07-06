Feature: Get Hello Error500

  Verify that the Hello Error-500 page works

  Scenario: Access the English Hello Error-500 page
    Given I am a visitor to "www.example.net"
    When I request "/hello/error-500"
    Then the response status code should be 500
    And I should see "Internal Server Error" in the response
    And I should see "The server encountered an internal error and was unable to complete your request." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 500 (500)" in the response
    And I should not see "Fallback for status 500" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the Chinese (Simplified) Hello Error-500 page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/error-500"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Error-500 page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/error-500"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response
