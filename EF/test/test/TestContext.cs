using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace test;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenresMovie> GenresMovies { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VDC9A9A;Database=test;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<GenresMovie>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GenresMovie");

            entity.HasOne(d => d.Genre).WithMany()
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GenresMovie_Genres");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GenresMovie_movie");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("movie");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
