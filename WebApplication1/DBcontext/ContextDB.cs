using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.DBcontext
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options)  : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Incident> Incident { get; set; }
    }
}
