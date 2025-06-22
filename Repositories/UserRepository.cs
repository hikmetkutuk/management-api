using ManagementApi.Data;
using ManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Repositories;

public class UserRepository(AppDbContext context)
{
    public async Task<List<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}