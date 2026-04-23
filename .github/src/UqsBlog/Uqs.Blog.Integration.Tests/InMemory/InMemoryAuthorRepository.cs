using Uqs.Blog.Domain.DomainObjects;
using Uqs.Blog.Domain.Repositories;

namespace Uqs.Blog.Integration.Tests.InMemory;

public class InMemoryAuthorRepository : IAuthorRepository
{
    private readonly Dictionary<int, Author> _authors = new();

    public Author? GetById(int id)
    {
        _authors.TryGetValue(id, out var author);
        return author;
    }

    // Test helper
    public void Seed(Author author)
    {
        _authors[author.Id] = author;
    }
}