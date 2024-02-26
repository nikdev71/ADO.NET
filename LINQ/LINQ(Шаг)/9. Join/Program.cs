using System;
using System.Collections.Generic;
using System.Linq;

namespace Join
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int CityId { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<City> cities = new List<City>()
            {
                new City() { Id = 1, CityName = "Odesa" },
                new City() { Id = 2, CityName = "Kyiv" },
                new City() { Id = 3, CityName = "Lviv" }
            };
            List<Student> students = new List<Student>()
            {
                new Student() { Id = 1, FirstName = "Oleg", LastName = "Petrov", Age = 18, CityId = 3 },
                new Student() { Id = 2, FirstName = "Marina", LastName = "Ivanova", Age = 19, CityId = 2 },
                new Student() { Id = 3, FirstName = "Taras", LastName = "Golovko", Age = 19, CityId = 2 },
                new Student() { Id = 4, FirstName = "Aleksei", LastName = "Smirnov", Age = 18, CityId = 1 },
                new Student() { Id = 5, FirstName = "Oleg", LastName = "Belov", Age = 21, CityId = 1 }
            };

            // Вывести имя, фамилию и город проживания каждого студента.
            var result = from student in students
                     from city in cities
                     where student.CityId == city.Id
                     select new
                     {
                         student.FirstName,
                         student.LastName,
                         city.CityName
                     };
            foreach (var student in result)
            {
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.CityName);
            }
            Console.WriteLine();

            result = from student in students
                         join city in cities on student.CityId equals city.Id
                         select new
                         {
                             student.FirstName,
                             student.LastName,
                             city.CityName
                         };

            foreach (var student in result)
            {
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.CityName);
            }
            Console.WriteLine();

            result = students.Join(cities, student => student.CityId, city => city.Id, (student, city) => new
            {
                student.FirstName,
                student.LastName,
                city.CityName
            });
            foreach (var student in result)
            {
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.CityName);
            }
            Console.WriteLine();

        }
    }
}
