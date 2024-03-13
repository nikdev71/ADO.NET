using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CodeFirstManyToMany
{
    // Для работы с БД MS SQL Server необходимо добавить пакет:
    // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)
    // Microsoft.Extensions.Configuration.Json. Этот пакет специально предназначен для работы с конфигурацией в формате json.
    /*
         Migrations предоставляет упорядоченный набор шагов, которые описывают процесс обновления схемы базы данных. 
         В каждом из этих шагов (так называемая миграция) содержится определенный код, который описывает применяемые изменения.
         
         Для использования миграций в Visual Stuido необходимо добавить в проект через менеджер Nuget пакет Microsoft.EntityFrameworkCore.Tools.
        
        Команда Add-Migration проверяет изменения с момента последней миграции и формирует шаблон для новой миграции с любыми обнаруженными изменениями. 
        Можно дать миграциям имя. Например, createDB. Add-Migration createDB
        Команда Update-Database применяет все ожидающие миграции в базе данных. 

        После выполнения миграций в проект будет добавлена папка Migrations с классом миграции:
        Папка содержит три файла:
            XXXXXXXXXXXXXX_createDB.cs: основной файл миграции, который содержит все применяемые действия.
            [Имя_контекста_данных]ModelSnapshot.cs: содержит текущее состояние модели, используется при создании следующей миграции.

        Кроме основных таблиц база данных также будет содержать дополнительную таблицу _EFMigrationsHystory, 
        которая будет хранить информацию о миграциях.
    */

    public class LanguageContext : DbContext
    {
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Language> Languages { get; set; }

        static DbContextOptions<LanguageContext> _options;

        static LanguageContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<LanguageContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public LanguageContext()
           : base(_options)
        {
          
        }
    }
}