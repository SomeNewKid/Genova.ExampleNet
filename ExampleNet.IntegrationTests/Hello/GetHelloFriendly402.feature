Feature: Get Hello Friendly402

  Verify that the Hello Friendly-402 page works

  Scenario: Access the English Hello Friendly-402 page
    Given I am a visitor to "www.example.net"
    When I request "/hello/friendly-402"
    Then the response status code should be 402
    And I should see "Payment Required" in the response
    And I should see "The server requires payment to complete your request." in the response
    And I should see "HelloPages" in the response
    And I should not see "View for status 402 (Unknown)" in the response
    And I should not see "Fallback for status 402" in the response

  Scenario: Access the Chinese (Simplified) Hello Friendly-402 page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/friendly-402"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Friendly-402 page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/friendly-402"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "View for status 404 (404)" in the response
    And I should not see "Fallback for status 404" in the response
    And I should see "Website: example-net" in the response
