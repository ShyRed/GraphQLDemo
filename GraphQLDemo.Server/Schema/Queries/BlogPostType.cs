namespace GraphQLDemo.Server.Schema.Queries;

public class BlogPostType
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public string Text { get; set; } = string.Empty;
    
    public IEnumerable<BlogTagType> Tags { get; set; } = default!;
    
    public BlogAuthorType Author { get; set; } = default!;
}