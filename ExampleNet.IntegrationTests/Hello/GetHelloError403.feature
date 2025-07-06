Feature: Get Hello Error403

  Verify that the Hello Error-403 page works

  Scenario: Access the English Hello Error-403 page
    Given I am a visitor to "www.example.net"
    When I request "/hello/error-403"
    Then the response status code should be 403
    And I should see "Forbidden" in the response
    And I should see "You do not have permission to access this resource." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 403 (403)" in the response
    And I should not see "Fallback for status 403" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the Chinese (Simplified) Hello Error-403 page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/error-403"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Error-403 page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/error-403"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
    And I should not see "Override by ExampleCom website" in the response
