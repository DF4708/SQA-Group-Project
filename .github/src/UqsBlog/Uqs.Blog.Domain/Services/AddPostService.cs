using Uqs.Blog.Domain.Repositories;

namespace Uqs.Blog.Domain.Services;

public interface IAddPostService
{
    int AddPost(int authorId);
}

public class AddPostService : IAddPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public AddPostService(IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }

    public int AddPost(int authorId)
    {
        // Guard: author must exist in the system before a post can be created
        var author = _authorRepository.GetById(authorId);
        if (author is null)
        {
            throw new ArgumentException("Author Id not found", nameof(authorId));
        }

        // Guard: locked authors are not permitted to create new posts
        if (author.IsLocked)
        {
            throw new InvalidOperationException("The author is locked");
        }

        // Persist: all guards passed — create the post and return the new ID
        var newPostId = _postRepository.CreatePost(authorId);
        return newPostId;
    }
}