using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbFirst_Stationery_;

public partial class StationeryContext : DbContext
{
    public StationeryContext()
    {
    }

    public StationeryContext(DbContextOptions<StationeryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Firm> Firms { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Stationery> Stationeries { get; set; }

    public virtual DbSet<TypesOfStationery> TypesOfStationeries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VDC9A9A;Database=Stationery;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Firm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Firms__3214EC071F3AD7CE");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Managers__3214EC07E581D28F");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sales__3214EC072D1162AC");

            entity.Property(e => e.PricePerUnitSold).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Firm).WithMany(p => p.Sales)
                .HasForeignKey(d => d.FirmId)
                .HasConstraintName("FK__Sales__FirmId__412EB0B6");

            entity.HasOne(d => d.Manager).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__Sales__ManagerId__4222D4EF");

            entity.HasOne(d => d.Stationery).WithMany(p => p.Sales)
                .HasForeignKey(d => d.StationeryId)
                .HasConstraintName("FK__Sales__Stationer__403A8C7D");
        });

        modelBuilder.Entity<Stationery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Statione__3214EC07C3A8A0DB");

            entity.ToTable("Stationery");

            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.Type).WithMany(p => p.Stationeries)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Stationer__TypeI__398D8EEE");
        });

        modelBuilder.Entity<TypesOfStationery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TypesOfS__3214EC07890DB4B6");

            entity.ToTable("TypesOfStationery");

            entity.Property(e => e.Title).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
