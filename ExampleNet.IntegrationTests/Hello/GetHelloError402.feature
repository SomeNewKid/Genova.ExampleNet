Feature: Get Hello Error402

  Verify that the Hello Error-402 page works

  Scenario: Access the English Hello Error-402 page
    Given I am a visitor to "www.example.net"
    When I request "/hello/error-402"
    Then the response status code should be 402
    And I should see "Unexpected Error" in the response
    And I should see "An unexpected error occurred. Please try again later." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 402 (Unknown)" in the response
    And I should not see "Fallback for status 402" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the English Hello Error-402 page using fallback HTML
    Given I am a visitor to "www.example.net"
    When I request "/hello/error-402?use-fallback-html"
    Then the response status code should be 402
    And I should see "Unexpected Error" in the response
    And I should see "An unexpected error occurred. Please try again later." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should not see "View for status 402 (Unknown)" in the response
    And I should see "Fallback for status 402" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the Chinese (Simplified) Hello Error-402 page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/error-402"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Error-402 page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/error-402"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response
