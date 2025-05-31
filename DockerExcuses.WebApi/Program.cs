using DockerExcuses.Persistence.EFCore.Data;
using DockerExcuses.Persistence.EFCore.Repositories;
using DockerExcuses.Persistence.Shared.Repositories;
using DockerExcuses.WebApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// !: InMemory
// builder.Services.AddSingleton<InMemoryDataStore>();
// builder.Services.AddScoped<IExcuseRepository, InMemoryExcuseRepository>();

// !: EFCore
builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite("Data Source=excuses.db"));
builder.Services.AddScoped<IExcuseRepository, ExcusesEfCoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.MapExcuseEndpoints();

app.Run();