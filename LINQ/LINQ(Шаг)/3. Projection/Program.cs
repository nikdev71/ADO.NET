using System;
using System.Collections.Generic;
using System.Linq;

namespace Projection
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Phone
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }
    class Pair
    {
        public int Value;
        public bool Even;
        public Pair(int a, bool b)
        {
            Value = a;
            Even = b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Проекция позволяет спроектировать из текущего типа выборки какой-то другой тип
            // Для проекции используется оператор select.
            List<User> users = new List<User>();
            users.Add(new User { Name = "Том", Age = 23 });
            users.Add(new User { Name = "Боб", Age = 27 });
            users.Add(new User { Name = "Джон", Age = 29 });

            // Например, необходим не весь объект, а только его свойство Name
            var names = from u in users select u.Name;

            foreach (var n in names)
                Console.WriteLine(n);
            Console.WriteLine('\n');

            // Аналогично можно создать объекты другого типа, в том числе анонимного

            int[] Mas = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Выполняем запрос к массиву Mas и выполняем преобразование в объект, 
            //содержащий два поля Value - число и
            //Even значение boolean, показывающее является ли число четным
            var Mas2 = from i in Mas
                       select new Pair(i, i % 2 == 0);
            foreach (var val in Mas2)
                Console.WriteLine("{0} - {1}", val.Value, val.Even);
            Console.WriteLine('\n');

            // Создание анонимного типа
            var anon = new { Age = 18, Name = "Ivan" };
            Console.WriteLine("age = " + anon.Age + "\tname = " + anon.Name);
            Console.WriteLine(anon.GetType());

            //Выполняем запрос к массиву Mas и выполняем преобразование в объект, 
            //содержащий два поля Value - число и
            //Even - значение boolean, показывающее является ли число четным
            var Mas3 = from i in Mas
                       select new { Value = i, Even = (i % 2 == 0) };
            foreach (var val in Mas3)
                Console.WriteLine("{0} - {1}", val.Value, val.Even);
            Console.WriteLine('\n');

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var digitOddEvens =
                from n in numbers
                select new { Digit = strings[n], Even = (n % 2 == 0) };

            foreach (var digit in digitOddEvens)
            {
                Console.WriteLine("The digit {0} is {1}.", digit.Digit, digit.Even ? "even" : "odd");
            }
            Console.WriteLine('\n');

            var items = from u in users
                        select new
                        {
                            FirstName = u.Name,
                            DateOfBirth = DateTime.Now.Year - u.Age
                        };

            foreach (var n in items)
                Console.WriteLine("{0} - {1}", n.FirstName, n.DateOfBirth);
            Console.WriteLine('\n');

            // В качестве альтернативы можно использовать метод расширения Select()
            // выборка имен
            names = users.Select(u => u.Name);
            foreach (string n in names)
                Console.WriteLine(n);
            Console.WriteLine('\n');

            // выборка объектов анонимного типа
            items = users.Select(u => new
            {
                FirstName = u.Name,
                DateOfBirth = DateTime.Now.Year - u.Age
            });
            foreach (var n in items)
                Console.WriteLine("{0} - {1}", n.FirstName, n.DateOfBirth);
            Console.WriteLine('\n');

            var numsInPlace = numbers.Select((num, index) => new { Num = num, InPlace = (num == index) });

            Console.WriteLine("Number: In-place?");
            foreach (var n in numsInPlace)
            {
                Console.WriteLine("{0}: {1}", n.Num, n.InPlace);
            }

            // Переменые в запросах и оператор let
            // Иногда возникает необходимость произвести в запросах LINQ
            // какие-то дополнительные промежуточные вычисления. 
            // Для этих целей можно определить в запросах свои переменные с помощью оператора let.
            var people = from u in users
                         let name = "Mr. " + u.Name
                         select new
                         {
                             Name = name,
                             Age = u.Age
                         };
            foreach (var n in people)
                Console.WriteLine("{0} - {1}", n.Name, n.Age);
            Console.WriteLine('\n');

            // В LINQ можно выбирать объекты не только из одного, 
            // но и из большего количества источников
            List<Phone> phones = new List<Phone>()
            {
                new Phone {Name="Lumia 630", Company="Microsoft" },
                new Phone {Name="iPhone 6", Company="Apple"},
                new Phone {Name="HTC One M9", Company="HTC"},
            };
            var people2 = from user in users
                          from phone in phones
                          select new { Name = user.Name, Phone = phone.Name };
            foreach (var p in people2)
                Console.WriteLine("{0} - {1}", p.Name, p.Phone);
            Console.WriteLine('\n');
            // При выборке из двух источников каждый элемент из первого источника 
            // будет сопоставляться с каждым элементом из второго источника

            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs =
                from a in numbersA
                from b in numbersB
                where a < b
                select new { a, b };

            Console.WriteLine("Pairs where a < b:");
            foreach (var pair in pairs)
            {
                Console.WriteLine("{0} is less than {1}", pair.a, pair.b);
            }
        }
    }
}
