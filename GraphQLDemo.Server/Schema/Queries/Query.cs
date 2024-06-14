using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus;
using GraphQLDemo.Server.Database;

namespace GraphQLDemo.Server.Schema.Queries;

public class Query
{
    [UsePaging(MaxPageSize = 100, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<BlogPostType> GetBlogPosts(BlogContext context, [Service] IMapper mapper)
    {
        return context.BlogPosts!.ProjectTo<BlogPostType>(mapper.ConfigurationProvider);
    }
}