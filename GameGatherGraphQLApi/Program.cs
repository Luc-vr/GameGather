using GameGatherGraphQLApi;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Graph QL
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

builder.Services.AddDbContext<GameGatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GameGatherDbConnection")));

builder.Services.AddControllers();

var app = builder.Build();

app.MapGraphQL("/graphql");

app.Run();
