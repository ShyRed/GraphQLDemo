using GraphQLDemo.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server.Schema.BlogPosts;

[Node]
[ExtendObjectType<BlogPost>]
public sealed class BlogPostNode
{
    [BindMember(nameof(BlogPost.Tags), Replace = true)]
    public async Task< IEnumerable<BlogTag>> GetTagsAsync(
        [Parent] BlogPost blogPost,
        BlogContext blogContext,
        IBlogTagsByIdDataLoader blogTagsById,
        CancellationToken cancellationToken)
    {
        var tagIds = await blogContext.BlogPosts
            .Where(bp => bp.Id == blogPost.Id)
            .Include(bp => bp.Tags)
            .SelectMany(bp => bp.Tags.Select(t => t.Id))
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);
        return await blogTagsById
            .LoadAsync(tagIds, cancellationToken)
            .ConfigureAwait(false);
    }
}