using GraphQLDemo.Server.Database;

namespace GraphQLDemo.Server.Schema.BlogPosts;

[Node]
[ExtendObjectType<BlogTag>]
public sealed class BlogTagNode
{
    [NodeResolver]
    public static async Task<BlogTag> GetBlogTagByIdAsync(
        Guid id,
        IBlogTagsByIdDataLoader blogTagsById,
        CancellationToken cancellationToken)
        => await blogTagsById
            .LoadAsync(id, cancellationToken)
            .ConfigureAwait(false);
}