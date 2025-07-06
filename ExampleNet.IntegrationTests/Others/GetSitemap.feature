Feature: Get Sitemap

  Verify that the sitemap page is available

  Scenario: Access the sitemap file
    Given I am a visitor to "www.example.net"
    When I request "/sitemap"
    Then the response status code should be 200
    And I should see "<html" in the response
    And I should see "<body" in the response

    And I should see "https://www.example.net" in the response
    And I should not see "https://www.example.com" in the response
    And I should not see "https://www.nibblon.com" in the response
    And I should not see "https://www.nibblon.net" in the response
    
    And I should see "/" as a hyperlink location
    And I should see "/hello/rewrite" as a hyperlink location
    And I should see "/hello/redirect" as a hyperlink location
    And I should see "/culture-info" as a hyperlink location
    
    And I should not see "/privacy" as a hyperlink location
    And I should not see "/terms-of-use" as a hyperlink location
    And I should not see "/hello/context" as a hyperlink location
    And I should not see "/hello/result" as a hyperlink location
    And I should not see "/hello/view" as a hyperlink location
    And I should not see "/hello/user" as a hyperlink location
    And I should not see "/hello/authenticated" as a hyperlink location
    And I should not see "/hello/authorized" as a hyperlink location
    And I should not see "/hello/rewritten" as a hyperlink location
    And I should not see "/hello/redirected" as a hyperlink location
    And I should not see "/hello/gzip" as a hyperlink location
    And I should not see "/hello/zstd" as a hyperlink location
    And I should not see "/hello/brotli" as a hyperlink location
    And I should not see "/hello/deflate" as a hyperlink location
    And I should not see "/hello/snappy" as a hyperlink location
    And I should not see "/hello/culture" as a hyperlink location


  Scenario: Access the sitemap file with query
    Given I am a visitor to "www.example.net"
    When I request "/sitemap?query"
    Then the response status code should be 200
    And I should see "<html" in the response
    And I should see "<body" in the response
    And I should see "https://www.example.net" in the response
