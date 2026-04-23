using Uqs.Blog.Domain.DomainObjects;
using Uqs.Blog.Domain.Services;
using Uqs.Blog.Integration.Tests.InMemory;

namespace Uqs.Blog.Integration.Tests;

public class AddPostServiceIntegrationTests
{
    [Fact]
    public void AddPost_ValidAuthor_CreatesAndStoresPost()
    {
        //Arrange
        //Real in memory repository
        var postRepository = new InMemoryPostRepository();
        var authorRepository = new InMemoryAuthorRepository();

        //Seed for valid and unlocked author going into repo
        authorRepository.Seed(new Author
        {
            Id = 1,
            Name = "Valid Author",
            IsLocked = false
        });

        //SUT w/ real in mem repo
        var sut = new AddPostService(postRepository, authorRepository);
  
        //Act
        //Create post for valid author
        var postId = sut.AddPost(1);

        //Get post id for verification of persistence
        var createdPost = postRepository.GetById(postId);

        //Assert
        //Verify post creation and storage; and state/persistence
        Assert.NotNull(createdPost);
        Assert.Equal(postId, createdPost!.Id);
        Assert.Equal(1, postRepository.Count);
    }
        [Fact]
    public void AddPost_AuthorLocked_ThrowsAndDoesNotPersistPost()
    {
        //Arrange
        var postRepository = new InMemoryPostRepository();
        var authorRepository = new InMemoryAuthorRepository();

        //Seed a locked author
        authorRepository.Seed(new Author
        {
            Id = 1,
            Name = "Locked Author",
            IsLocked = true
        });
        
        //SUT
        var sut = new AddPostService(postRepository, authorRepository);
    
        //Act/Assert
        //Verify exception was thrown and no data persisted
        Assert.Throws<InvalidOperationException>(() => sut.AddPost(1));

        
        Assert.Equal(0, postRepository.Count);
    }

    [Fact]
    public void AddPost_AuthorDoesNotExist_ThrowsAndDoesNotPersistPost()
    {
        //Arrange
        var postRepository = new InMemoryPostRepository();
        var authorRepository = new InMemoryAuthorRepository();
    
    //SUT (No seed simulates missing author)
    var sut = new AddPostService(postRepository, authorRepository);
    //Act/Assert
    //Verify exception was thrown and no update to system occured
    Assert.Throws<ArgumentException>(() => sut.AddPost(6767));

    Assert.Equal(0, postRepository.Count);

    }
}