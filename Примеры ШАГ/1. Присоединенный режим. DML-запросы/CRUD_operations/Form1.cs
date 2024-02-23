using System.Data;
using System.Data.SqlClient;

namespace CRUD_operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "SELECT name, themes, author FROM books";
                SqlDataReader reader = command.ExecuteReader();
                int count = reader.FieldCount;
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                while (reader.Read())
                {
                    string? res = "", temp = "";
                    for (int i = 0; i < count; i++)
                    {
                        temp = reader[i].ToString();
                        res += temp + "  ";
                    }
                    listBox1.Items.Add(res);
                    res = "";
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "SELECT name, themes, author FROM books";
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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "SELECT name, themes, author FROM books";
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                listBox1.Items.Clear();
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
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

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "SELECT COUNT(*) FROM books";
                int res = (int)command.ExecuteScalar();
                MessageBox.Show("Количество записей в таблице Books - " + res.ToString());
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

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "INSERT INTO books (name, pages, yearpress, themes, author, press, comment, quantity )VALUES ('SQL', 1000, 2004, 'Базы данных', 'Генник', 'Питер', 'Карманный справочник', 3)";
                int n = command.ExecuteNonQuery();
                if (n == 1)
                    MessageBox.Show("Запись успешно добавлена в таблицу!");
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

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "DELETE FROM books WHERE id<>1 and name = 'SQL'";
                int n = command.ExecuteNonQuery();
                MessageBox.Show("Количество удаленных записей: " + n.ToString());
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

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "UPDATE books SET yearpress = 2007 WHERE press='BHV';";
                int n = command.ExecuteNonQuery();
                MessageBox.Show("Количество обновленных записей: " + n.ToString());
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
    }
}