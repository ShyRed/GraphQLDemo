using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.Server.Database;

public class BlogTag
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public IEnumerable<BlogPost> BlogPosts { get; set; } = default!;
}