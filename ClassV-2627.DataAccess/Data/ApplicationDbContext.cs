using GlassV_2627.Models;
using Microsoft.EntityFrameworkCore;

namespace GlassV_2627.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    
    public DbSet<Teacher> Teachers { get; set; }
}