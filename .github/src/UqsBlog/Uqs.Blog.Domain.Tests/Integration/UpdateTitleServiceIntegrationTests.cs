using NSubstitute;
using Uqs.Blog.Domain.DomainObjects;
using Uqs.Blog.Domain.Repositories;
using Uqs.Blog.Domain.Services;
using Xunit;

namespace Uqs.Blog.Domain.Tests.Integration;

public class UpdateTitleServiceIntegrationTests
{
    [Fact]
    public void UpdateTitle_ValidTitle_CallsRepositoryUpdateOnce()
    {
        // Arrange
        var postRepository = Substitute.For<IPostRepository>();

        var postId = 1;
        var post = new Post
        {
            Id = postId,
            Title = "Old Title"
        };

        postRepository.GetById(postId).Returns(post);

        var sut = new UpdateTitleService(postRepository);

        // Act
        sut.UpdateTitle(postId, "New Valid Title");

        // Assert
        postRepository.Received(1).Update(Arg.Is<Post>(p =>
            p.Id == postId &&
            p.Title == "New Valid Title"
        ));
    }

    [Fact]
    public void UpdateTitle_TitleLongerThan90_DoesNotCallRepositoryUpdate()
    {
        // Arrange
        var postRepository = Substitute.For<IPostRepository>();

        var postId = 1;
        var post = new Post
        {
            Id = postId,
            Title = "Old Title"
        };

        postRepository.GetById(postId).Returns(post);

        var sut = new UpdateTitleService(postRepository);
        var tooLongTitle = new string('A', 91);

        // Act / Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            sut.UpdateTitle(postId, tooLongTitle));

        postRepository.DidNotReceive().Update(Arg.Any<Post>());
    }
}