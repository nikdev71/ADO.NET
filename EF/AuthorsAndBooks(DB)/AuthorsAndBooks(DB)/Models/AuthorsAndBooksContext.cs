using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthorsAndBooks_DB_.Models;

public partial class AuthorsAndBooksContext : DbContext
{
    public AuthorsAndBooksContext()
    {
    }

    public AuthorsAndBooksContext(DbContextOptions<AuthorsAndBooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=DESKTOP-VDC9A9A;Database=AuthorsAndBooks;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07C797EE72");

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC0712CB6B13");

            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorId__398D8EEE");
        });

        modelBuilder.Entity<Book>()
       .HasOne(b => b.Author)
       .WithMany(a => a.Books)
       .HasForeignKey(b => b.AuthorId)
       .OnDelete(DeleteBehavior.Cascade);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
