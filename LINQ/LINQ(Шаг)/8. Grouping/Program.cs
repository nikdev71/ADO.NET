using System;
using System.Collections.Generic;
using System.Linq;

namespace Grouping
{
    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Для группировки данных по определенным параметрам применяется оператор group by или метод GroupBy().

            List<Phone> phones = new List<Phone>
            {
                new Phone {Name="Lumia 430", Company="Microsoft" },
                new Phone {Name="Mi 5", Company="Xiaomi" },
                new Phone {Name="LG G3", Company="LG" },
                new Phone {Name="iPhone 5", Company="Apple" },
                new Phone {Name="Lumia 930", Company="Microsoft" },
                new Phone {Name="iPhone 6", Company="Apple" },
                new Phone {Name="Lumia 630", Company="Microsoft" },
                new Phone {Name="LG G4", Company="LG" }
            };

            // Результатом оператора group является выборка, которая состоит из групп. 
            // Каждая группа представляет объект IGrouping<string, Phone>: параметр string указывает на тип ключа, 
            // а параметр Phone - на тип сгруппированных объектов
            // Каждая группа имеет ключ, который мы можем получить через свойство Key: g.Key
            var phoneGroups = from phone in phones
                              group phone by phone.Company;
            // Если в выражении LINQ последним оператором, 
            // выполняющим операции над выборкой, является group, то оператор select не применяется

            // Все элементы группы можно получить с помощью дополнительной итерации. 
            // Элементы группы имеют тот же тип, что и тип объектов, которые передавались оператору group, 
            // то есть в данном случае объекты типа Phone
            foreach (IGrouping<string, Phone> g in phoneGroups)
            {
                Console.WriteLine(g.Key);
                foreach (var t in g)
                    Console.WriteLine(t.Name);
                Console.WriteLine();
            }
            Console.WriteLine('\n');

            // Аналогичный запрос можно построить с помощью метода расширения GroupBy
            phoneGroups = phones.GroupBy(p => p.Company);
            foreach (IGrouping<string, Phone> g in phoneGroups)
            {
                Console.WriteLine(g.Key);
                foreach (var t in g)
                    Console.WriteLine(t.Name);
                Console.WriteLine();
            }
            Console.WriteLine('\n');

            // Выражение group phone by phone.Company into g определяет переменную g, которая будет содержать группу. 
            // С помощью этой переменной мы можем затем создать новый объект анонимного типа: 
            // select new { Name = g.Key, Count = g.Count() } 
            // Теперь результат запроса LINQ будет представлять набор объектов таких анонимных типов, 
            // у которых два свойства Name и Count
            var phoneGroups2 = from phone in phones
                               group phone by phone.Company into g
                               select new { Name = g.Key, Count = g.Count() };
            foreach (var group in phoneGroups2)
                Console.WriteLine("{0} : {1}", group.Name, group.Count);
            Console.WriteLine('\n');

            // Аналогичная операция с помощью метода GroupBy()
            phoneGroups2 = phones.GroupBy(p => p.Company)
                        .Select(g => new { Name = g.Key, Count = g.Count() });
            foreach (var group in phoneGroups2)
                Console.WriteLine("{0} : {1}", group.Name, group.Count);
            Console.WriteLine('\n');

            var phoneGroups3 = from phone in phones
                               group phone by phone.Company into g
                               select new
                               {
                                   Name = g.Key,
                                   Count = g.Count(),
                                   Phones = from p in g select p
                               };
            foreach (var group in phoneGroups3)
            {
                Console.WriteLine("{0} : {1}", group.Name, group.Count);
                foreach (var phone in group.Phones)
                    Console.WriteLine(phone.Name);
                Console.WriteLine();
            }
            Console.WriteLine('\n');

            // Аналогичный запрос с помощью метода GroupBy
            var phoneGroups4 = phones.GroupBy(p => p.Company)
                        .Select(g => new
                        {
                            Name = g.Key,
                            Count = g.Count(),
                            Phones = g.Select(p => p)
                        });
            foreach (var group in phoneGroups4)
            {
                Console.WriteLine("{0} : {1}", group.Name, group.Count);
                foreach (var phone in group.Phones)
                    Console.WriteLine(phone.Name);
                Console.WriteLine();
            }
            Console.WriteLine('\n');


            string[] websites = { "microsoft.com", "asp.net", "php.net", "google.com", "wikipedia.org", "itstep.org", "odessa.tv", "sport.ua" };
            // Сформировать запрос на получение списка веб-сайтов, группируемых по имени домена самого верхнего уровня. 
            var webAddrs = from addr in websites
                           where addr.LastIndexOf(".") != -1
                           group addr by addr.Substring(addr.LastIndexOf("."));

            foreach (var sites in webAddrs)
            {
                Console.WriteLine("Веб-сайты, сгруппированные по имени домена " + sites.Key);
                foreach (var site in sites)
                    Console.WriteLine(" " + site);
                Console.WriteLine();
            }
            Console.WriteLine('\n');

            webAddrs = from addr in websites
                       where addr.LastIndexOf(".") != -1
                       group addr by addr.Substring(addr.LastIndexOf(".")) into result
                       where result.Key.Substring(result.Key.LastIndexOf(".")) == ".org"
                       select result;

            foreach (var sites in webAddrs)
            {
                Console.WriteLine("Веб-сайты, сгруппированные по имени домена " + sites.Key);
                foreach (var site in sites)
                    Console.WriteLine(" " + site);
                Console.WriteLine();
            }
            Console.WriteLine('\n');

            string[] words = new string[] { "blueberry", "cherry", "pear", "banana", "currant", "pineapple" };

            var wordGroups =
                from w in words
                group w by w[0] into result
                select new { FirstLetter = result.Key, Words = result };

            foreach (var g in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", g.FirstLetter);
                foreach (var w in g.Words)
                {
                    Console.WriteLine(w);
                }
            }
            Console.WriteLine('\n');
        }
    }
}
