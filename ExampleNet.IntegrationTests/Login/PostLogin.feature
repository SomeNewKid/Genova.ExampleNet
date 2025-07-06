Feature: Post Login

  Verify that the Login page form submission works as expected

  Scenario: Access the English Login page with empty credentials
    Given I am a visitor to "www.example.net"
    When I request "/login"
    And I enter a "Username" of ""
    And I enter a "Password" of ""
    And I submit the form
    Then the response status code should be 200
    And I should see "Please log in" in the response
    And I should see "Username is required" in the response
    And I should see "Password is required" in the response

  Scenario: Access the English Login page with whitespace credentials
    Given I am a visitor to "www.example.net"
    When I request "/login"
    And I enter a "Username" of " "
    And I enter a "Password" of " "
    And I submit the form
    Then the response status code should be 200
    And I should see "Please log in" in the response
    And I should see "Username is required" in the response
    And I should see "Password is required" in the response

  Scenario: Access the English Login page with invalid username
    Given I am a visitor to "www.example.net"
    When I request "/login"
    And I enter a "Username" of "not-an-email-address"
    And I submit the form
    Then the response status code should be 200
    And I should see "Please log in" in the response
    And I should see "Username must be a valid email address" in the response

  Scenario: Access the English Login page with invalid credentials
    Given I am a visitor to "www.example.net"
    When I request "/login"
    And I enter a "Username" of "Thief@example.com"
    And I enter a "Password" of "not-valid"
    And I submit the form
    Then the response status code should be 200
    And I should see "Please log in" in the response
    And I should see "Invalid username or password" in the response

  Scenario: Access the English Login page with valid credentials
    Given I am a visitor to "www.example.net"
    When I request "/login"
    And I enter a "Username" of "Thief@example.com"
    And I enter a "Password" of "Thief@example.com123"
    And I submit the form
    Then the response status code should be 302
    And I should be redirected to "/"    
