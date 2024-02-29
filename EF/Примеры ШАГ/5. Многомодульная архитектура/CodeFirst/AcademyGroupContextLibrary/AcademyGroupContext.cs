using Microsoft.EntityFrameworkCore;
using AcademyGroupLibrary;
using Microsoft.Extensions.Configuration;

namespace AcademyGroupContextLibrary
{
    // Для работы с БД MS SQL Server необходимо добавить пакет:
    // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)

    // Lazy loading или ленивая загрузка предполагает неявную автоматическую загрузку связанных данных при обращении к навигационному свойству.
    // Microsoft.EntityFrameworkCore.Proxies

    public class AcademyGroupContext : DbContext
    {
        static DbContextOptions<AcademyGroupContext> _options;

        static AcademyGroupContext()        
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AcademyGroupContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public AcademyGroupContext()
            : base(_options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AcademyGroup> AcademyGroups { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // метод UseLazyLoadingProxies() делает доступной ленивую загрузку.
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
