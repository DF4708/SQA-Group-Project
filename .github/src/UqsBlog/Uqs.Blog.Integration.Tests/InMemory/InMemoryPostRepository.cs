using Uqs.Blog.Domain.DomainObjects;
using Uqs.Blog.Domain.Repositories;

namespace Uqs.Blog.Integration.Tests.InMemory;

public class InMemoryPostRepository : IPostRepository
{
    private readonly Dictionary<int, Post> _posts = new();
    private int _nextId = 1;

    public int CreatePost(int authorId)
    {
        var post = new Post
        {
            Id = _nextId++,
            Title = string.Empty
        };

        _posts[post.Id] = post;
        return post.Id;
    }

    public Post? GetById(int id)
    {
        _posts.TryGetValue(id, out var post);
        return post;
    }

    public void Update(Post post)
    {
        _posts[post.Id] = post;
    }

    // Test helper
    public void Seed(Post post)
    {
        _posts[post.Id] = post;

        if (post.Id >= _nextId)
            _nextId = post.Id + 1;
    }

    // Helpful for assertions
    public int Count => _posts.Count;
}