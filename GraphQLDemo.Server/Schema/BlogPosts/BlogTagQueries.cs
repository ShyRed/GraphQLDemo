using GraphQLDemo.Server.Database;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server.Schema.BlogPosts;

[QueryType]
public static class BlogTagQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<BlogTag> GetBlogTags(BlogContext blogContext)
        => blogContext.BlogTags;

    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, BlogTag>> GetBlogTagsByIdAsync(
        IReadOnlyList<Guid> ids,
        BlogContext blogContext,
        CancellationToken cancellationToken)
        => await blogContext.BlogTags
            .Where(blogTag => ids.Contains(blogTag.Id))
            .ToDictionaryAsync(blogTag => blogTag.Id, cancellationToken)
            .ConfigureAwait(false);
}