using Microsoft.EntityFrameworkCore;
using WebAPiAsist.Models;

namespace WebAPiAsist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<App> App { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<App>()
                .HasOne(a => a.Usuario)           
                .WithMany(u => u.App)            
                .HasForeignKey(a => a.UsuarioId)  
                .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
