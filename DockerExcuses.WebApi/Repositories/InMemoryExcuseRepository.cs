using DockerExcuses.WebApi.Models;

namespace DockerExcuses.WebApi.Repositories;

public class InMemoryDataStore
{
    public readonly List<Excuse> Excuses =
    [
        new() { Id = 1, Text = "My computer exploded", Category = "work" },
        new() { Id = 2, Text = "My cat hid my car keys", Category = "pets" },
        new() { Id = 3, Text = "Gravity stopped working for me temporarily", Category = "general" }
    ];

    public int NextExcuseId = 3;
}

public class InMemoryExcuseRepository : IExcuseRepository
{
    private readonly InMemoryDataStore _dataStore;

    public InMemoryExcuseRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public Task<List<Excuse>> GetAllExcuses() => Task.FromResult(
        _dataStore.Excuses
    );

    public Task<Excuse?> GetExcuseById(int id) => Task.FromResult(
        _dataStore.Excuses.FirstOrDefault(e => e.Id == id)
    );

    public Task<Excuse> AddExcuse(ExcuseInputDto excuse)
    {
        _dataStore.NextExcuseId += 1;
        var newExcuse = new Excuse
        {
            Id = _dataStore.NextExcuseId,
            Text = excuse.Text,
            Category = excuse.Category
        };
        _dataStore.Excuses.Add(newExcuse);
        return Task.FromResult(newExcuse);
    }

    public Task<Excuse?> UpdateExcuseById(int id, ExcuseInputDto excuse)
    {
        var existingExcuse = _dataStore.Excuses.FirstOrDefault(e => e.Id == id);
        if (existingExcuse != null)
        {
            existingExcuse.Text = excuse.Text;
            existingExcuse.Category = excuse.Category;
        }

        return Task.FromResult(existingExcuse);
    }

    public Task<bool> DeleteExcuseById(int id)
    {
        var existingExcuse = _dataStore.Excuses.FirstOrDefault(e => e.Id == id);
        if (existingExcuse == null)
        {
            return Task.FromResult(false);
        }

        _dataStore.Excuses.Remove(existingExcuse);
        return Task.FromResult(true);
    }
}