using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CodeFirstManyToMany
{
    // Для работы с БД MS SQL Server необходимо добавить пакет:
    // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)

    // Microsoft.Extensions.Configuration.Json. Этот пакет специально предназначен для работы с конфигурацией в формате json.

    public class LanguageContext : DbContext
    {
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Language> Languages { get; set; }

        public LanguageContext(DbContextOptions<LanguageContext> options)
            : base(options)
        { 
            if (Database.EnsureCreated())
            {
                Language lang1 = new Language { Name = "Английский" };
                Language lang2 = new Language { Name = "Испанский" };
                Language lang3 = new Language { Name = "Французский" };
                Language lang4 = new Language { Name = "Португальский" };

                Languages.Add(lang1);
                Languages.Add(lang2);
                Languages.Add(lang3);
                Languages.Add(lang4);

                Continent c1 = new Continent
                {
                    Name = "Африка",
                    Languages = new List<Language>() { lang1, lang3, lang4 }
                };
                Continent c2 = new Continent
                {
                    Name = "Южная Америка",
                    Languages = new List<Language>() { lang2, lang4 }
                };
                Continent c3 = new Continent
                {
                    Name = "Европа",
                    Languages = new List<Language>() { lang1, lang2, lang3, lang4 }
                };

                Continents.Add(c1);
                Continents.Add(c2);
                Continents.Add(c3);
                SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-G30VB0K\MSSQLSERVER01;Database=ManyToMany;Integrated Security=SSPI;TrustServerCertificate=true");          
        }
    }
}