using NSubstitute;
using Uqs.Blog.Domain.DomainObjects;
using Uqs.Blog.Domain.Repositories;
using Uqs.Blog.Domain.Services;
using Xunit;

namespace Uqs.Blog.Domain.Tests.Integration;

public class AddPostServiceIntegrationTests
{
    [Fact]
    public void AddPost_ValidAuthor_CallsCreatePostOnce_AndReturnsNewPostId()
    {
        // Arrange
        var postRepository = Substitute.For<IPostRepository>();
        var authorRepository = Substitute.For<IAuthorRepository>();

        var authorId = 1;
        var expectedPostId = 42;

        var author = new Author
        {
            Id = authorId,
            Name = "Valid Author",
            IsLocked = false
        };

        authorRepository.GetById(authorId).Returns(author);
        postRepository.CreatePost(authorId).Returns(expectedPostId);

        var sut = new AddPostService(postRepository, authorRepository);

        // Act
        var result = sut.AddPost(authorId);

        // Assert
        Assert.Equal(expectedPostId, result);
        postRepository.Received(1).CreatePost(authorId);
    }

    [Fact]
    public void AddPost_AuthorLocked_DoesNotCallCreatePost()
    {
        // Arrange
        var postRepository = Substitute.For<IPostRepository>();
        var authorRepository = Substitute.For<IAuthorRepository>();

        var authorId = 1;

        var lockedAuthor = new Author
        {
            Id = authorId,
            Name = "Locked Author",
            IsLocked = true
        };

        authorRepository.GetById(authorId).Returns(lockedAuthor);

        var sut = new AddPostService(postRepository, authorRepository);

        // Act / Assert
        Assert.Throws<InvalidOperationException>(() => sut.AddPost(authorId));
        postRepository.DidNotReceive().CreatePost(Arg.Any<int>());
    }
}