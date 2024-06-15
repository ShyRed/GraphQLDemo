using AutoMapper;
using AutoMapper.QueryableExtensions;
using GraphQLDemo.Server.Database;

namespace GraphQLDemo.Server.Schema.Queries;

public class Query
{
    [UsePaging(MaxPageSize = 50, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<BlogPostType> GetBlogPosts(BlogContext blogContext, [Service] IMapper mapper)
    {
        return blogContext.BlogPosts!.ProjectTo<BlogPostType>(mapper.ConfigurationProvider);
    }

    [UsePaging(MaxPageSize = 50, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<BlogAuthorType> GetBlogAuthors(BlogContext blogContext, [Service] IMapper mapper)
    {
        return blogContext.BlogAuthors!.ProjectTo<BlogAuthorType>(mapper.ConfigurationProvider);
    }
    
    [UsePaging(MaxPageSize = 50, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<BlogTagType> GetBlogTags(BlogContext blogContext, [Service] IMapper mapper)
    {
        return blogContext.BlogTags!.ProjectTo<BlogTagType>(mapper.ConfigurationProvider);
    }
}