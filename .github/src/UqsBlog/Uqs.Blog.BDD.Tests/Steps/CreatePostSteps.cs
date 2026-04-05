using Reqnroll;
using Xunit;
using Uqs.Blog.Domain.DomainObjects;
using Uqs.Blog.Domain.Repositories;
using Uqs.Blog.Domain.Services;

namespace Uqs.Blog.BDD.Tests.Steps;

[Binding]
public class CreatePostSteps
{
    private int _authorId;
    private string _title = string.Empty;

    private int _statusCode;
    private int _returnedPostId;
    private string? _errorMessage;

    private FakeAuthorRepository _authorRepository = null!;
    private FakePostRepository _postRepository = null!;
    private AddPostService _sut = null!;

    [Given(@"an author exists with AuthorId (.*)")]
    public void GivenAnAuthorExistsWithAuthorId(int authorId)
    {
        _authorId = authorId;
        _authorRepository = new FakeAuthorRepository(new Author
        {
            Id = authorId,
            Name = "Valid Author",
            IsLocked = false
        });
    }

    [Given(@"no author exists with AuthorId (.*)")]
    public void GivenNoAuthorExistsWithAuthorId(int authorId)
    {
        _authorId = authorId;
        _authorRepository = new FakeAuthorRepository(null);
    }

    [Given(@"the author is not locked")]
    public void GivenTheAuthorIsNotLocked()
    {
        if (_authorRepository.CurrentAuthor is not null)
        {
            _authorRepository.CurrentAuthor.IsLocked = false;
        }
    }

    [Given(@"the author is locked")]
    public void GivenTheAuthorIsLocked()
    {
        if (_authorRepository.CurrentAuthor is not null)
        {
            _authorRepository.CurrentAuthor.IsLocked = true;
        }
    }

    [Given(@"the post title is ""(.*)""")]
    public void GivenThePostTitleIs(string title)
    {
        _title = title;
    }

    [When(@"the client submits a create post request")]
    public void WhenTheClientSubmitsACreatePostRequest()
    {
        _postRepository = new FakePostRepository
        {
            CreatePostReturnValue = 101
        };

        _sut = new AddPostService(_postRepository, _authorRepository);

        try
        {
            // Representative stub behavior:
            // Simulated API response by calling the domain service
            // and translating exceptions into HTTP-like status codes.
            _returnedPostId = _sut.AddPost(_authorId);
            _statusCode = 201;
        }
        catch (ArgumentException ex)
        {
            _statusCode = 400;
            _errorMessage = ex.Message;
        }
        catch (InvalidOperationException ex)
        {
            _statusCode = 400;
            _errorMessage = ex.Message;
        }
    }

    [Then(@"the response status should be (.*)")]
    public void ThenTheResponseStatusShouldBe(int expectedStatusCode)
    {
        global::Xunit.Assert.Equal(expectedStatusCode, _statusCode);
    }

    [Then(@"a post id should be returned")]
    public void ThenAPostIdShouldBeReturned()
    {
        global::Xunit.Assert.True(_returnedPostId > 0);
    }

    [Then(@"the error should be ""(.*)""")]
    public void ThenTheErrorShouldBe(string expectedMessage)
    {
        global::Xunit.Assert.Contains(expectedMessage, _errorMessage ?? string.Empty);
    }

    [Then(@"the post should be saved")]
    public void ThenThePostShouldBeSaved()
    {
        global::Xunit.Assert.Equal(1, _postRepository.CreatePostCallCount);
        global::Xunit.Assert.Equal(_authorId, _postRepository.LastCreatePostAuthorId);
    }

    [Then(@"the post should not be saved")]
    public void ThenThePostShouldNotBeSaved()
    {
        global::Xunit.Assert.Equal(0, _postRepository.CreatePostCallCount);
    }

    private sealed class FakeAuthorRepository : IAuthorRepository
    {
        public Author? CurrentAuthor { get; }

        public FakeAuthorRepository(Author? author)
        {
            CurrentAuthor = author;
        }

        public Author? GetById(int id) => CurrentAuthor;
    }

    private sealed class FakePostRepository : IPostRepository
    {
        public int CreatePostReturnValue { get; set; }

        public int CreatePostCallCount { get; private set; }
        public int? LastCreatePostAuthorId { get; private set; }

        public int CreatePost(int authorId)
        {
            CreatePostCallCount++;
            LastCreatePostAuthorId = authorId;
            return CreatePostReturnValue;
        }

        public Post? GetById(int postId) => throw new NotImplementedException();
        public void Update(Post post) => throw new NotImplementedException();
    }
}