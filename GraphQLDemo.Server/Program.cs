using System.Diagnostics;
using GraphQLDemo.Server.Database;
using GraphQLDemo.Server.Schema.Queries;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Setup AutoMapper to enable mappings from
        // database entities to schema entities
        builder.Services
            .AddAutoMapper(typeof(Program));
        
        // Setup database
        var dbPath = System.IO.Path.Combine( new System.IO.FileInfo(typeof(Program).Assembly.Location).DirectoryName!,
            "graphqldemo.db");
        builder.Services
            .AddPooledDbContextFactory<BlogContext>( options => 
                options.UseSqlite($"Data Source={dbPath}")
                    .EnableSensitiveDataLogging()
                    .LogTo(message => Debug.WriteLine(message)));
        
        // Enable authorization
        builder.Services
            .AddAuthorization();
            
        // Setup GraphQL
        builder.Services
            .AddGraphQLServer()
            .RegisterDbContext<BlogContext>(DbContextKind.Pooled)
            .AddQueryType<Query>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryableCursorPagingProvider();

        var app = builder.Build();

        // Initialize the database
        await using (var scope = app.Services.CreateAsyncScope())
            await BlogContext.Initialize(scope);
        
        // Setup middlewares
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapGraphQL();
        
        // Start server
        await app.RunAsync();
    }
}