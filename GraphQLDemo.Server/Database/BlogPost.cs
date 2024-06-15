using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.Server.Database;

public class BlogPost
{
    public Guid Id { get; set; }
    
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;
    
    [MaxLength(16000)]
    public string Text { get; set; } = string.Empty;

    public IEnumerable<BlogTag> Tags { get; set; } = default!;
    
    public BlogAuthor Author { get; set; } = default!;
}