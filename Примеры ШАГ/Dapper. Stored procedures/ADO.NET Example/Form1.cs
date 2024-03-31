using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
#pragma warning disable CA1416

namespace ADO.NET_Example
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Themes { get; set; }
        public string Press { get; set; }
        public string Pages { get; set; }
    }

    public partial class Form1 : Form
    {
        string? connectionString;

        public Form1()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        private void Select_Click(object sender, EventArgs e)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var lowborder = Convert.ToInt32(textBox1.Text);
                    var highborder = Convert.ToInt32(textBox2.Text);
                    var sqlQuery = "select Name, Author, Themes, Press, Pages from Books where pages>=@lowborder and pages<=@highborder";
                    var books = db.Query<Book>(sqlQuery, new { lowborder, highborder });
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Book_Click(object sender, EventArgs e)
        {
            /*
           create procedure Delete_Book
           @name nvarchar (255) as
           delete from books where name like @name 
		   go
		   
            declare @name nvarchar(255)
            set @name = '%Visual%Basic%'
            execute Delete_Book @name 
            go
            */

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@name", "%Visual%Basic%", dbType: DbType.String, direction: ParameterDirection.Input);
                    int n = db.Execute("Delete_Book", dynamicParams, commandType: CommandType.StoredProcedure);
                    MessageBox.Show("Количество удаленных записей: " + n.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void How_many_books_Click(object sender, EventArgs e)
        {
            /*
            create procedure how_many_books @quantity int output  
            as
            SELECT @quantity = COUNT(*) FROM books 
            go

            declare @quantity int
            execute how_many_books @quantity output
            select 'Количество записей в таблице Books:',@quantity
            go
            */

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@quantity", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    db.Execute("how_many_books", dynamicParams, commandType: CommandType.StoredProcedure);
                    MessageBox.Show("Количество записей в таблице Books: " + dynamicParams.Get<int>("@quantity").ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MaxPages_Click(object sender, EventArgs e)
        {
            /*
              Отобразить издательство, у которого наибольшее количество страниц.

               create view sum_of_pages (Pages, Press)as
               SELECT sum(Pages), Press from books
               group by Press
               go

              create procedure MaxPages @press nvarchar(255) output
               as
               declare @s int
               SELECT @press=Press, @s=sum(Pages)
               FROM books
               GROUP BY Press
               HAVING sum(Pages)= (select max(Pages)from sum_of_pages)
               return @s
               go

                declare @izdat nvarchar(255), @summa int
                execute @summa = MaxPages @izdat output
                select 'Издательство:',@izdat,'Сумма:',@summa
            */

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@press", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    dynamicParams.Add("@s", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    db.Execute("MaxPages", dynamicParams, commandType: CommandType.StoredProcedure);
                    MessageBox.Show("Издательство: " + dynamicParams.Get<string>("@press")
                                        + "  Сумма: " + dynamicParams.Get<int>("@s").ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BooksList_Click(object sender, EventArgs e)
        {
            /*
              Create function BooksList()
               returns table
               as
               return (
                   Select Name, Author, Themes, Press, Pages                 
                   from books 
                   )
               go

              select * from BooksList()
             */

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var books = db.Query<Book>("SELECT * FROM BooksList()");
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowBooksByThemes_Click(object sender, EventArgs e)
        {
            /*
            create procedure ShowBooksByThemes
            @themes nvarchar (255) as
            select Name, Author, Themes, Press, Pages from books
            where themes like @themes
            order by Name desc
            go

            execute ShowBooksByThemes 'Программирование' 
            */

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@themes", "Программирование", dbType: DbType.String, direction: ParameterDirection.Input);
                    var books = db.Query<Book>("ShowBooksByThemes", dynamicParams, commandType: CommandType.StoredProcedure);
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Book_Click(object sender, EventArgs e)
        {
            /*
            create procedure Add_Book @name nvarchar(100), @pages int, @yearpress int,
						 @themes nvarchar(50), @author nvarchar(50), @press nvarchar(50), 
						 @comment nvarchar(50), @quantity int
            as
            INSERT INTO books (name, pages, yearpress, themes, author, press, comment, quantity)
            VALUES (@name, @pages, @yearpress, @themes, @author, @press, @comment, @quantity)
             */

            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@name", "SQL", dbType: DbType.String, direction: ParameterDirection.Input);
                    dynamicParams.Add("@pages", 1000, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    dynamicParams.Add("@yearpress", 2021, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    dynamicParams.Add("@themes", "Базы данных", dbType: DbType.String, direction: ParameterDirection.Input);
                    dynamicParams.Add("@author", "Генник", dbType: DbType.String, direction: ParameterDirection.Input);
                    dynamicParams.Add("@press", "BHV", dbType: DbType.String, direction: ParameterDirection.Input);
                    dynamicParams.Add("@comment", "Справочник", dbType: DbType.String, direction: ParameterDirection.Input);
                    dynamicParams.Add("@quantity", 200, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    int n = db.Execute("Add_Book", dynamicParams, commandType: CommandType.StoredProcedure);
                    if (n == 1)
                        MessageBox.Show("Книга успешно добавлена в таблицу!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}




