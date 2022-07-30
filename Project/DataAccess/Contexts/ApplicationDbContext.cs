using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.DataAccess.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product>? Products { get; set; }
}