Feature: Get Hello Cache

  Verify that the Hello Cache page works

  Scenario: Access the English Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should be from the output cache


  Scenario: Access the English Hello Cache page after ExampleCom
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "https://www.example.com/hello/purge-cache"
    # And the response status code should be 200
    # And I should see "This cache has been purged" in the response
    # And I should see "ExampleCom HelloPages" in the response

    And I request "https://www.example.com/hello/cache"
    # And the response status code should be 200
    # And I should see "This page should be cached" in the response
    # And I should see "ExampleCom HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should be from the output cache


  Scenario: Access the Simplified Chinese Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/zh/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/zh/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Traditional Chinese Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/zh-hk/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/zh-hk/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Arabic Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ar/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ar/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Hebrew Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/he/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/he/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Persian Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/fa/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/fa/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Urdu Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ur/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ur/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Japanese Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ja/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ja/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Thai Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/th/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/th/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Korean Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/ko/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/ko/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Greek Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/el/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/el/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache


  Scenario: Access the Hindi Hello Cache page
    Given I am a visitor to "www.example.net"
    When I request "/hello/purge-cache"
    Then the response status code should be 200
    And I should see "This cache has been purged" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response

    And I request "/hello/cache"
    And the response status code should be 200
    And I should see "This page should be cached" in the response
    And I should see "Banana and tomato" in the response
    And I should see "ExampleNet HelloPages" in the response
    And the response should not be from the output cache

    And I request "/hi/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache

    And I request "/hi/hello/cache"
    And the response status code should be 404
    And I should see "Not Found" in the response
    And the response should not be from the output cache
