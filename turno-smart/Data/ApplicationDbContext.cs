using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using turno_smart.Models;

namespace turno_smart.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuarios>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.Id);  // define ID como primary key

        }

         public DbSet<Paciente> Pacientes { get; set; } = default!;
         public DbSet<Medico> Medicos { get; set; } = default!;
         public DbSet<Especialidad> Especialidades { get; set; } = default!;
         public DbSet<Turno> Turnos { get; set; } = default!;
         public DbSet<Estudio> Estudios { get; set; } = default!;
         public DbSet<HistorialMedico> HistorialesMedicos { get; set; } = default!;
        //public IEnumerable<object> Disponibilidades { get; internal set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
    }
}
