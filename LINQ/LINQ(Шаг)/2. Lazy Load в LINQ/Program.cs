using System;
using System.Collections.Generic;
using System.Linq;

namespace Lazy_Load_в_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> languages = new List<string>() { "C++", "C#", "Java", "PHP" };
            IEnumerable<string> result = from language in languages where language.StartsWith("C") select language;
            foreach (var str in result)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
            // Принцип Lazy Load заключается в том, что загрузка данных в память не происходит до момента, 
            // пока они действительно не понадобятся.
            // Lazy Load позволяет нам многократно использовать один и тот же запрос к одному источнику, 
            // с гарантией получения самых актуальных данных.

            languages[0] = "JavaScript";

            foreach (var str in result)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
            // LINQ может быть выполнен немедленно. Для этого необходимо заключить весь запрос в скобки, и 
            // вызвать один из методов преобразования ToList (), ToArray(), ToDictionary().

            languages = new List<string>() { "C++", "C#", "Java", "PHP" };
            result = (from language in languages where language.StartsWith("C") select language).ToList();
            foreach (var str in result)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
            languages[0] = "JavaScript";
            foreach (var str in result)
            {
                Console.WriteLine(str);
            }
        }
    }
}
