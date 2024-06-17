using GraphQLDemo.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server.Schema.BlogPosts;

[QueryType]
public static class BlogAuthorQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<BlogAuthor> GetBlogAuthors(BlogContext blogContext)
        => blogContext.BlogAuthors;
    
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, BlogAuthor>> GetBlogAuthorsByIdAsync(
        IReadOnlyList<Guid> ids,
        BlogContext blogContext,
        CancellationToken cancellationToken)
        => await blogContext.BlogAuthors
            .Where(blogAuthor => ids.Contains(blogAuthor.Id))
            .ToDictionaryAsync(blogAuthor => blogAuthor.Id, cancellationToken)
            .ConfigureAwait(false);
}