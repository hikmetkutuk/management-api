using ManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
}