using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
#pragma warning disable CA1416

namespace ADO.NET_Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand();
            try
            {
                connect.Open();
                cmd.Connection = connect;
                cmd.CommandText = "select * from Books where pages>=@lowborder and pages<=@highborder";
                cmd.CommandType = CommandType.Text;
                SqlParameter first;
                SqlParameter second;
                first = cmd.Parameters.Add("lowborder", SqlDbType.Int);
                second = cmd.Parameters.Add("highborder", SqlDbType.Int);
                first.Value = Convert.ToInt32(textBox1.Text);
                second.Value = Convert.ToInt32(textBox2.Text);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connect.Close();
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
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand("Delete_Book", connect);
            try
            {
                connect.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("name", SqlDbType.NVarChar);
                cmd.Parameters["name"].Value = "%Visual%Basic%";
                int n = cmd.ExecuteNonQuery();
                MessageBox.Show("Количество удаленных записей: " + n.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connect.Close();
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

            SqlConnection connect = new SqlConnection();
            SqlCommand cmd = new SqlCommand("how_many_books", connect);
            try
            {
                connect.ConnectionString = @"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI";
                connect.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = cmd.Parameters.Add("quantity", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Количество записей в таблице Books: " + param.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connect.Close();
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

            SqlConnection connect = new SqlConnection();
            SqlCommand cmd = new SqlCommand("MaxPages", connect);
            try
            {
                connect.ConnectionString = @"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI";
                connect.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = cmd.Parameters.Add("s", SqlDbType.Int);
                param.Direction = ParameterDirection.ReturnValue;
                SqlParameter param2 = cmd.Parameters.Add("press", SqlDbType.NVarChar);
                param2.Direction = ParameterDirection.Output;
                param2.Size = 255;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Издательство: " + param2.Value.ToString() + "  Сумма: " + param.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connect.Close();
            }
        }

        private void BooksList_Click(object sender, EventArgs e)
        {
            /*
              Create function BooksList()
               returns table
               as
               return (
                   Select Name, Author, Themes, Press                 
                   from books 
                   )
               go

              select * from BooksList()
             */
            SqlConnection connect = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                connect.ConnectionString = @"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI";
                connect.Open();
                command.Connection = connect;
                command.CommandText = "select * from BooksList()";
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
        }

        private void ShowBooksByThemes_Click(object sender, EventArgs e)
        {
            /*
            create procedure ShowBooksByThemes
            @themes nvarchar (255) as
            select * from books
            where themes like @themes
            order by Name desc
            go

            execute ShowBooksByThemes 'Программирование' 
            */
            SqlConnection connect = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                connect.ConnectionString = @"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI";
                connect.Open();
                command.Connection = connect;
                command.CommandText = "execute ShowBooksByThemes 'Программирование'";
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
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
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand cmd = new SqlCommand("Add_Book", connect);
            try
            {
                connect.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("name", SqlDbType.NVarChar);
                cmd.Parameters["name"].Value = "SQL";
                cmd.Parameters.Add("pages", SqlDbType.Int);
                cmd.Parameters["pages"].Value = 1000;
                cmd.Parameters.Add("yearpress", SqlDbType.Int);
                cmd.Parameters["yearpress"].Value = 2020;
                cmd.Parameters.Add("themes", SqlDbType.NVarChar);
                cmd.Parameters["themes"].Value = "Базы данных";
                cmd.Parameters.Add("author", SqlDbType.NVarChar);
                cmd.Parameters["author"].Value = "Генник";
                cmd.Parameters.Add("press", SqlDbType.NVarChar);
                cmd.Parameters["press"].Value = "Питер";
                cmd.Parameters.Add("comment", SqlDbType.NVarChar);
                cmd.Parameters["comment"].Value = "Справочник";
                cmd.Parameters.Add("quantity", SqlDbType.Int);
                cmd.Parameters["quantity"].Value = 10;
                int n = cmd.ExecuteNonQuery();
                if (n == 1)
                    MessageBox.Show("Книга успешно добавлена в таблицу!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                connect.Close();
            }
        }
    }
}




