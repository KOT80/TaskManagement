using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        TaskItem task,
        CancellationToken cancellationToken)
    {
        await _context.Tasks.AddAsync(task, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<TaskItem?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(
                task => task.Id == id,
                cancellationToken);
    }
    
    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(
        bool? isCompleted,
        CancellationToken cancellationToken)
    {
        var query = _context.Tasks.AsNoTracking();

        if (isCompleted.HasValue)
        {
            query = query.Where(task =>
                task.IsCompleted == isCompleted.Value);
        }

        return await query
            .OrderByDescending(task => task.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    
    public Task UpdateAsync(
        TaskItem task,
        CancellationToken cancellationToken)
    {
        _context.Tasks.Update(task);

        return _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(
                task => task.Id == id,
                cancellationToken);

        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}