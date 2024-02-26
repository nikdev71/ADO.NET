using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorting
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Для сортировки набора данных по возрастанию используется оператор orderby
            int[] numbers = { 3, 12, 4, 10, 34, 20, 55, -66, 77, 88, 4 };
            var orderedNumbers = from i in numbers
                                 orderby i ascending
                                 select i;
            foreach (var i in orderedNumbers)
                Console.Write("{0,4}", i);
            Console.WriteLine('\n');

            double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

            var sortedDoubles =
                from d in doubles
                orderby d descending
                select d;

            Console.WriteLine("The doubles from highest to lowest:");
            foreach (var i in sortedDoubles)
                Console.Write("{0,6}", i);
            Console.WriteLine('\n');

            string[] words = { "cherry", "apple", "blueberry" };

            var sortedWords =
                from w in words
                orderby w.Length
                select w;

            Console.WriteLine("The sorted list of words (by length):");
            foreach (var w in sortedWords)
            {
                Console.WriteLine(w);
            }
            Console.WriteLine('\n');

            List<User> users = new List<User>()
            {
                new User { Name = "Том", Age = 33 },
                new User { Name = "Боб", Age = 30 },
                new User { Name = "Джон", Age = 21 },
                new User { Name = "Элис", Age = 24 }
            };

            var sortedUsers = from u in users
                              orderby u.Name
                              select u;

            foreach (var u in sortedUsers)
                Console.WriteLine(u.Name);
            Console.WriteLine('\n');

            sortedUsers = from u in users
                          orderby u.Name descending
                          select u;

            foreach (var u in sortedUsers)
                Console.WriteLine(u.Name);
            Console.WriteLine('\n');

            // Вместо оператора orderby можно использовать метод расширения OrderBy
            IEnumerable<int> sortedNumbers = numbers.OrderBy(i => i);
            foreach (var i in sortedNumbers)
                Console.Write("{0,4}", i);
            Console.WriteLine('\n');

            sortedUsers = users.OrderBy(u => u.Name, new SortByName());
            // sortedUsers = users.OrderByDescending(u=>u.Name);
            foreach (var u in sortedUsers)
                Console.WriteLine(u.Name);
            Console.WriteLine('\n');

            // Множественные критерии сортировки
            List<User> users2 = new List<User>()
            {
                new User { Name = "Том", Age = 33 },
                new User { Name = "Боб", Age = 30 },
                new User { Name = "Джон", Age = 21 },
                new User { Name = "Элис", Age = 24 },
                new User { Name = "Том", Age = 23 },
                new User { Name = "Боб", Age = 36 },
                new User { Name = "Джон", Age = 25 },
                new User { Name = "Элис", Age = 20 }
            };
            var result = from user in users2
                         orderby user.Name descending, user.Age
                         select user;
            foreach (var u in result)
                Console.WriteLine("{0} - {1}", u.Name, u.Age);
            Console.WriteLine('\n');

            // С помощью методов расширения то же самое можно сделать через метод 
            // ThenBy()(для сортировки по возрастанию) 
            // и ThenByDescending() (для сортировки по убыванию)
            result = users2.OrderByDescending(u => u.Name).ThenBy(u => u.Age);
            foreach (var u in result)
                Console.WriteLine("{0} - {1}", u.Name, u.Age);
        }
    }

    public class SortByName : IComparer<string>
    {
        public int Compare(string obj1, string obj2)
        {
            return obj2.CompareTo(obj1);
        }
    }
}
