using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.Server.Schema.Queries;

public class BlogAuthorType
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public IEnumerable<BlogPostType> BlogPosts { get; set; } = default!;
}