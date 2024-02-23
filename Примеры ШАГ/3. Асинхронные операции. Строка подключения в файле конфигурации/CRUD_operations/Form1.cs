using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// Используя менеджер пакетов Manage NuGet Packages, подключить три пакета:
// System.Data.SqlClient
// Microsoft.Extensions.Configuration
// Microsoft.Extensions.Configuration.Json
// В свойствах файла конфигурации требуется указать значение Copy if newer для свойства Copy to Output Directory

namespace CRUD_operations
{
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

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "SELECT name, themes, author FROM books";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                int count = reader.FieldCount;
                listBox1.DataSource = null;
                listBox1.Items.Clear();
                while (await reader.ReadAsync())
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
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "SELECT name, themes, author FROM books";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "SELECT name, themes, author FROM books";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                listBox1.Items.Clear();
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "Name";
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "SELECT COUNT(*) FROM books";
                object? res = await command.ExecuteScalarAsync();
                int quantity = (int)res!;
                MessageBox.Show("Количество записей в таблице Books - " + res.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "INSERT INTO books (name, pages, yearpress, themes, author, press, comment, quantity )VALUES ('SQL', 1000, 2004, 'Базы данных', 'Генник', 'Питер', 'Карманный справочник', 3)";
                int n = await command.ExecuteNonQueryAsync();
                if (n == 1)
                    MessageBox.Show("Запись успешно добавлена в таблицу!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "DELETE FROM books WHERE id<>1 and name = 'SQL'";
                int n = await command.ExecuteNonQueryAsync();
                MessageBox.Show("Количество удаленных записей: " + n.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                command.Connection = connect;
                command.CommandText = "UPDATE books SET yearpress = 2007 WHERE press='BHV';";
                int n = await command.ExecuteNonQueryAsync();
                MessageBox.Show("Количество обновленных записей: " + n.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await command.DisposeAsync();
                await connect.CloseAsync();
            }
        }
    }
}