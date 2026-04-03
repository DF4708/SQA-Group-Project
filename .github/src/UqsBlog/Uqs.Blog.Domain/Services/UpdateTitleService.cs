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
        // Normalize: null title is treated as empty string
        if (title is null)
        {
            title = string.Empty;
        }

        // Normalize: trim leading and trailing whitespace before validation
        title = title.Trim();

        // Guard: title must not exceed maximum allowed length
        if (title.Length > TITLE_MAX_LENGTH)
        {
            throw new ArgumentOutOfRangeException(nameof(title),
                $"The title can be a max of {TITLE_MAX_LENGTH} letters");
        }

        // Guard: post must exist before title can be updated
        var post = _postRepository.GetById(postId);
        if (post is null)
        {
            throw new ArgumentException($"Unable to find a post of Id {postId}",
                nameof(post));
        }

        // Persist: apply the new title and save via repository
        post.Title = title;
        _postRepository.Update(post);
    }
}