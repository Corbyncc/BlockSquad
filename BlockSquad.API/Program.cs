using BlockSquad.Api.Gslt;
using BlockSquad.Api.Lobbies.Services;
using BlockSquad.Api.ServerProviders;
using BlockSquad.API.Database;
using BlockSquad.API.Users.Services;
using BlockSquad.Shared;
using BlockSquad.Shared.Lobbies;
using BlockSquad.Shared.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Database connection string not found")));

builder.Services.AddBlockSquadShared();

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddSingleton<ILobbiesService, LobbiesService>();
builder.Services.AddTransient<IServerProvider, AzureServerProvider>();
builder.Services.AddSingleton<IGsltService, GsltService>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
