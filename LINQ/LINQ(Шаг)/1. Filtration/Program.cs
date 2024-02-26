using System;
using System.Collections.Generic;
using System.Linq;


namespace LINQ
{
 
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        public User()
        {
            Languages = new List<string>();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] Mas = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var val in Mas)
                Console.WriteLine(val);

            //Выполняем запрос к массиву Mas и получим его элементы, которые больше 4, но меньше 8
            // Mas2 - неявно типизированная переменная запроса
            // i - переменная диапазона
            // Mas - источник данных
            // Тип переменной диапазона выводится из источника данных
            // i > 4 && i < 8   -  это булево выражение или предикат

            var Mas2 = from i in Mas
                       where i > 4
                       where i < 8
                       select i;
            foreach (var val in Mas2)
                Console.Write("{0}   ", val);
            Console.WriteLine();

            Mas2 = from i in Mas
                   where i > 4 && i < 8
                   select i;
            foreach (var val in Mas2)
                Console.Write("{0}   ", val);
            Console.WriteLine();

            string[] teams = { "Барселона", "Динамо Киев", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Бавария" };

            // Выберем из массива строки, начинающиеся на определенную букву, и отсортируем полученный список
            // 1 способ
            var selectedTeams = new List<string>();
            foreach (var s in teams)
            {
                if (s.ToUpper().StartsWith("Б"))
                    selectedTeams.Add(s);
            }
            selectedTeams.Sort();

            foreach (var s in selectedTeams)
                Console.WriteLine(s);

            // 2 способ через LINQ
            var selectedTeams2 = from t in teams // определяем каждый объект из teams как t
                                 where t.ToUpper().StartsWith("Б") //фильтрация по критерию
                                 orderby t  // упорядочиваем по возрастанию
                                 select t; // выбираем объект

            foreach (var s in selectedTeams2)
                Console.WriteLine(s);

            // 3 способ через методы расширения LINQ
            var selectedTeams3 = teams.Where(t => t.ToUpper().StartsWith("Б")).OrderBy(t => t);

            foreach (var s in selectedTeams3)
                Console.WriteLine(s);

            // Используем стандартный синтаксис linq и метод расширения Count(), 
            // возвращающий количество элементов в выборке
            int number = (from t in teams where t.ToUpper().StartsWith("Б") select t).Count();
            Console.WriteLine(number);
            Console.WriteLine('\n');

            // Фильтрация выборки

            //Для выбора элементов из некоторого набора по условию используется метод расширения Where. 
            // Например, выберем все четные элементы, которые больше 10.
            int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

            IEnumerable<int> evens = numbers.Where(i => i % 2 == 0 && i > 10);

            foreach (var i in evens)
                Console.Write("{0}\t", i);
            Console.WriteLine('\n');

            // Фильтрация с помощью операторов LINQ
            evens = from i in numbers
                    where i % 2 == 0 && i > 10
                    select i;
            foreach (var i in evens)
                Console.Write("{0}\t", i);
            Console.WriteLine('\n');

            // Создадим набор пользователей и выберем из них тех, которым больше 25 лет
            List<User> users = new List<User>
            {
                new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
                new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
                new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
                new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            };

            var selectedUsers = from user in users
                                where user.Age > 25
                                select user;
            foreach (var user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
            Console.WriteLine('\n');

            // Аналогичный запрос с помощью метода расширения Where
            selectedUsers = users.Where(u => u.Age > 25);
            foreach (var user in selectedUsers)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
            Console.WriteLine('\n');

            // Выберем пользователей, владеющих английским, которым меньше 28 лет 
            var selectedUsers2 = from user in users
                                 from lang in user.Languages
                                 where user.Age < 28
                                 where lang == "английский"
                                 select user;
            foreach (var user in selectedUsers2)
                Console.WriteLine("{0} - {1}", user.Name, user.Age);
            Console.WriteLine('\n');

            /*
             Список используемых методов расширения LINQ

                Select: определяет проекцию выбранных значений
                Where: определяет фильтр выборки
                OrderBy: упорядочивает элементы по возрастанию
                OrderByDescending: упорядочивает элементы по убыванию
                ThenBy: задает дополнительные критерии для упорядочивания элементов возрастанию
                ThenByDescending: задает дополнительные критерии для упорядочивания элементов по убыванию
                Join: соединяет две коллекции по определенному признаку
                GroupBy: группирует элементы по ключу
                ToLookup: группирует элементы по ключу, при этом все элементы добавляются в словарь
                GroupJoin: выполняет одновременно соединение коллекций и группировку элементов по ключу
                Reverse: располагает элементы в обратном порядке
                All: определяет, все ли элементы коллекции удовлятворяют определенному условию
                Any: определяет, удовлетворяет хотя бы один элемент коллекции определенному условию
                Contains: определяет, содержит ли коллекция определенный элемент
                Distinct: удаляет дублирующиеся элементы из коллекции
                Except: возвращает разность двух коллекцию, то есть те элементы, которые содератся только в одной коллекции
                Union: объединяет две однородные коллекции
                Intersect: возвращает пересечение двух коллекций, то есть те элементы, которые встречаются в обоих коллекциях
                Count: подсчитывает количество элементов коллекции, которые удовлетворяют определенному условию
                Sum: подсчитывает сумму числовых значений в коллекции
                Average: подсчитывает cреднее значение числовых значений в коллекции
                Min: находит минимальное значение
                Max: находит максимальное значение
                Take: выбирает определенное количество элементов
                Skip: пропускает определенное количество элементов
                TakeWhile: возвращает цепочку элементов последовательности, до тех пор, пока условие истинно
                SkipWhile: пропускает элементы в последовательности, пока они удовлетворяют заданному условию, и затем возвращает оставшиеся элементы
                Concat: объединяет две коллекции
                Zip: объединяет две коллекции в соответствии с определенным условием
                First: выбирает первый элемент коллекции
                FirstOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию
                Single: выбирает единственный элемент коллекции, если коллекция содердит больше или меньше одного элемента, то генерируется исключение
                SingleOrDefault: выбирает первый элемент коллекции или возвращает значение по умолчанию
                ElementAt: выбирает элемент последовательности по определенному индексу
                ElementAtOrDefault: выбирает элемент коллекции по определенному индексу или возвращает значение по умолчанию, если индекс вне допустимого диапазона
                Last: выбирает последний элемент коллекции
                LastOrDefault: выбирает последний элемент коллекции или возвращает значение по умолчанию
            */
        }
    }
}
