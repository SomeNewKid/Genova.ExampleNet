Feature: Get Hello Dont Cache

  Verify that the Hello Don’t Cache page works

  Scenario: Access the English Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache


  Scenario: Access the Simplified Chinese Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/zh/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/zh/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Traditional Chinese Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/zh/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/zh-hk/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/zh-hk/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Arabic Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ar/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ar/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Hebrew Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/he/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/he/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Persian Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/fa/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/fa/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Urdu Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ur/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ur/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Japanese Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ja/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ja/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Thai Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/th/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/th/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Korean Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ko/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ko/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Greek Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/el/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/el/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Hindi Hello Don’t Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/dont-cache"
    And the response status code should be 200
    And I should see "This page should not be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/hi/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/hi/hello/dont-cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

