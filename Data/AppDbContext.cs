using SinemaDunyasi.Models;
using Microsoft.EntityFrameworkCore;
namespace SinemaDunyasi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Film> Filmler { get; set; }
    }
}
