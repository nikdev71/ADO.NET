using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CodeFirstManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                var config = builder.Build();
                string connectionString = config.GetConnectionString("DefaultConnection");

                var optionsBuilder = new DbContextOptionsBuilder<LanguageContext>();
                var options = optionsBuilder.UseSqlServer(connectionString).Options;

                using (LanguageContext db = new LanguageContext(options))
                {
                    List<Continent> list = db.Continents.Include(l => l.Languages).ToList();
                    foreach (var l in list)
                    {
                        Console.WriteLine(l.Name);
                        foreach (var cont in l.Languages)
                        {
                            Console.WriteLine("\t" + cont.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
            
    }
}
