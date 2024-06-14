using AutoMapper;
using GraphQLDemo.Server.Database;
using GraphQLDemo.Server.Schema.Queries;

namespace GraphQLDemo.Server.Profiles;

public sealed class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogPost, BlogPostType>();
    }
}