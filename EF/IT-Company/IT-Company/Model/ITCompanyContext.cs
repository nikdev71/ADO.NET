using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IT_Company.Model
{
    public class ITCompanyContext : DbContext
    {
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Staff> Employees { get; set; }

        static DbContextOptions<ITCompanyContext> _options;

        static ITCompanyContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection")!;

            var optionsBuilder = new DbContextOptionsBuilder<ITCompanyContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }
        public ITCompanyContext() : base(_options)
        {
            if (Database.EnsureCreated())
            {
                JobPosition job1 = new JobPosition { Title = "Software Developer" };
                JobPosition job2 = new JobPosition { Title = "QA" };
                JobPosition job3 = new JobPosition { Title = "System Administator" };

                JobPositions?.Add(job1);
                JobPositions?.Add(job2);
                JobPositions?.Add(job3);

                Staff employee1 = new Staff { Name = "Иван", Surname = "Иванов", Age = 30, JobPosition = job1 };
                Staff employee2 = new Staff { Name = "Петр", Surname = "Петров", Age = 25, JobPosition = job1 };
                Staff employee3 = new Staff { Name = "Анна", Surname = "Сидорова", Age = 28, JobPosition = job2 };
                Staff employee4 = new Staff { Name = "Мария", Surname = "Смирнова", Age = 35, JobPosition = job2 };
                Staff employee5 = new Staff { Name = "Алексей", Surname = "Николаев", Age = 40, JobPosition = job3 };
                Staff employee6 = new Staff { Name = "Елена", Surname = "Иванова", Age = 32, JobPosition = job3 };

                Employees?.Add(employee1);
                Employees?.Add(employee2);
                Employees?.Add(employee3);
                Employees?.Add(employee4);
                Employees?.Add(employee5);
                Employees?.Add(employee6);

                SaveChanges();

            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(u => u.Age).HasDefaultValue(18);
                entity.ToTable(t => t.HasCheckConstraint("Age", "Age >0 AND Age <120"));

                entity.Property(j => j.Name).HasMaxLength(30); 
                entity.Property(j => j.Surname).HasMaxLength(30);

                entity.Property(j => j.Surname).IsRequired();
                entity.Property(j => j.Name).IsRequired();

                entity.HasOne(j=> j.JobPosition).WithMany(x=>x.Staff).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<JobPosition>(entity =>
            {
                entity.Property(j => j.Title).HasMaxLength(30);
                entity.Property(j => j.Title).IsRequired();
                entity.HasIndex(i => new { i.Title }, "jb_unq").IsUnique();
            });

            
        }
    }
}
