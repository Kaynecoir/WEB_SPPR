using Microsoft.EntityFrameworkCore;
using WEB_153503_Olszewski.Domain.Entities;

namespace WEB_153503_Olszewski.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
