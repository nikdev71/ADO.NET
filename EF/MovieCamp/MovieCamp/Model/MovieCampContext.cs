using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.Model
{
    internal class MovieCampContext : DbContext
    {
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Director> Directors { get; set;}
        public DbSet<Genre> Genres { get; set;}
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        static DbContextOptions<MovieCampContext> _options;

        static MovieCampContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection")!;

            var optionsBuilder = new DbContextOptionsBuilder<MovieCampContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }
        public MovieCampContext() : base(_options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.Property(d => d.Name).HasMaxLength(30);
                entity.Property(d => d.LastName).HasMaxLength(30);
                entity.Property(u => u.Age).HasDefaultValue(18);
                entity.ToTable(t => t.HasCheckConstraint("Age", "Age >0 AND Age <120"));

            });
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(d => d.Title).HasMaxLength(30);
                entity.HasIndex(i => new { i.Title }, "gnr_unq").IsUnique();

            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(d => d.Login).HasMaxLength(30);
                entity.Property(d => d.Password).HasMaxLength(20);
                entity.ToTable(t => t.HasCheckConstraint("Password", "LEN(Password) >7 AND LEN(Password) <21"));
            });
            modelBuilder.Entity<Movie>(entity =>
            {
                var minYear = 1900;
                entity.ToTable(t=>t.HasCheckConstraint($"CK_Movie_Year", $"Year >= {minYear}")) ;
            });
        }
    }
}
