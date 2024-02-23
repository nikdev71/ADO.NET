using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace linq
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    };

    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }
    }
    class Department
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    class Good
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region TASK 1
            List<Person> person = new List<Person>()
            {
                new Person(){ Name = "Andrey", Age = 24, City = "Kyiv"},
                new Person(){ Name = "Liza", Age = 18, City = "Odesa" },
                new Person(){ Name = "Oleg", Age = 15, City = "London" },
                new Person(){ Name = "Sergey", Age = 55, City = "Kyiv" },
                new Person(){ Name = "Sergey", Age = 32, City = "Lviv" }
            };
            Console.WriteLine("Request LINQ\n");
            var olderThan25Query =
                from p in person
                where p.Age > 25
                select p;
            Console.WriteLine("People older than 25:");
            foreach (var p in olderThan25Query)
            {
                Console.WriteLine($"{p.Name}, {p.Age}");
            }

            var notInLondonQuery =
                from p in person
                where p.City != "London"
                select p;

            Console.WriteLine("\nPeople not living in London:");
            foreach (var p in notInLondonQuery)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.City}");
            }

            var namesInKyivQuery =
                from p in person
                where p.City == "Kyiv"
                select p.Name;

            Console.WriteLine("\nNames of people living in Kyiv:");
            foreach (var name in namesInKyivQuery)
            {
                Console.WriteLine(name);
            }

            var sergeyOver35Query =
                from p in person
                where p.Name == "Sergey" && p.Age > 35
                select p;

            Console.WriteLine("\nPeople named Sergey older than 35:");
            foreach (var p in sergeyOver35Query)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.City}");
            }

            var peopleInOdesaQuery =
                from p in person
                where p.City == "Odesa"
                select p;

            Console.WriteLine("\nPeople living in Odesa:");
            foreach (var p in peopleInOdesaQuery)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.City}\n");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Methods LINQ\n");
            
            var people = person.Where(p => p.Age >25);

            Console.WriteLine("People older 25 years:");
            foreach (var p in people)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.City}\n");
            }

            people = person.Where(p => p.City != "Лондон");

            Console.WriteLine("People lives not in London:");
            foreach (var p in people)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.City}\n");
            }

            people = person.Where(p => p.City == "Kiyv");

            Console.WriteLine("People lives in Kiyv:");
            foreach (var p in people)
            {
                Console.WriteLine($"{p.Name}\n");
            }

            people = person.Where(p => p.Age > 35 && p.Name=="Sergey");

            Console.WriteLine("People age > 35 and name = sergey:");
            foreach (var p in people)
            {
                Console.WriteLine($"{p.Name}\n");
            }

            people = person.Where(p => p.City == "Odesa");

            Console.WriteLine("People lives in Odessa:");
            foreach (var p in people)
            {
                Console.WriteLine($"{p.Name}, {p.Age}, {p.City}\n");
            }

            Console.ForegroundColor = ConsoleColor.White;
            #endregion

            #region Task 2
            List<Department> departments = new List<Department>()
            {
                new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
                new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                new Department(){ Id = 3, Country = "France", City = "Paris" },
                new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
            };


            List<Employee> employees = new List<Employee>()
            {
            new Employee()
            { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
            new Employee()
            { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
            new Employee()
            { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
            new Employee()
            { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
            new Employee()
            { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
            new Employee()
            { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
            new Employee()
            { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age = 27, DepId = 4 }
            };
            Console.WriteLine("Request LINQ\n");

            var ukraineEmployeesNotInOdessa =
            from emp in employees
            join dep in departments on emp.DepId equals dep.Id
            where dep.Country == "Ukraine" && dep.City != "Odesa"
            select new { emp.FirstName, emp.LastName };

            Console.WriteLine("Employees in Ukraine but not in Odessa:");
            foreach (var emp in ukraineEmployeesNotInOdessa)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName}");
            }

            var distinctCountries =
                   (from dep in departments
                    select dep.Country).Distinct();

            Console.WriteLine("\nDistinct countries:");
            foreach (var country in distinctCountries)
            {
                Console.WriteLine(country);
            }

            var firstThreeOver25 =
                        (from emp in employees
                         where emp.Age > 25
                         select emp).Take(3);

            Console.WriteLine("\nFirst three employees over 25:");
            foreach (var emp in firstThreeOver25)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName}, {emp.Age}");
            }

            var studentsFromKyivOver23 =
                from emp in employees
                join dep in departments on emp.DepId equals dep.Id
                where dep.Country == "Ukraine" && dep.City == "Kyiv" && emp.Age > 23
                select new { emp.FirstName, emp.LastName, emp.Age , dep.City};

            Console.WriteLine("\nStudents from Kyiv over 23 years old:");
            foreach (var student in studentsFromKyivOver23)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}, {student.Age}, {student.City}");
            }


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMethods LINQ\n");

             ukraineEmployeesNotInOdessa = employees
                 .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                 .Where(x => x.dep.Country == "Ukraine" && x.dep.City != "Odesa")
                 .Select(x => new { x.emp.FirstName, x.emp.LastName });

            Console.WriteLine("Employees in Ukraine but not in Odessa:");
            foreach (var emp in ukraineEmployeesNotInOdessa)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName}");
            }

             distinctCountries = departments.Select(dep => dep.Country).Distinct();

            Console.WriteLine("\nDistinct countries:");
            foreach (var country in distinctCountries)
            {
                Console.WriteLine(country);
            }

             firstThreeOver25 = employees.Where(emp => emp.Age > 25).Take(3);

            Console.WriteLine("\nFirst three employees over 25:");
            foreach (var emp in firstThreeOver25)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName}, {emp.Age}");
            }

             studentsFromKyivOver23 = employees
                .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                .Where(x => x.dep.City == "Kyiv" && x.emp.Age > 23).Select(d => new { d.emp.FirstName, d.emp.LastName, d.emp.Age, d.dep.City });

            Console.WriteLine("\nStudents from Kyiv over 23 years old:");
            foreach (var student in studentsFromKyivOver23)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName} {student.Age}");
            }
            Console.ResetColor();
            #endregion

            #region Task 3
            // Принцип Lazy Load заключается в том, что загрузка данных в память не происходит до момента, 
            // пока они действительно не понадобятся.
            // Lazy Load позволяет нам многократно использовать один и тот же запрос к одному источнику, 
            // с гарантией получения самых актуальных данных.
            // LINQ может быть выполнен немедленно. Для этого необходимо заключить весь запрос в скобки, и 
            // вызвать один из методов преобразования ToList (), ToArray(), ToDictionary().
            //ToList прерывает ленивую загрузку(Lazy Load), которая будет вызываться только при итерации по ней, тоесть ToList() нам немедленно возвращает список
            Console.WriteLine("Request LINQ\n");
            
            var studentinUkraine = (from emp in employees
                                   join dep in departments on emp.DepId equals dep.Id
                                   where dep.Country=="Ukraine"
                                   orderby emp.FirstName ascending
                                   select new { emp.FirstName, emp.LastName }).ToList();

            Console.WriteLine("\nStudents from Ukraine sort ascending:");
            foreach (var std in studentinUkraine)
            {
                Console.WriteLine($"{std.FirstName} {std.LastName}");
            }


            var employeeAgeDescending = (from emp in employees
                                        orderby emp.Age descending
                                        select new {emp.Id, emp.FirstName, emp.LastName, emp.Age}).ToList();

            Console.WriteLine("\nStudents sort by age descending");
            foreach (var emp in employeeAgeDescending)
            {
                Console.WriteLine($"{emp.Id} {emp.FirstName} {emp.LastName} {emp.Age}");
            }

            var studentsGroupByAgeCount = (from emp in employees
                                          group emp by emp.Age into e
                                          select new { age = e.Key, count = e.Count() }).ToList();

            Console.WriteLine("\nAge - Quantity");
            foreach (var student in studentsGroupByAgeCount)
            {
                Console.WriteLine($"{student.age} - {student.count}");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nMethods LINQ\n");

            studentinUkraine = employees
                .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                .Where(x => x.dep.Country == "Ukraine").Select(d => new { d.emp.FirstName, d.emp.LastName }).OrderBy(m => m.FirstName).ToList();

            Console.WriteLine("\nStudents from Ukraine sort ascending:");
            foreach (var std in studentinUkraine)
            {
                Console.WriteLine($"{std.FirstName} {std.LastName}");
            }

            employeeAgeDescending = employees.OrderByDescending(emp => emp.Age).Select(m => new { m.Id, m.FirstName, m.LastName, m.Age }).ToList();
                ;
            Console.WriteLine("\nStudents sort by age descending");
            foreach (var emp in employeeAgeDescending)
            {
                Console.WriteLine($"{emp.Id} {emp.FirstName} {emp.LastName} {emp.Age}");
            }

            studentsGroupByAgeCount = employees.GroupBy(e => e.Age).Select(x => new { age = x.Key, count = x.Count()}).ToList();

            Console.WriteLine("\nAge - Quantity");
            foreach (var student in studentsGroupByAgeCount)
            {
                Console.WriteLine($"{student.age} - {student.count}");
            }
            Console.ResetColor();
            #endregion


            #region Task 4
            List<Good> goods1 = new List<Good>()
            {
                new Good()
                { Id = 1, Title = "Nokia 1100", Price = 450.99, Category = "Mobile" },
                new Good()
                { Id = 2, Title = "Iphone 4", Price = 5000, Category = "Mobile" },
                new Good()
                { Id = 3, Title = "Refregirator 5000", Price = 2555, Category = "Kitchen" },
                new Good()
                { Id = 4, Title = "Mixer", Price = 150, Category = "Kitchen" },
                new Good()
                { Id = 5, Title = "Magnitola", Price = 1499, Category = "Car" },
                new Good()
                    { Id = 6, Title = "Samsung Galaxy", Price = 3100, Category = "Mobile" },
                new Good()
                    { Id = 7, Title = "Auto Cleaner", Price = 2300, Category = "Car" },
                new Good()
                    { Id = 8, Title = "Owen", Price = 700, Category = "Kitchen" },
                new Good()
                    { Id = 9, Title = "Siemens Turbo", Price = 3199, Category = "Mobile" },
                new Good()
                    { Id = 10, Title = "Lighter", Price = 150, Category = "Car" }
            };

            Console.WriteLine("\nRequest LINQ\n");

            var prodcutPriceOver1000 = from g in goods1
                                       where g.Price > 1000 && g.Category == "Mobile"
                                       select new {g.Title,g.Price,g.Category };
            Console.WriteLine("\nProduct with price over 1000 and category \"Mobile\"\n");
            foreach(var g in prodcutPriceOver1000)
            {
                Console.WriteLine($"{ g.Title} { g.Price} { g.Category}");
            }


            var prodcutPriceOver10002 = from g in goods1
                                       where g.Price > 1000 && g.Category != "Kitchen"
                                       select new { g.Title, g.Price, g.Category };
            Console.WriteLine("\nProduct with price over 1000 and category not \"Kitchen\"\n");
            foreach (var g in prodcutPriceOver10002)
            {
                Console.WriteLine($"{g.Title} {g.Price} {g.Category}");
            }

            var dist = (from g in goods1
                        select new { g.Category }).Distinct();
            Console.WriteLine($"\nDistinct\n");
            foreach (var g in dist)
            {
                Console.WriteLine($"{g.Category}");
            }

            var nameCategorySortProduct  = from g in goods1
                                           orderby g.Title ascending
                                           select new { g.Title,g.Category };
            Console.WriteLine("\nProduct sort by title\n");
            foreach(var g in nameCategorySortProduct)
            {
                Console.WriteLine($"{g.Title} {g.Category}");
            }


            var count = (from g in goods1
                        where g.Category == "Car" || g.Category == "Mobile"
                        select new { g.Category }).Count();

            Console.WriteLine("\nCount product with categories 'Car' and 'Mobile' = {0}\n", count);

            var categorywithcount = from g in goods1
                                    group g by g.Category into gr
                                    select new { Category = gr.Key, Count = gr.Count() };

            Console.WriteLine("\nCount product with every categories", count);
            foreach (var g in categorywithcount)
            {
                Console.WriteLine($"{g.Category} {g.Count}");
            }
            Console.WriteLine("\nMethods LINQ\n");

            prodcutPriceOver1000 = goods1.Where(g => g.Price >1000 && g.Category =="Mobile").Select(g => new {g.Title, g.Price,g.Category});
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nProduct with price over 1000 and category \"Mobile\"\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var g in prodcutPriceOver1000)
            {
                Console.WriteLine($"{g.Title} {g.Price} {g.Category}");
            }

            prodcutPriceOver10002 = goods1.Where(g => g.Price > 1000 && g.Category != "Kitchen").Select(g => new { g.Title, g.Price, g.Category });
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nProduct with price over 1000 and category not \"Kitchen\"\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var g in prodcutPriceOver1000)
            {
                Console.WriteLine($"{g.Title} {g.Price} {g.Category}");
            }

            var avg = goods1.Average(g=> g.Price);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAverage price = {0}\n", avg);
            Console.ForegroundColor = ConsoleColor.Cyan;


            dist = goods1.Select(x=> new {x.Category}).Distinct();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nDistinct\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach(var g in dist)
            {
                Console.WriteLine($"{g.Category}");
            }

            nameCategorySortProduct = goods1.OrderBy(x => x.Category).Select(b => new { b.Title, b.Category });

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\nProduct sort by title\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var g in nameCategorySortProduct)
            {
                Console.WriteLine($"{g.Title} {g.Category}");
            }

            count = goods1.Where(g => g.Category == "Car" || g.Category == "Mobile").Count();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCount product with categories 'Car' and 'Mobile' = {0}\n", count);
            Console.ForegroundColor = ConsoleColor.Cyan;


            categorywithcount = goods1.GroupBy(g=>g.Category).Select(x => new {Category=x.Key,Count=x.Count()});

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCount product with every categories", count);
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var g in categorywithcount)
            {
                Console.WriteLine($"{g.Category} {g.Count}");
            }

            Console.ResetColor();
            #endregion

        }
    }
}