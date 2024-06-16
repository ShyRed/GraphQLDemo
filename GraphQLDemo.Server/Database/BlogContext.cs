using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server.Database;

public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options)
{
    public DbSet<BlogPost> BlogPosts { get; init; }
    public DbSet<BlogTag> BlogTags { get; set; }
    public DbSet<BlogAuthor> BlogAuthors { get; init; }

    public static async Task Initialize(AsyncServiceScope scope)
    {
        // Create database context and apply entity framework migrations.
        var database = scope.ServiceProvider.GetRequiredService<BlogContext>();
        await database.Database.MigrateAsync();

        // Do not initialize database with fake data, if there is already
        // data present in the database.
        if (await database.BlogPosts!.AnyAsync())
            return;
        
        // Create fake data
        var blogAuthors = new Bogus.Faker<BlogAuthor>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.FirstName, f => f.Name.FirstName())
            .RuleFor(o => o.LastName, f => f.Name.LastName())
            .RuleFor(o => o.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .Generate(10);

        var blogTags = new Bogus.Faker<BlogTag>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Name, f => f.Hacker.Noun())
            .Generate(8)
            .ToArray();
        
        var blogPosts = new Bogus.Faker<BlogPost>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Title, f => "About " + f.Company.CompanyName())
            .RuleFor(o => o.Description, f => f.Company.CatchPhrase())
            .RuleFor(o => o.Text, f => f.Lorem.Text())
            .RuleFor(o => o.Tags, f => f.Random.ArrayElements<BlogTag>(blogTags))
            .RuleFor(o => o.Author, f => blogAuthors[RandomNumberGenerator.GetInt32(0, blogAuthors.Count)])
            .Generate(250);

        // Save fake data in database
        await database.BlogPosts!.AddRangeAsync(blogPosts);
        await database.SaveChangesAsync();
    }
}