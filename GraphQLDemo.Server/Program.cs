using System.Diagnostics;
using GraphQLDemo.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Server;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Setup database
        var dbPath = System.IO.Path.Combine( new System.IO.FileInfo(typeof(Program).Assembly.Location).DirectoryName!,
            "graphqldemo.db");
        builder.Services
            .AddDbContext<BlogContext>( options => 
                options.UseSqlite($"Data Source={dbPath}")
                    .EnableSensitiveDataLogging()
                    .LogTo(message => Debug.WriteLine(message)));
        
        builder.Services
            .AddCors()
            .AddAuthorization();
            
        // Setup GraphQL
        builder.Services
            .AddGraphQLServer()
            .AddTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddQueryableCursorPagingProvider()
            .RegisterDbContext<BlogContext>();

        var app = builder.Build();

        // Initialize the database
        await using (var scope = app.Services.CreateAsyncScope())
            await BlogContext.Initialize(scope);
        
        // Setup middlewares
        app.UseHttpsRedirection();
        app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseAuthorization();
        app.MapGraphQL();
        
        // Start server
        await app.RunAsync();
    }
}