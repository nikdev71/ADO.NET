using Microsoft.EntityFrameworkCore;

namespace VUZCodeFirst;

public partial class VuzContext : DbContext
{
    public VuzContext()
    {
    }

    public VuzContext(DbContextOptions<VuzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Lecture> Lectures { get; set; }

    public virtual DbSet<LectureType> LectureTypes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Sgroup> Sgroups { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-G30VB0K\\MSSQLSERVER01;Database=VUZ;Integrated Security=SSPI;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepPk).HasName("dep_prk");

            entity.ToTable("DEPARTMENT");

            entity.HasIndex(e => new { e.Name, e.FacFk }, "dep_unq_nam_frk").IsUnique();

            entity.Property(e => e.DepPk)
                .ValueGeneratedNever()
                .HasColumnName("DepPK");
            entity.Property(e => e.Building)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FacFk).HasColumnName("FacFK");
            entity.Property(e => e.Fund).HasColumnType("numeric(7, 2)");
            entity.Property(e => e.Head)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.FacFkNavigation).WithMany(p => p.Departments)
                .HasForeignKey(d => d.FacFk)
                .HasConstraintName("dep_frk_fac");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacPk).HasName("fac_prk");

            entity.ToTable("FACULTY");

            entity.HasIndex(e => e.Dean, "fac_unq_den").IsUnique();

            entity.HasIndex(e => e.Name, "fac_unq_nam").IsUnique();

            entity.Property(e => e.FacPk)
                .ValueGeneratedNever()
                .HasColumnName("FacPK");
            entity.Property(e => e.Building)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Dean)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Fund).HasColumnType("numeric(7, 2)");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Lecture>(entity =>
        {
            entity.HasKey(e => e.LecPk).HasName("lec_prk");

            entity.ToTable("LECTURE");

            entity.HasIndex(e => new { e.TchFk, e.Week, e.DayWeek, e.Lesson }, "lec_unq1").IsUnique();

            entity.HasIndex(e => new { e.GrpFk, e.Week, e.DayWeek, e.Lesson }, "lec_unq2").IsUnique();

            entity.HasIndex(e => new { e.RomFk, e.Week, e.DayWeek, e.Lesson }, "lec_unq3").IsUnique();

            entity.Property(e => e.LecPk)
                .ValueGeneratedNever()
                .HasColumnName("lecPK");
            entity.Property(e => e.DayWeek)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Day_week");
            entity.Property(e => e.GrpFk).HasColumnName("GrpFK");
            entity.Property(e => e.IdType).HasColumnName("id_type");
            entity.Property(e => e.Lesson).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.RomFk).HasColumnName("RomFK");
            entity.Property(e => e.SbjFk).HasColumnName("SbjFK");
            entity.Property(e => e.TchFk).HasColumnName("TchFK");
            entity.Property(e => e.Week).HasColumnType("numeric(1, 0)");

            entity.HasOne(d => d.GrpFkNavigation).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.GrpFk)
                .HasConstraintName("lec_frk_grp");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lec_typ");

            entity.HasOne(d => d.RomFkNavigation).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.RomFk)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("lec_frk_rom");

            entity.HasOne(d => d.SbjFkNavigation).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.SbjFk)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("lec_frk_sbj");

            entity.HasOne(d => d.TchFkNavigation).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.TchFk)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("lec_frk_tch");
        });

        modelBuilder.Entity<LectureType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LECTURE___3213E83FA5BEF071");

            entity.ToTable("LECTURE_TYPE");

            entity.HasIndex(e => e.Name, "UQ__LECTURE___72E12F1B3CF51D88").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__POST__3213E83F949C352C");

            entity.ToTable("POST");

            entity.HasIndex(e => e.Name, "UQ__POST__72E12F1B9FC6243D").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RomPk).HasName("rom_prk");

            entity.ToTable("ROOM");

            entity.HasIndex(e => new { e.Num, e.Building }, "rom_unq_num_bld").IsUnique();

            entity.Property(e => e.RomPk)
                .ValueGeneratedNever()
                .HasColumnName("RomPK");
            entity.Property(e => e.Building)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Floor).HasColumnType("numeric(2, 0)");
            entity.Property(e => e.Num).HasColumnType("numeric(3, 0)");
            entity.Property(e => e.Seats).HasColumnType("numeric(3, 0)");
        });

        modelBuilder.Entity<Sgroup>(entity =>
        {
            entity.HasKey(e => e.GrpPk).HasName("grp_prk");

            entity.ToTable("SGROUP");

            entity.HasIndex(e => new { e.Num, e.Kurs, e.DepFk }, "grp_unq_num_yar_dep").IsUnique();

            entity.Property(e => e.GrpPk)
                .ValueGeneratedNever()
                .HasColumnName("GrpPK");
            entity.Property(e => e.CurFk).HasColumnName("CurFK");
            entity.Property(e => e.DepFk).HasColumnName("DepFK");
            entity.Property(e => e.Kurs).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Num).HasColumnType("numeric(3, 0)");
            entity.Property(e => e.Quantity).HasColumnType("numeric(2, 0)");
            entity.Property(e => e.Rating)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(3, 0)");

            entity.HasOne(d => d.CurFkNavigation).WithMany(p => p.Sgroups)
                .HasForeignKey(d => d.CurFk)
                .HasConstraintName("grp_frk_tch");

            entity.HasOne(d => d.DepFkNavigation).WithMany(p => p.Sgroups)
                .HasForeignKey(d => d.DepFk)
                .HasConstraintName("grp_frk_dep");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SbjPk).HasName("sbj_prk");

            entity.ToTable("SUBJECT");

            entity.HasIndex(e => e.Name, "UQ__SUBJECT__737584F6608EA08D").IsUnique();

            entity.Property(e => e.SbjPk)
                .ValueGeneratedNever()
                .HasColumnName("SbjPK");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TchPk).HasName("tch_prk");

            entity.ToTable("TEACHER");

            entity.HasIndex(e => new { e.Name, e.Idcode }, "UNIQUE_NAME").IsUnique();

            entity.Property(e => e.TchPk)
                .ValueGeneratedNever()
                .HasColumnName("TchPK");
            entity.Property(e => e.DepFk).HasColumnName("DepFK");
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.IdPost).HasColumnName("id_Post");
            entity.Property(e => e.Idcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("IDCode");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Rise)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(6, 2)");
            entity.Property(e => e.Salary)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(6, 2)");
            entity.Property(e => e.Tel)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.ChiefNavigation).WithMany(p => p.InverseChiefNavigation)
                .HasForeignKey(d => d.Chief)
                .HasConstraintName("tch_frk_tch");

            entity.HasOne(d => d.DepFkNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.DepFk)
                .HasConstraintName("tch_frk_dep");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_KEY_POST");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
