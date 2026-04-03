using Uqs.Blog.Domain.Repositories;

namespace Uqs.Blog.Domain.Services;

public class UpdateTitleService
{
    private readonly IPostRepository _postRepository;
    private const int TITLE_MAX_LENGTH = 90;
    public UpdateTitleService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public void UpdateTitle(int postId, string title)
    {
        
    }
}
