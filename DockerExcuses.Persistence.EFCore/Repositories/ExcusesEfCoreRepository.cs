using DockerExcuses.Persistence.EFCore.Data;
using DockerExcuses.Persistence.Shared.Models;
using DockerExcuses.Persistence.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DockerExcuses.Persistence.EFCore.Repositories;

public class ExcusesEfCoreRepository : IExcuseRepository
{
    private readonly ApiDbContext _context;
    private readonly ILogger<ExcusesEfCoreRepository> _logger;

    public ExcusesEfCoreRepository(
        ApiDbContext context,
        ILogger<ExcusesEfCoreRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Excuse>> GetAllExcuses()
    {
        try
        {
            return await _context.Excuses
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving excuses");
            throw;
        }
    }

    public async Task<Excuse?> GetExcuseById(int id)
    {
        try
        {
            return await _context.Excuses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the excuse");
            throw;
        }
    }

    public async Task<Excuse> AddExcuse(ExcuseInputDto excuse)
    {
        try
        {
            var newExcuse = new Excuse
            {
                Text = excuse.Text,
                Category = excuse.Category
            };
            await _context.Excuses.AddAsync(newExcuse);
            await _context.SaveChangesAsync();
            return newExcuse;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while adding the excuse");
            throw;
        }
    }

    public async Task<Excuse?> UpdateExcuseById(int id, ExcuseInputDto excuse)
    {
        try
        {
            var existingExcuse = await _context.Excuses.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExcuse != null)
            {
                existingExcuse.Text = excuse.Text;
                existingExcuse.Category = excuse.Category;
                await _context.SaveChangesAsync();
            }

            return existingExcuse;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while updating the excuse");
            throw;
        }
    }

    public async Task<bool> DeleteExcuseById(int id)
    {
        try
        {
            var existingExcuse = await _context.Excuses.FirstOrDefaultAsync(e => e.Id == id);
            if (existingExcuse != null)
            {
                _context.Excuses.Remove(existingExcuse);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the excuse");
            throw;
        }
    }
}