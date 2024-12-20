using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using turno_smart.Models;

namespace turno_smart.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuarios>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect()) dbCreator.Create();
                    if (!dbCreator.HasTables()) dbCreator.CreateTables();
                }
            }
            catch (Exception)
            {
                throw;
            }
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
            // Relación Usuario -> Paciente (1:1)
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Usuario)
                .WithOne()
                .HasForeignKey<Paciente>(p => p.DNI)
                .HasPrincipalKey<Usuarios>(u => u.DNI) // Establecer DNI como clave principal
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Usuario -> Medico (1:1)
            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Usuario)
                .WithOne()
                .HasForeignKey<Medico>(m => m.DNI)
                .HasPrincipalKey<Usuarios>(u => u.DNI) // Establecer DNI como clave principal
                .OnDelete(DeleteBehavior.Restrict);
            // Relación Usuario -> Paciente (1:1) desde Usuario hacia Paciente
            modelBuilder.Entity<Usuarios>()
                .HasOne(u => u.Paciente)  // Usuario tiene un solo Paciente
                .WithOne(p => p.Usuario)  // Paciente tiene un solo Usuario
                .HasForeignKey<Paciente>(p => p.DNI)  // La clave foránea en Paciente será DNI
                .HasPrincipalKey<Usuarios>(u => u.DNI)  // La clave principal de Usuario es DNI
                .OnDelete(DeleteBehavior.Restrict);  // Restricción en la eliminación

            // Relación Usuario -> Medico (1:1) desde Usuario hacia Medico
            modelBuilder.Entity<Usuarios>()
                .HasOne(u => u.Medico)  // Usuario tiene un solo Medico
                .WithOne(m => m.Usuario)  // Medico tiene un solo Usuario
                .HasForeignKey<Medico>(m => m.DNI)  // La clave foránea en Medico será DNI
                .HasPrincipalKey<Usuarios>(u => u.DNI)  // La clave principal de Usuario es DNI
                .OnDelete(DeleteBehavior.Restrict);  // Restricción en la eliminación
            modelBuilder.Entity<Usuarios>()
                .HasIndex(u => u.DNI)
                .IsUnique(); // DNI debe ser único
            // Relación Especialidad -> Medico (1:N)
            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Especialidad)
                .WithMany(e => e.Medicos)
                .HasForeignKey(m => m.IdEspecialidad)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Paciente -> Turnos (1:N)
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Paciente)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Medico -> Turnos (1:N)
            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Medico)
                .WithMany(m => m.Turnos)
                .HasForeignKey(t => t.IdMedico)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Paciente -> HistorialMedico (1:N)
            modelBuilder.Entity<HistorialMedico>()
                .HasOne(h => h.Paciente)
                .WithMany(p => p.HistorialMedico)
                .HasForeignKey(h => h.IdPaciente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Medico -> HistorialMedico (1:N)
            modelBuilder.Entity<HistorialMedico>()
                .HasOne(h => h.Medico)
                .WithMany(m => m.HistorialMedico)
                .HasForeignKey(h => h.IdMedico)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Estudio -> HistorialMedico (1:N)
            //modelBuilder.Entity<HistorialMedico>()
            //    .HasOne(h => h.Estudio)
            //    .WithMany(e => e.HistorialMedico)
            //    .HasForeignKey(h => h.IdEstudio)
            //    .OnDelete(DeleteBehavior.Restrict);

        }

         public DbSet<Paciente> Pacientes { get; set; } = default!;
         public DbSet<Medico> Medicos { get; set; } = default!;
         public DbSet<Especialidad> Especialidades { get; set; } = default!;
         public DbSet<Turno> Turnos { get; set; } = default!;
         public DbSet<Estudio> Estudios { get; set; } = default!;
         public DbSet<HistorialMedico> HistorialesMedicos { get; set; } = default!;
        public DbSet<CentroMedico> CentroMedico { get; set; } = default!;
        public DbSet<Recepcionista> Recepcionistas { get; set; } = default!;

    }
}
