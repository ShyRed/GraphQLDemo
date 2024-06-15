namespace GraphQLDemo.Server.Schema.Queries;

public class BlogTagType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public IEnumerable<BlogPostType> BlogPosts { get; set; } = default!;   
}