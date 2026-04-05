Feature: Create blog post
  As a user of the UqsBlog API
  I want to submit a blog post
  So that valid posts are persisted

  Scenario: Valid unlocked author creates a post
    Given an author exists with AuthorId 1
    And the author is not locked
    And the post title is "Valid Title"
    When the client submits a create post request
    Then the response status should be 201
    And a post id should be returned
    And the post should be saved

  Scenario: Author does not exist
    Given no author exists with AuthorId 999
    And the post title is "Valid Title"
    When the client submits a create post request
    Then the response status should be 400
    And the error should be "Author Id not found"
    And the post should not be saved

  Scenario: Author is locked
    Given an author exists with AuthorId 7
    And the author is locked
    And the post title is "Valid Title"
    When the client submits a create post request
    Then the response status should be 400
    And the error should be "The author is locked"
    And the post should not be saved