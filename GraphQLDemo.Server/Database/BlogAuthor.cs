using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.Server.Database;

public class BlogAuthor
{
    public Guid Id { get; set; }
    
    [MaxLength(255)]
    public string UserName { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string FirstName { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string LastName { get; set; } = string.Empty;
    
    public ICollection<BlogPost> BlogPosts { get; set; } = default!;
}