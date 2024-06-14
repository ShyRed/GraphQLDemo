﻿namespace GraphQLDemo.Server.Database;

public class BlogPost
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public string Text { get; set; } = string.Empty;
}