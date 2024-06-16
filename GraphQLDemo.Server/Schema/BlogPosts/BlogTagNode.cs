using GraphQLDemo.Server.Database;

namespace GraphQLDemo.Server.Schema.BlogPosts;

[Node]
[ExtendObjectType<BlogTag>]
public sealed class BlogTagNode
{
    [NodeResolver]
    public static async Task<BlogTag> GetBlogTagByIdAsync(
        Guid id,
        IBlogTagsByIdDataLoader blogTagById,
        CancellationToken cancellationToken)
        => await blogTagById
            .LoadAsync(id, cancellationToken)
            .ConfigureAwait(false);
}