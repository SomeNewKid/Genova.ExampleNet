Feature: Get Hello Redirect

  Verify that the Hello Redirect page works

  Scenario: Access the English Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/hello/redirect"
    Then the response status code should be 200
    And I should see "This path has not been redirected" in the response
    And I should see "ExampleNet HelloPages" in the response

  Scenario: Access the English Hello Redirect page with a query string
    Given I am a visitor to "www.example.net"
    When I request "/hello/redirect?query=banana"
    Then the response status code should be 200
    And I should see "This path has not been redirected" in the response
    And I should see "ExampleNet HelloPages" in the response
    And I should see "Query Value: banana" in the response

  Scenario: Access the Simplified Chinese Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Traditional Chinese Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Arabic Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/ar/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Hebrew Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/he/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Persian Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/fa/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Urdu Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/ur/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Japanese Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/ja/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Thai Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/th/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Korean Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/ko/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Greek Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/el/hello/redirect"
    Then the response status code should be 404

  Scenario: Access the Hindi Hello Redirect page
    Given I am a visitor to "www.example.net"
    When I request "/hi/hello/redirect"
    Then the response status code should be 404
