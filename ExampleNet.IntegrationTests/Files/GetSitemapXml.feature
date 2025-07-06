Feature: Get Sitemap XML

  Verify that the sitemap XML file is available

  Scenario: Access the sitemap.xml file
    Given I am a visitor to "www.example.net"
    When I request "/sitemap.xml"
    Then the response status code should be 200
    And I should see "urlset" in the response
    And I should see "<url>" in the response
    And I should see "<loc>" in the response

    And I should see "https://www.example.net" in the response
    And I should not see "https://www.example.com" in the response
    And I should not see "https://www.nibblon.com" in the response
    And I should not see "https://www.nibblon.net" in the response    

    And I should see "/" as a sitemap location
    And I should see "/sitemap" as a sitemap location
    And I should see "/hello/rewrite" as a sitemap location
    And I should see "/hello/redirect" as a sitemap location
    And I should see "/culture-info" as a sitemap location
    
    And I should not see "/privacy" as a sitemap location
    And I should not see "/terms-of-use" as a sitemap location
    And I should not see "/hello/context" as a sitemap location
    And I should not see "/hello/result" as a sitemap location
    And I should not see "/hello/view" as a sitemap location
    And I should not see "/hello/user" as a sitemap location
    And I should not see "/hello/authenticated" as a sitemap location
    And I should not see "/hello/authorized" as a sitemap location
    And I should not see "/hello/rewritten" as a sitemap location
    And I should not see "/hello/redirected" as a sitemap location
    And I should not see "/hello/gzip" as a sitemap location
    And I should not see "/hello/zstd" as a sitemap location
    And I should not see "/hello/brotli" as a sitemap location
    And I should not see "/hello/deflate" as a sitemap location
    And I should not see "/hello/snappy" as a sitemap location
    And I should not see "/hello/culture" as a sitemap location


  Scenario: Access the sitemap.xml file with query
    Given I am a visitor to "www.example.net"
    When I request "/sitemap.xml?query"
    Then the response status code should be 200
    And I should see "urlset" in the response
    And I should see "<url>" in the response
    And I should see "<loc>" in the response
    And I should see "https://www.example.net" in the response

  Scenario: Access the missing.xml file
    Given I am a visitor to "www.example.net"
    When I request "/missing.xml"
    Then the response status code should be 404
