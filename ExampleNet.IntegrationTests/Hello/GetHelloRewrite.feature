Feature: Get Hello Rewrite

  Verify that the Hello Rewrite page works

  Scenario: Access the English Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/hello/rewrite"
    Then the response status code should be 200
    And I should see "This path has not been rewritten" in the response
    And I should see "ExampleNet HelloPages" in the response

  Scenario: Access the English Hello Rewrite page with a query string
    Given I am a visitor to "www.example.net"
    When I request "/hello/rewrite?query=banana"
    Then the response status code should be 200
    And I should see "This path has not been rewritten" in the response
    And I should see "ExampleNet HelloPages" in the response
    And I should see "Query Value: banana" in the response

  Scenario: Access the Simplified Chinese Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/zh/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Traditional Chinese Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/zh-hk/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Arabic Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/ar/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Hebrew Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/he/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Persian Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/fa/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Urdu Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/ur/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Japanese Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/ja/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Thai Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/th/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Korean Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/ko/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Greek Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/el/hello/rewrite"
    Then the response status code should be 404

  Scenario: Access the Hindi Hello Rewrite page
    Given I am a visitor to "www.example.net"
    When I request "/hi/hello/rewrite"
    Then the response status code should be 404
