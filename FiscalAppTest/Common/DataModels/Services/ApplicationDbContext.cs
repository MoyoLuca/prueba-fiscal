using DataModels.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataModels.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Addendas> Addendas { get; set; }

        // Otras entidades y configuraciones aquí

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la clave primaria para la entidad Addendas
            modelBuilder.Entity<Addendas>().HasKey(a => a.IdAddenda);

            // Otras configuraciones de entidades aquí

            base.OnModelCreating(modelBuilder);
        }
    }
}
