using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DapperAcademyGroup
{

    class MainClass
    {
        static string? connectionString;

        static void Main()
        {
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");

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
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var groups = db.Query<AcademyGroup>("SELECT * FROM AcademyGroups");
                int iter = 0;
                foreach (var group in groups)
                    Console.WriteLine($"Группа #{++iter} {group.Name}");
            }
            Console.ReadKey();
        }

        static void AddNewGroup()
        {
            Console.Clear();           
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string groupname;
                do
                {
                    Console.WriteLine("Введите название новой группы: ");
                    groupname = Console.ReadLine()!;
                }
                while (groupname.Trim().IsNullOrEmpty());
                var academygroup = new AcademyGroup { Name = groupname };
                var sqlQuery = "INSERT INTO AcademyGroups (Name) VALUES(@Name)";
                int number = db.Execute(sqlQuery, academygroup);
                if (number != 0)
                    Console.WriteLine($"Группа успешно добавлена!");
            }
            Console.ReadKey();
        }

        static void EditGroup()
        {
            Console.Clear();          
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var group = db.Query<AcademyGroup>("SELECT * FROM AcademyGroups").ToList()[number - 1];
                string groupname;
                do
                {
                    Console.WriteLine("Введите новое название группы: ");
                    groupname = Console.ReadLine()!;
                }
                while (groupname.Trim().IsNullOrEmpty());
                group.Name = groupname;
                var sqlQuery = "UPDATE AcademyGroups SET Name = @Name WHERE Id = @Id";
                number = db.Execute(sqlQuery, group);
                if (number != 0)
                    Console.WriteLine("Группа успешно изменена!");
            }
            Console.ReadKey();
        }

        static void RemoveGroup()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var group = db.Query<AcademyGroup>("SELECT * FROM AcademyGroups").ToList()[number - 1];
                var sqlQuery = "DELETE FROM AcademyGroups WHERE Id = @id";
                number = db.Execute(sqlQuery, new { group.Id });
                if (number != 0)
                    Console.WriteLine("Группа успешно удалена!");
            }
            Console.ReadKey();
        }

        static void ShowAllStudents()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var students = db.Query<StudentViewModel>(
                    "SELECT Students.FirstName, Students.LastName, Students.Age, Students.GPA, AcademyGroups.Name " +
                    "FROM AcademyGroups INNER JOIN Students ON AcademyGroups.Id = Students.AcademyGroupId");
                int iter = 0;
                foreach (var st in students)
                {
                    Console.Write($"Студент #{++iter}{st.FirstName,15}");
                    Console.Write($"{st.LastName,15}");
                    Console.Write($"{st.Age,10}");
                    Console.Write($"{st.GPA,10}");
                    Console.WriteLine($"{st.Name,10}");
                }
                Console.WriteLine();
                var stud = db.Query<Student>("SELECT * FROM Students");
                iter = 0;
                foreach (var st in stud)
                {
                    Console.Write($"Студент #{++iter}{st.FirstName,15}");
                    Console.Write($"{st.LastName,15}");
                    Console.Write($"{st.Age,10}");
                    Console.Write($"{st.GPA,10}");
                    var group = db.QueryFirstOrDefault<AcademyGroup>("SELECT * FROM AcademyGroups WHERE Id = @AcademyGroupId", 
                        new {st.AcademyGroupId});
                    Console.WriteLine($"{group?.Name,10}");
                }
            }
            Console.ReadKey();
        }

        static void ShowStudentsByGroup()
        {
            Console.Clear();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var group = db.Query<AcademyGroup>("SELECT * FROM AcademyGroups").ToList()[number - 1];
                var students = db.Query<Student>("SELECT * FROM Students WHERE AcademyGroupId = @Id",
                    new { group.Id });
                int iter = 0;
                foreach (var st in students)
                {
                    Console.Write($"Студент #{++iter}{st.FirstName,15}");
                    Console.Write($"{st.LastName,15}");
                    Console.Write($"{st.Age,10}");
                    Console.Write($"{st.GPA,10}");
                    Console.WriteLine($"{group?.Name,10}");
                }
            }
            Console.ReadKey();
        }

        static void AddNewStudent()
        {
            Console.Clear();
            
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string firstname, lastname;
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
                float gpa = float.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите порядковый номер группы: ");
                int number = int.Parse(Console.ReadLine()!);
                var group = db.Query<AcademyGroup>("SELECT * FROM AcademyGroups").ToList()[number - 1];
                var student = new Student { FirstName = firstname, LastName = lastname, Age = age, 
                    GPA = gpa, AcademyGroupId = group.Id };

                var sqlQuery = "INSERT INTO Students (FirstName, LastName, Age, GPA, AcademyGroupId) " +
                    "VALUES(@FirstName, @LastName, @Age, @GPA, @AcademyGroupId)";
                number = db.Execute(sqlQuery, student);
                if (number != 0)
                    Console.WriteLine("Студент успешно добавлен!");
            }
            Console.ReadKey();
        }

        static void EditStudent()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Console.WriteLine("Введите порядковый номер студента: ");
                int number = int.Parse(Console.ReadLine()!);
                var student = db.Query<Student>("SELECT * FROM Students").ToList()[number - 1];
                string firstname, lastname;
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
                float gpa = float.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите порядковый номер группы: ");
                number = int.Parse(Console.ReadLine()!);
                var group = db.Query<AcademyGroup>("SELECT * FROM AcademyGroups").ToList()[number - 1];
                student.FirstName = firstname;
                student.LastName = lastname;
                student.Age = age;
                student.GPA = gpa;
                student.AcademyGroupId = group.Id;
                var sqlQuery = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Age = @Age, " +
                    "GPA = @GPA, AcademyGroupId = @AcademyGroupId WHERE Id = @Id";
                number = db.Execute(sqlQuery, student);
                if (number != 0)
                    Console.WriteLine("Студент успешно изменен!");
            }          
            Console.ReadKey();
        }

        static void RemoveStudent()
        {
            Console.Clear();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                Console.WriteLine("Введите порядковый номер студента: ");
                int number = int.Parse(Console.ReadLine()!);
                var student = db.Query<Student>("SELECT * FROM Students").ToList()[number - 1];
                var sqlQuery = "DELETE FROM Students WHERE Id = @id";
                number = db.Execute(sqlQuery, new { student.Id });
                if (number != 0)
                    Console.WriteLine("Студент успешно удален!");
            }
            Console.ReadKey();
        }
    }
}
