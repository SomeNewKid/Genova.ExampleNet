Feature: Get Hello Friendly502

  Verify that the Hello Friendly-502 page works

  Scenario: Access the English Hello Friendly-502 page
    Given I am a visitor to "www.example.net"
    When I request "/hello/friendly-502"
    Then the response status code should be 502
    And I should see "Bad Gateway" in the response
    And I should see "The server received an invalid response from an upstream server." in the response
    And I should see "HelloPages" in the response
    And I should not see "View for status 502 (502)" in the response
    And I should not see "Fallback for status 502" in the response

  Scenario: Access the Chinese (Simplified) Hello Friendly-502 page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/friendly-502"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Friendly-502 page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/friendly-502"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
