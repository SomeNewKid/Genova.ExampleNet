Feature: Get Culture Info

  Verify that the Culture Info page works

  Scenario: Access the Culture Info page
    Given I am a visitor to "www.example.net"
    When I request "/culture-info"
    Then the response status code should be 200
    And I should see "Culture Info" in the response
    And I should see "Afrikaans (South Africa)" in the response
    And I should see "English (United States)" in the response
    And I should see "Persian (Iran)" in the response
    And I should see "Chinese (China)" in the response

  Scenario: Access the Culture Info Afrikaans page
    Given I am a visitor to "www.example.net"
    When I request "/culture-info/af-za"
    Then the response status code should be 200
    And I should see "Afrikaans (South Africa)" in the response
    And I should see "Suid-Afrika" in the response

  Scenario: Access the Culture Info Czech page
    Given I am a visitor to "www.example.net"
    When I request "/culture-info/cs-cz"
    Then the response status code should be 200
    And I should see "Czech (Czechia)" in the response
    And I should see "Česko" in the response

  Scenario: Access the Culture Info Arabic page
    Given I am a visitor to "www.example.net"
    When I request "/culture-info/ar-sa"
    Then the response status code should be 200
    And I should see "Arabic (Saudi Arabia)" in the response
    And I should see "المملكة العربية السعودية" in the response

  Scenario: Access the Invalid Culture Info page
    Given I am a visitor to "www.example.net"
    When I request "/culture-info/xx-xx-123"
    Then the response status code should be 404
