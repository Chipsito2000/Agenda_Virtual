using System;
using System.Collections.Generic;
using Agenda_Virtual.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agenda_Virtual.Clases.DB
{
    public partial class AgendaContext : DbContext
    {
        public AgendaContext()
        {
        }

        public AgendaContext(DbContextOptions<AgendaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Visita> Visitas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=mraczesin0;Database=Agenda;Trusted_Connection=True; Integrated Security=True");
            }
            else
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contraseña");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Visita>(entity =>
            {
                entity.ToTable("visitas");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Alumno)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Escuela)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Municipio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Padre)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
