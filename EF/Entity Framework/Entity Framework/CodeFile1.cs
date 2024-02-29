using Entity_Framework;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace CodeFirst.LazyLoading
{
    class MainClass
    {

        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Выборка");
                Console.WriteLine("2. CRUD-операции");
                Console.WriteLine("0. Выход");
                int res = int.Parse(Console.ReadLine()!);
                switch (res)
                {
                    case 1:
                        SelectOperations();
                        break;
                    case 2:
                        CrudOperations();
                        break;
                    case 0:
                        return;
                }
            }
        }
        static void SelectOperations()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Вся инфомация о странах");
                    Console.WriteLine("2. Все страны");
                    Console.WriteLine("3. Все столицы");
                    Console.WriteLine("4. Страны находящиеся в Европе");
                    Console.WriteLine("5. Название стран с площадью больше введенного числа...");
                    Console.WriteLine("6. Страны в названии которых есть 'е' или 'a' ");
                    Console.WriteLine("7. Страны котоые начинаются с А");
                    Console.WriteLine("8. Страны в диапазоне по площади");
                    Console.WriteLine("9. Страны с насилением выше...");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            AllAboutCountry();
                            break;
                        case 2:
                            CountryTitle();
                            break;
                        case 3:
                            CountryCapital();
                            break;
                        case 4:
                            CountriesInEurope();
                            break;
                        case 5:
                            CountiesWithSquareOverGiven();
                            break;
                        case 6:
                            CountriesWith_E_or_A();
                            break;
                        case 7:
                            CountriesStartsWith_A();
                            break;
                        case 8:
                            CountriesInrange();
                            break;
                        case 9:
                            CountriesWithPopulationOverGiven();
                            break;
                        case 0:
                            return;
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void CrudOperations()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Добавить страну");
                    Console.WriteLine("2. Добавить регион");
                    Console.WriteLine("3. Изменить страну ");
                    Console.WriteLine("4. Изменить регион");
                    Console.WriteLine("5. Удалить страну");
                    Console.WriteLine("6. Удалить регион");
                    Console.WriteLine("0. Выход");
                    int res = int.Parse(Console.ReadLine()!);
                    switch (res)
                    {
                        case 1:
                            AddCountry();
                            break;
                        case 2:
                            AddContinent();
                            break;
                        case 3:
                            EditCountry();
                            break;
                        case 4:
                            EditContinent();
                            break;
                        case 5:
                            RemoveCountry();
                            break;
                        case 6:
                            RemoveContinent();
                            break;
                        case 0:
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void AllAboutCountry()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Select(x => x).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Title + " " + country.Capital + " " + country.Square + " км2 " + country.Continent.Title);

                }
            }
            Console.ReadKey();
        }
        static void CountryTitle()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Select(x => x.Title).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country);
                }
            }
            Console.ReadKey();
        }
        static void CountryCapital()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Select(x => x.Capital).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country);
                }
            }
            Console.ReadKey();
        }
        static void CountriesInEurope()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Where(x => x.Continent.Title == "Европа").Select(c => c).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Title + " " + country.Continent.Title);
                }
            }
            Console.ReadKey();
        }
        static void CountiesWithSquareOverGiven()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введите площать в м^2");
                int sq = int.Parse(Console.ReadLine()!);
                using (var db = new CountryContext())
                {
                    var query = db.Countries.Where(c => c.Square > sq);
                    foreach (var country in query)
                    {
                        Console.WriteLine(country.Title + " " + country.Square + " км^2");
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
        static void CountriesWith_E_or_A()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Where(x => x.Continent.Title.Contains("e") || x.Continent.Title.Contains("а")).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Title);
                }
            }
            Console.ReadKey();
        }
        static void CountriesStartsWith_A()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Where(x => x.Title.StartsWith("А") || x.Title.StartsWith("а")).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Title);
                }
            }
            Console.ReadKey();
        }
        static void CountriesInrange()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введите площадь от");
                int sq = int.Parse(Console.ReadLine()!);
                Console.WriteLine("До");
                int sq2 = int.Parse(Console.ReadLine()!);
                using (var db = new CountryContext())
                {
                    var query = db.Countries.Where(c => c.Square > sq && c.Square < sq2);
                    foreach (var country in query)
                    {
                        Console.WriteLine(country.Title + " " + country.Square + " км^2");
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
        static void CountriesWithPopulationOverGiven()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Введите население страны в миллионах");
                int sq = int.Parse(Console.ReadLine()!);
                using (var db = new CountryContext())
                {
                    var query = db.Countries.Where(c => c.Population > sq);
                    foreach (var country in query)
                    {
                        Console.WriteLine(country.Title + " " + country.Population);
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
        static int AllContinets()
        {
            using (var db = new CountryContext())
            {
                Console.Clear();
                Console.WriteLine("Выберите номер континента");
                var query = db.Continents.ToList();
                int max=0;
                foreach(var q in query)
                {
                    Console.WriteLine(q.Id + " " + q.Title);
                    if(q.Id > max)
                        max = q.Id;
                }
                return max;

            }
        }
        static Country NewCountry()
        {
            string title, capital;
            double population;
            long square;
            int maxid, number;
            do
            {
                Console.WriteLine("Введите название страны");
                title = Console.ReadLine()!;
            }
            while (title.Trim().IsNullOrEmpty());
            do
            {
                Console.WriteLine("Введите столицу страны");
                capital = Console.ReadLine()!;
            }
            while (capital.Trim().IsNullOrEmpty());
            do
            {
                Console.WriteLine("Введите население страны");
                population = double.Parse(Console.ReadLine()!);
            } while (population < 0);
            do
            {
                Console.WriteLine("Введите площадь страны в км^2");
                square = long.Parse(Console.ReadLine()!);
            } while (square < 0);

            do
            {
                maxid = AllContinets();
                number = int.Parse(Console.ReadLine()!);
            } while (number < 0 || number > maxid);

            using(var db = new CountryContext())
            {
                Continent cont = (db.Continents.FirstOrDefault(x => x.Id == number));
                Country newcountry = new Country { Title = title, Capital = capital, Population = population, Square = square, Continent = cont };
                return newcountry;
            }
        }
        static void AddCountry()
        {
            using (var db = new CountryContext())
            {
                Console.Clear();
                string title, capital;
                double population;
                long square;
                int maxid, number;
                do
                {
                    Console.WriteLine("Введите название страны");
                    title = Console.ReadLine()!;
                }
                while (title.Trim().IsNullOrEmpty());
                do
                {
                    Console.WriteLine("Введите столицу страны");
                    capital = Console.ReadLine()!;
                }
                while (capital.Trim().IsNullOrEmpty());
                do
                {
                    Console.WriteLine("Введите население страны");
                    population = double.Parse(Console.ReadLine()!);
                } while (population < 0);
                do
                {
                    Console.WriteLine("Введите площадь страны в км^2");
                    square = long.Parse(Console.ReadLine()!);
                } while (square < 0);

                do
                {
                    maxid = AllContinets();
                    number = int.Parse(Console.ReadLine()!);
                } while (number < 0 || number > maxid);
                Country newcountry;
                
                Continent? cont = (db.Continents.FirstOrDefault(x => x.Id == number));
                newcountry = new Country { Title = title, Capital = capital, Population = population, Square = square, Continent = cont };
                
                db.Countries.Add(newcountry);
                db.SaveChanges();
                Console.WriteLine("Вы добавили страну");
                Console.ReadKey();
            }
        }
        static void AddContinent()
        {
            string newcontinent;
            do
            {
                Console.Clear();
                Console.WriteLine("Название нового континента:");
                newcontinent = Console.ReadLine()!;
            }
            while (newcontinent.Trim().IsNullOrEmpty());
            using (var db = new CountryContext())
            {
                var continent = new Continent { Title = newcontinent };
                db.Continents.Add(continent);
                db.SaveChanges();
                Console.WriteLine("Континент добавлен");
            }
            Console.ReadKey();
        }
        static void AllCountries()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Select(x => new { x.Id, x.Title }).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Id +" "+ country.Title);
                }
            }
        }
        static void EditCountry()
        {
            using (var db = new CountryContext())
            {
                Country? targetcountry = null;

                do
                {
                    Console.Clear();
                    AllCountries();
                    Console.WriteLine("Выберите номер страны");
                    int res;
                    if (int.TryParse(Console.ReadLine(), out res))
                    {
                        targetcountry = db.Countries.FirstOrDefault(c => c.Id == res);
                        if (targetcountry != null)
                        {
                            break;
                        }
                    }

                } while (true);

                Console.Clear();
                Console.WriteLine($"{targetcountry.Title} {targetcountry.Capital} {targetcountry.Square} км^2 {targetcountry.Population}млн {targetcountry.Continent}");

                Country newcountry = NewCountry();
                targetcountry.Title = newcountry.Title;
                targetcountry.Capital = newcountry.Capital;
                targetcountry.Square = newcountry.Square;
                targetcountry.Population = newcountry.Population;
                targetcountry.Continent = newcountry.Continent;
                db.SaveChanges();
                Console.WriteLine("Страна изменена");
                Console.ReadKey();
            }
        }
        static void EditContinent()
        {
            using (var db = new CountryContext())
            {
                int maxid, number;
                Continent? targetcontinent;
                do
                {
                    Console.Clear();
                    maxid = AllContinets();
                    if (int.TryParse(Console.ReadLine(), out number))
                    {
                        targetcontinent = db.Continents.FirstOrDefault(c => c.Id == number);
                        if (targetcontinent != null)
                        {
                            break;
                        }
                    }
                } while (true);

                Console.Clear();
                string newtitle;
                Console.WriteLine($"{targetcontinent.Title}");
                Console.WriteLine("Введите новое название");
                do
                {
                    newtitle = Console.ReadLine()!;
                } while (newtitle.Trim().IsNullOrEmpty());

                targetcontinent.Title = newtitle;
                db.SaveChanges();
                Console.WriteLine("Вы изменили континент");
                Console.ReadKey();
            }
        }
        static void RemoveCountry()
        {
            Console.Clear();
            int res;
            Country? country;
            using(var db = new CountryContext())
            {
                Console.WriteLine("Введите номер страны которую хотите удалить");
                do
                {
                    AllCountries();
                    if (int.TryParse(Console.ReadLine(), out res))
                    {
                        country = db.Countries.FirstOrDefault(c => c.Id == res);
                        if (country != null) break; 
                    }
                } while (true);
                db.Countries.RemoveRange(country);
                db.SaveChanges() ;
                Console.WriteLine("Страна удалена");
                Console.ReadKey();
            }
        }
        static void RemoveContinent()
        {
            Console.Clear();
            int res;
            Continent? continent;
            using (var db = new CountryContext())
            {
                Console.WriteLine("Введите номер континента который хотите удалить");
                do
                {
                    AllContinets();
                    if (int.TryParse(Console.ReadLine(), out res))
                    {
                        continent = db.Continents.FirstOrDefault(c => c.Id == res);
                        if (continent != null) break;
                    }
                } while (true);
                db.Continents.RemoveRange(continent);
                db.SaveChanges();
                Console.WriteLine("Континент удален");
                Console.ReadKey();
            }
        }
    }
}
