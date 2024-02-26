using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CodeFirst.EagerLoading
{
    class MainClass
    {
        // Для работы с БД MS SQL Server необходимо добавить пакет:
        // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)

        static void Main()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. Показать все группы");
                    Console.WriteLine("2. Добавить группу");
                    Console.WriteLine("3. Редактировать группу");
                    Console.WriteLine("4. Удалить группу");
                    Console.WriteLine("5. Показать всех студентов");
                    Console.WriteLine("6. Показать студентов конкретной группы");
                    Console.WriteLine("7. Добавить студента");
                    Console.WriteLine("8. Редактировать студента");
                    Console.WriteLine("9. Удалить студента");
                    Console.WriteLine("0. Выход");
                    int result = int.Parse(Console.ReadLine()!);
                    switch (result)
                    {
                        case 1:
                            ShowAllGroups();
                            break;
                        case 2:
                            AddNewGroup();
                            break;
                        case 3:
                            EditGroup();
                            break;
                        case 4:
                            RemoveGroup();
                            break;
                        case 5:
                            ShowAllStudents();
                            break;
                        case 6:
                            ShowStudentsByGroup();
                            break;
                        case 7:
                            AddNewStudent();
                            break;
                        case 8:
                            EditStudent();
                            break;
                        case 9:
                            RemoveStudent();
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

        static void ShowAllGroups()
        {
            Console.Clear();
            using (var db = new AcademyGroupContext())
            {
                var query = from gr in db.AcademyGroups
                            select gr;
                int iter = 0;
                foreach (var group in query)
                    Console.WriteLine($"Группа #{++iter} {group.Name}");
            }
            Console.ReadKey();
        }

        static void AddNewGroup()
        {
            Console.Clear();
            string groupname;
            do
            {
                Console.WriteLine("Введите название новой группы: ");
                groupname = Console.ReadLine()!;
            }
            while (groupname.Trim().IsNullOrEmpty());
            using (var db = new AcademyGroupContext())
            {
                var academygroup = new AcademyGroup { Name = groupname };
                db.AcademyGroups.Add(academygroup);
                db.SaveChanges();
                Console.WriteLine("Группа успешно добавлена!");

            }
            Console.ReadKey();
        }

        static void EditGroup()
        {
            Console.Clear();
            using (var db = new AcademyGroupContext())
            {
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var query = (from gr in db.AcademyGroups
                             select gr).ToList()[number - 1];
                string groupname;
                do
                {
                    Console.WriteLine("Введите новое название группы: ");
                    groupname = Console.ReadLine()!;
                }
                while (groupname.Trim().IsNullOrEmpty());
                query.Name = groupname;
                db.SaveChanges();
                Console.WriteLine("Группа успешно изменена!");
            }
            Console.ReadKey();
        }

        static void RemoveGroup()
        {
            Console.Clear();
            using (var db = new AcademyGroupContext())
            {
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var query = (from gr in db.AcademyGroups
                             select gr).ToList()[number - 1];
                db.AcademyGroups.RemoveRange(query);
                db.SaveChanges();
                Console.WriteLine("Группа успешно удалена!");
            }
            Console.ReadKey();
        }

        static void ShowAllStudents()
        {
            Console.Clear();
            using (var db = new AcademyGroupContext())
            {
                var query = (from st in db.Students.Include(s => s.AcademyGroup)
                             select st).ToList();
                int iter = 0;
                foreach (var st in query)
                {
                    Console.Write($"Студент #{++iter}{st.FirstName,15}");
                    Console.Write($"{st.LastName,15}");
                    Console.Write($"{st.Age,10}");
                    Console.Write($"{st.GPA,10}");
                    Console.WriteLine($"{st.AcademyGroup?.Name,10}");
                }
            }
            Console.ReadKey();
        }

        static void ShowStudentsByGroup()
        {
            Console.Clear();
            using (var db = new AcademyGroupContext())
            {
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var query = (from gr in db.AcademyGroups.Include(gr => gr.Students)
                             select gr).ToList()[number - 1];
                int iter = 0;
                foreach (var st in query.Students!)
                {
                    Console.Write($"Студент #{++iter}{st.FirstName,15}");
                    Console.Write($"{st.LastName,15}");
                    Console.Write($"{st.Age,10}");
                    Console.Write($"{st.GPA,10}");
                    Console.WriteLine($"{st.AcademyGroup?.Name,10}");
                }
            }
            Console.ReadKey();
        }

        static void AddNewStudent()
        {
            Console.Clear();
            string firstname, lastname;

            using (var db = new AcademyGroupContext())
            {
                do
                {
                    Console.WriteLine("Введите имя студента: ");
                    firstname = Console.ReadLine()!;
                }
                while (firstname.Trim().IsNullOrEmpty());
                do
                {
                    Console.WriteLine("Введите фамилию студента: ");
                    lastname = Console.ReadLine()!;
                }
                while (lastname.Trim().IsNullOrEmpty());
                Console.WriteLine("Введите возраст студента: ");
                int age = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите средний балл студента: ");
                double gpa = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var query = (from gr in db.AcademyGroups
                             select gr).ToList()[number - 1];
                var st = new Student { FirstName = firstname, LastName = lastname, Age = age, GPA = gpa, AcademyGroup = query };
                db.Students?.Add(st);
                db.SaveChanges();
                Console.WriteLine("Студент успешно добавлен!");

            }
            Console.ReadKey();
        }

        static void EditStudent()
        {
            Console.Clear();
            string firstname, lastname;

            using (var db = new AcademyGroupContext())
            {
                Console.WriteLine("Введите порядковый номер студента: ");
                int number = int.Parse(Console.ReadLine()!);
                var student = (from st in db.Students
                               select st).ToList()[number - 1];
                do
                {
                    Console.WriteLine("Введите имя студента: ");
                    firstname = Console.ReadLine()!;
                }
                while (firstname.Trim().IsNullOrEmpty());
                do
                {
                    Console.WriteLine("Введите фамилию студента: ");
                    lastname = Console.ReadLine()!;
                }
                while (lastname.Trim().IsNullOrEmpty());
                Console.WriteLine("Введите возраст студента: ");
                int age = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите средний балл студента: ");
                double gpa = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите порядковый номер группы: ");
                number = int.Parse(Console.ReadLine()!);
                var group = (from gr in db.AcademyGroups
                             select gr).ToList()[number - 1];
                student.FirstName = firstname;
                student.LastName = lastname;
                student.Age = age;
                student.GPA = gpa;
                student.AcademyGroup = group;
                db.SaveChanges();
                Console.WriteLine("Студент успешно изменен!");

            }
            Console.ReadKey();
        }

        static void RemoveStudent()
        {
            Console.Clear();
            using (var db = new AcademyGroupContext())
            {
                Console.WriteLine("Введите порядковый номер студента: ");
                int number = int.Parse(Console.ReadLine()!);
                var student = (from st in db.Students
                               select st).ToList()[number - 1];
                db.Students.RemoveRange(student);
                db.SaveChanges();
                Console.WriteLine("Студент успешно удален!");

            }
            Console.ReadKey();
        }
    }
}
