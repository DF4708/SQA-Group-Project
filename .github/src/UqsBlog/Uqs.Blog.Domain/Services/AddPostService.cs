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
        return 0;
    }
}
