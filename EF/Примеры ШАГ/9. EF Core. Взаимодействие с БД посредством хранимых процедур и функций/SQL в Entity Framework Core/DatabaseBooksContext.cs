using Microsoft.EntityFrameworkCore;

namespace SQL_в_Entity_Framework_Core;

public partial class DatabaseBooksContext : DbContext
{
    public DatabaseBooksContext()
    {
    }

    public DatabaseBooksContext(DbContextOptions<DatabaseBooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G30VB0K\\MSSQLSERVER01;Database=Database_Books;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("aaaaabooks_PK")
                .IsClustered(false);

            entity.ToTable("books");

            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.Comment).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Press).HasMaxLength(50);
            entity.Property(e => e.Themes).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
