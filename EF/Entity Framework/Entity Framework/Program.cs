using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework
{
    public class CountryContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public CountryContext()
        {
            if(Database.EnsureCreated())
            {
                Continent continent1 = new Continent { Title = "Европа"  };
                Continent continent2 = new Continent { Title = "Северная Америка"  };
                Continent continent3 = new Continent { Title = "Южная Америка" };
                Continents?.Add(continent1);
                Continents?.Add(continent2);
                Continents?.Add(continent3);
                Countries?.Add( new Country { Title="США", Capital="Вашингтон", Square = 9834000, Population= 331.9, Continent = continent2 });
                Countries?.Add( new Country { Title="Канада", Capital="Оттава", Square = 9985000, Population= 38.25, Continent = continent2 });
                Countries?.Add( new Country { Title="Германия", Capital="Берлин", Square = 357592, Population = 83.2, Continent = continent1 });
                Countries?.Add( new Country { Title="Испания", Capital="Мадрид", Square = 506030, Population = 47.42, Continent = continent1 });
                Countries?.Add( new Country { Title="Португалия", Capital="Лиссабон", Square = 92152, Population = 10.33, Continent = continent1 });
                Countries?.Add( new Country { Title="Англия", Capital="Лондон", Square = 130279, Population = 55.98, Continent = continent1 });
                Countries?.Add( new Country { Title="Хорватия", Capital="Загреб", Square = 56594, Population = 3.899, Continent = continent1 });
                Countries?.Add( new Country { Title="Агентина", Capital="Буэнос-Айрос", Square = 2780000, Population = 45.81, Continent = continent3 });
                Countries?.Add( new Country { Title="Бразилия", Capital="Базилиа", Square = 8510000, Population = 214.3, Continent = continent3 });
                Countries?.Add( new Country { Title="Чили", Capital="Сантьяго", Square = 756626, Population = 19.49, Continent = continent3 });

                SaveChanges();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=DESKTOP-VDC9A9A;Database=CountryDB;Integrated Security=SSPI;TrustServerCertificate=true");
        }
    }
}
