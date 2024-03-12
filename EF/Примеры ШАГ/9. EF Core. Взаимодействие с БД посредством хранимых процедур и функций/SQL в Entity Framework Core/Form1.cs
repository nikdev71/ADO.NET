using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace SQL_в_Entity_Framework_Core
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void All_Objects_From_Books(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    var books = db.Books.FromSqlRaw("SELECT * FROM Books").OrderBy(b => b.Name).ToList();
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Parameterized_Query(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter first = new SqlParameter("@lowborder", Convert.ToInt32(textBox1.Text));
                    SqlParameter second = new SqlParameter("@highborder", Convert.ToInt32(textBox2.Text));
                    var books = db.Books.FromSqlRaw("select * from Books where pages>=@lowborder and pages<=@highborder", first, second).ToList();
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SqlInterpolated(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    var first = Convert.ToInt32(textBox1.Text);
                    var second = Convert.ToInt32(textBox2.Text);
                    var books = db.Books.FromSqlInterpolated($"select * from Books where pages>={first} and pages<={second}").ToList();
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryToInsert(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    int numberOfRowInserted = db.Database.ExecuteSqlRaw("INSERT INTO books (name, pages, yearpress, themes, author, press, comment, quantity )VALUES ('SQL', 1000, 2004, 'Базы данных', 'Генник', 'BHV', 'Карманный справочник', 3)");
                    if (numberOfRowInserted == 1)
                        MessageBox.Show("Запись успешно добавлена в таблицу!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryToDelete(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    int id = 1;
                    string name = "SQL";
                    int numberOfRowDeleted = db.Database.ExecuteSqlInterpolated($"DELETE FROM books WHERE id<>{id} and name = {name}");
                    MessageBox.Show("Количество удаленных записей: " + numberOfRowDeleted.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryToUpdate(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    int year = 2007;
                    string press = "BHV";
                    int numberOfRowUpdated = db.Database.ExecuteSqlInterpolated($"UPDATE books SET yearpress = {year} WHERE press={press}");
                    MessageBox.Show("Количество обновленных записей: " + numberOfRowUpdated.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
          Create function BooksList(
                @Author nvarchar(50)
            )
              returns @returntable table
              (
                Id int,
                Name nvarchar(100),
	            Author nvarchar (50),
                Themes nvarchar (50),
	            Press nvarchar (50),
	            Pages int,
	            YearPress int,
	            Comment nvarchar (50),
	            Quantity int
            )
              as
              BEGIN
                INSERT @returntable
                SELECT Id, Name, Author, Themes, Press, Pages, YearPress, Comment, Quantity
	            FROM Books WHERE Author = @Author
                RETURN
              END
              go

              select * from BooksList(N'Архангельский')
        */

        private void ExecuteTableFunction(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter param = new SqlParameter("@author", "Архангельский");
                    var books = db.Books.FromSqlRaw("SELECT * FROM BooksList (@author)", param).ToList();
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
            create procedure ShowBooksByThemes
            @themes nvarchar (255) as
            select * from books
            where themes like @themes
            order by Name desc
            go

            execute ShowBooksByThemes 'Программирование' 
         */

        private void ShowBooksByThemes(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter param = new("@themes", "Программирование");
                    var books = db.Books.FromSqlRaw("ShowBooksByThemes @themes", param).ToList();
                    dataGridView1.DataSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void How_many_books(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@quantity",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output,
                    };
                    db.Database.ExecuteSqlRaw("how_many_books @quantity out", param);
                    MessageBox.Show("Количество записей в таблице Books: " + param.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void Delete_Book(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@name",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = "%Visual%Basic%"
                    };
                    var numberOfRowDeleted = db.Database.ExecuteSqlRaw("Delete_Book @name", param);
                    MessageBox.Show("Количество удаленных записей: " + numberOfRowDeleted.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
              Отобразить издательство, у которого наибольшее количество страниц.

               create view sum_of_pages (Pages, Press) as
               SELECT sum(Pages), Press from books
               group by Press
               go

              create procedure MaxPages @press nvarchar(255) output, @sum int output
              as
              SELECT @press=Press, @sum=sum(Pages)
              FROM books
              GROUP BY Press
              HAVING sum(Pages)= (select max(Pages)from sum_of_pages)
              go

               declare @izdat nvarchar(255), @sum int
               execute MaxPages @izdat output, @sum output
               select 'Издательство:',@izdat,'Сумма:',@sum
         */

        private void MaxPages(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@press",
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Output,
                        Size = 50
                    };
                    SqlParameter param2 = new()
                    {
                        ParameterName = "@sum",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("MaxPages @press out, @sum out", param, param2);
                    MessageBox.Show("Издательство: " + param.Value.ToString() + "  Сумма: " + param2.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
            create procedure Add_Book @name nvarchar(100), @pages int, @yearpress int,
						 @themes nvarchar(50), @author nvarchar(50), @press nvarchar(50), 
						 @comment nvarchar(50), @quantity int
            as
            INSERT INTO books (name, pages, yearpress, themes, author, press, comment, quantity)
            VALUES (@name, @pages, @yearpress, @themes, @author, @press, @comment, @quantity)
        */

        private void Add_Book(object sender, EventArgs e)
        {
            try
            {
                using (DatabaseBooksContext db = new DatabaseBooksContext())
                {
                    SqlParameter[] sqlParameters = {
                        new SqlParameter("name", "SQL"),
                        new SqlParameter("pages", 1000),
                        new SqlParameter("yearpress", 2020),
                        new SqlParameter("themes", "Базы данных"),
                        new SqlParameter("author", "Генник"),
                        new SqlParameter("press", "BHV"),
                        new SqlParameter("comment", "Справочник"),
                        new SqlParameter("quantity", 10)
                    };
                    var numberOfRowInserted = db.Database.ExecuteSqlRaw("Add_Book @name, @pages, @yearpress, @themes, @author, @press, @comment, @quantity", sqlParameters);
                    if (numberOfRowInserted == 1)
                        MessageBox.Show("Запись успешно добавлена в таблицу!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
