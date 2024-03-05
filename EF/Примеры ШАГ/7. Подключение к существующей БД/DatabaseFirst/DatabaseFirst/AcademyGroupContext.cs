using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst;

public partial class AcademyGroupContext : DbContext
{
    public AcademyGroupContext()
    {
    }

    public AcademyGroupContext(DbContextOptions<AcademyGroupContext> options)
        : base(options)
    {
    }

    public DbSet<AcademyGroup> AcademyGroups { get; set; }

    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G30VB0K\\MSSQLSERVER01;Database=AcademyGroup;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademyGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AcademyGroups");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Students");

            entity.HasIndex(e => e.AcademyGroupId, "IX_AcademyGroup_Id");

            entity.Property(e => e.AcademyGroupId).HasColumnName("AcademyGroup_Id");

            entity.HasOne(d => d.AcademyGroup).WithMany(p => p.Students)
                .HasForeignKey(d => d.AcademyGroupId)
                .HasConstraintName("FK_dbo.Students_dbo.AcademyGroups_AcademyGroup_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
