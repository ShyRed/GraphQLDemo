using GraphQLDemo.Server.Database;
using HotChocolate.Resolvers;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server.Schema.BlogPosts;

[QueryType]
public static class BlogPostQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<BlogPost> GetBlogPosts(BlogContext blogContext)
        => blogContext.BlogPosts;
    
    [DataLoader]
    public static async Task<IReadOnlyDictionary<Guid, BlogPost>> GetBlogPostsByIdAsync(
        IReadOnlyList<Guid> ids,
        BlogContext blogContext,
        CancellationToken cancellationToken)
        => await blogContext.BlogPosts
            .Where(blogPost => ids.Contains(blogPost.Id))
            .ToDictionaryAsync(blogPost => blogPost.Id, cancellationToken)
            .ConfigureAwait(false);
}