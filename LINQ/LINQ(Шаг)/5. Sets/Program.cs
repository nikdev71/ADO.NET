using System;
using System.Linq;

namespace Sets
{
    class Program
    {
        static void Main(string[] args)
        {
            // LINQ имеет несколько методов для работы с множествами: разность, объединение и пересечение

            // С помощью метода Except можно получить разность двух множеств
            string[] soft = { "Microsoft", "Google", "Apple" };
            string[] hard = { "Apple", "IBM", "Samsung" };

            // разность множеств
            var result = soft.Except(hard);

            foreach (var s in result)
                Console.WriteLine(s);
            Console.WriteLine('\n');

            // Для получения пересечения множеств, то есть общих для обоих наборов элементов, применяется метод Intersect

            // пересечение множеств
            result = soft.Intersect(hard);

            foreach (var s in result)
                Console.WriteLine(s);
            Console.WriteLine('\n');

            // Для объединения двух множеств используется метод Union. 
            // Его результатом является новый набор, в котором имеются элементы, как из одного, так и из второго множества. 
            // Повторяющиеся элементы добавляются в результат только один раз
            // объединение множеств
            result = soft.Union(hard);

            foreach (var s in result)
                Console.WriteLine(s);
            Console.WriteLine('\n');

            // Если необходимо простое объединение двух наборов, то можно использовать метод Concat
            result = soft.Concat(hard);
            foreach (var s in result)
                Console.WriteLine(s);
            Console.WriteLine('\n');

            // Для удаления дубликатов в наборе используется метод Distinct
            result = result.Distinct();
            foreach (var s in result)
                Console.WriteLine(s);
            Console.WriteLine('\n');
        }
    }
}
