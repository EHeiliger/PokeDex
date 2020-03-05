using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PokeDex.Models
{
    public partial class PokeDexContext : DbContext
    {
        public PokeDexContext()
        {
        }

        public PokeDexContext(DbContextOptions<PokeDexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attacks> Attacks { get; set; }
        public virtual DbSet<Pokemons> Pokemons { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<Types> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-68G0NVSO;DataBase=PokeDex;Trusted_Connection=True;Integrated Security=SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attacks>(entity =>
            {
                entity.Property(e => e.Attack)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Pokemons>(entity =>
            {
                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.HasOne(d => d.AttackId1Navigation)
                    .WithMany(p => p.PokemonsAttackId1Navigation)
                    .HasForeignKey(d => d.AttackId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pokemons_Attacks");

                entity.HasOne(d => d.AttackId2Navigation)
                    .WithMany(p => p.PokemonsAttackId2Navigation)
                    .HasForeignKey(d => d.AttackId2)
                    .HasConstraintName("FK_Pokemons_Attacks1");

                entity.HasOne(d => d.AttackId3Navigation)
                    .WithMany(p => p.PokemonsAttackId3Navigation)
                    .HasForeignKey(d => d.AttackId3)
                    .HasConstraintName("FK_Pokemons_Attacks2");

                entity.HasOne(d => d.AttackId4Navigation)
                    .WithMany(p => p.PokemonsAttackId4Navigation)
                    .HasForeignKey(d => d.AttackId4)
                    .HasConstraintName("FK_Pokemons_Attacks3");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Pokemons)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pokemons_Regions");

                entity.HasOne(d => d.TypeId1Navigation)
                    .WithMany(p => p.PokemonsTypeId1Navigation)
                    .HasForeignKey(d => d.TypeId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pokemons_Types");

                entity.HasOne(d => d.TypeId2Navigation)
                    .WithMany(p => p.PokemonsTypeId2Navigation)
                    .HasForeignKey(d => d.TypeId2)
                    .HasConstraintName("FK_Pokemons_Types1");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
