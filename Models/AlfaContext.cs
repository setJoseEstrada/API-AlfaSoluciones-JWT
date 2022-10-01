using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Alfa.Models
{
    public partial class AlfaContext : DbContext
    {
        public AlfaContext()
        {
        }

        public AlfaContext(DbContextOptions<AlfaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Beca> Becas { get; set; }
        public virtual DbSet<BecasAlumno> BecasAlumnos { get; set; }
        public virtual DbSet<TipoBeca> TipoBecas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server= JOSE-ESTRADA\\MSSQLSERVER01;Database=Alfa; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Genero).HasColumnName("genero");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Beca>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Becas)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK__Becas__id_tipo__286302EC");
            });

            modelBuilder.Entity<BecasAlumno>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAlumno).HasColumnName("idAlumno");

                entity.Property(e => e.IdBecas).HasColumnName("idBecas");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.BecasAlumnos)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("FK__BecasAlum__idAlu__2B3F6F97");

                entity.HasOne(d => d.IdBecasNavigation)
                    .WithMany(p => p.BecasAlumnos)
                    .HasForeignKey(d => d.IdBecas)
                    .HasConstraintName("FK__BecasAlum__idBec__2C3393D0");
            });

            modelBuilder.Entity<TipoBeca>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
