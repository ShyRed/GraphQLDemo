using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server.Database;

public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
{
    public DbSet<BlogPost> BlogPosts { get; set; }

    public static async Task Initialize(AsyncServiceScope scope)
    {
        var blogPosts = new Bogus.Faker<BlogPost>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Title, f => "About " + f.Company.CompanyName())
            .RuleFor(o => o.Description, f => f.Company.CatchPhrase())
            .RuleFor(o => o.Text, f => f.Lorem.Text())
            .Generate(1000);

        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BlogContext>>();
        await using var database = await factory.CreateDbContextAsync();

        await database.BlogPosts!.AddRangeAsync(blogPosts);
        await database.SaveChangesAsync();
    }
}