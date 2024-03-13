using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstManyToMany
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (LanguageContext db = new LanguageContext())
                {
                    List<Language> list = db.Languages.Include(l => l.Continents).ToList();
                    if (list == null || list.Count() == 0)
                    {
                        Language lang1 = new Language { Name = "Английский" };
                        Language lang2 = new Language { Name = "Испанский" };
                        Language lang3 = new Language { Name = "Французский" };
                        Language lang4 = new Language { Name = "Португальский" };

                        db.Languages.Add(lang1);
                        db.Languages.Add(lang2);
                        db.Languages.Add(lang3);
                        db.Languages.Add(lang4);

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

                        db.Continents.Add(c1);
                        db.Continents.Add(c2);
                        db.Continents.Add(c3);
                        db.SaveChanges();
                    }
                    list = db.Languages.Include(l => l.Continents).ToList();
                    foreach (var l in list)
                    {
                        Console.WriteLine(l.Name);
                        foreach (var cont in l.Continents)
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
