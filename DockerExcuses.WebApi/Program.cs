using DockerExcuses.WebApi.Models;
using DockerExcuses.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<InMemoryDataStore>();
builder.Services.AddScoped<IExcuseRepository, InMemoryExcuseRepository>();

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

app.MapGet("/excuses", async (IExcuseRepository excuseRepository) =>
{
    try
    {
        var excuses = await excuseRepository.GetAllExcuses();
        return Results.Ok(excuses);
    }
    catch (Exception e)
    {
        return Results.Problem("An error occurred while retrieving excuses: " + e.Message);
    }
}).WithName("GetAllExcuses");

app.MapGet("/excuses/{id:int:min(0)}", async (int id, IExcuseRepository excuseRepository) =>
{
    try
    {
        var excuse = await excuseRepository.GetExcuseById(id);
        return excuse == null
            ? Results.NotFound()
            : Results.Ok(excuse);
    }
    catch (Exception e)
    {
        return Results.Problem("An error occurred while retrieving the excuse: " + e.Message);
    }
}).WithName("GetExcuseById");

app.MapPost("/excuses", async (ExcuseInputDto excuse, IExcuseRepository excuseRepository) =>
{
    try
    {
        var createdExcuse = await excuseRepository.AddExcuse(excuse);
        return Results.Created($"/excuses/{createdExcuse.Id}", createdExcuse);
    }
    catch (Exception e)
    {
        return Results.Problem("An error occurred while adding the excuse: " + e.Message);
    }
}).WithName("AddExcuse");

app.MapPut("/excuses/{id:int:min(0)}", async (int id, ExcuseInputDto excuse, IExcuseRepository excuseRepository) =>
{
    try
    {
        var updatedExcuse = await excuseRepository.UpdateExcuseById(id, excuse);
        return updatedExcuse == null
            ? Results.NotFound()
            : Results.Ok(updatedExcuse);
    }
    catch (Exception e)
    {
        return Results.Problem("An error occurred while updating the excuse: " + e.Message);
    }
}).WithName("UpdateExcuse");

app.MapDelete("/excuses/{id:int}", async (int id, IExcuseRepository excuseRepository) =>
{
    try
    {
        var deleted = await excuseRepository.DeleteExcuseById(id);
        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
    catch (Exception e)
    {
        return Results.Problem("An error occurred while deleting the excuse: " + e.Message);
    }
}).WithName("DeleteExcuse");

app.Run();