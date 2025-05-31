using DockerExcuses.Persistence.Shared.Models;

namespace DockerExcuses.Persistence.Shared.Repositories;

public interface IExcuseRepository
{
    Task<List<Excuse>> GetAllExcuses();
    Task<Excuse?> GetExcuseById(int id);
    Task<Excuse> AddExcuse(ExcuseInputDto excuse);
    Task<Excuse?> UpdateExcuseById(int id, ExcuseInputDto excuse);
    Task<bool> DeleteExcuseById(int id);
}