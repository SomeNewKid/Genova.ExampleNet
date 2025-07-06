Feature: Get Hello Exception

  Verify that the Hello Exception page works

  Scenario: Access the English Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/hello/exception"
    Then the response status code should be 500
    And I should see "Internal Server Error" in the response
    And I should see "The server encountered an internal error and was unable to complete your request." in the response
    And I should see "UseLocalizedExceptionHandler" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Chinese (Simplified) Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Arabic Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/ar/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Hebrew Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/he/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Persian Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/fa/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Urdu Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/ur/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Japanese Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/ja/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Thai Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/th/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Korean Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/ko/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Greek Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/el/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response

  Scenario: Access the Hindi Hello Exception page
    Given I am a visitor to "www.example.net"
    When I request "/hi/hello/exception"
    Then the response status code should be 404
    And I should see "Not Found" in the response
    And I should see "The resource you are looking for could not be found on this server." in the response
    And I should see "UseLocalizedStatusCodePages" in the response
    And I should see "Website: example-net" in the response
