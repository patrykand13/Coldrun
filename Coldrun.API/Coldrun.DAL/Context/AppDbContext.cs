using Coldrun.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coldrun.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<TruckEntity> Trucks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
