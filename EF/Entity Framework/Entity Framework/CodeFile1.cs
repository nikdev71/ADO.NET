using Entity_Framework;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirst.LazyLoading
{
    class MainClass
    {
        
        static void Main()
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
        static void AllAboutCountry()
        {
            Console.Clear();
            using (var db = new CountryContext())
            {
                var query = db.Countries.Select(x=>x).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Title+" "+ country.Capital + " "+ country.Square + " км2 " + country.Continent.Title);

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
                var query = db.Countries.Where(x => x.Continent.Title == "Европа").Select(c=>c).ToList();
                foreach (var country in query)
                {
                    Console.WriteLine(country.Title +" "+ country.Continent.Title);
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
            catch(Exception ex)
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
                        Console.WriteLine(country.Title + " " + country.Square+" км^2");
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
    }
}
