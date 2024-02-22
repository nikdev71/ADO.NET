using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
#pragma warning disable CA1416

namespace WindowsApplication1
{
    // Используя менеджер пакетов Manage NuGet Packages, подключить три пакета:
    // System.Data.SqlClient
    // Microsoft.Extensions.Configuration
    // Microsoft.Extensions.Configuration.Json
    // В свойствах файла конфигурации требуется указать значение Copy if newer для свойства Copy to Output Directory

    public partial class Form1 : Form
    {
        DataSet dataset = new DataSet();
        SqlConnection connection;
        SqlDataAdapter adapter1, adapter2, adapter3;
        SqlCommandBuilder build1, build2, build3;

        public Form1()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        private void Create_Tables_Click(object sender, EventArgs e)
        {
            try
            {
                adapter1 = new SqlDataAdapter("select * from customers", connection);
                // SqlCommandBuilder автоматически генерирует однотабличные команды, которые позволяют согласовать изменения, вносимые в объект DataSet, с базой данных
                build1 = new SqlCommandBuilder(adapter1); // команды INSERT, UPDATE, DELETE будут сгенерированы автоматически

                MessageBox.Show(build1.GetUpdateCommand().CommandText);
                MessageBox.Show(build1.GetInsertCommand().CommandText);
                MessageBox.Show(build1.GetDeleteCommand().CommandText);

                // Создаем пустую таблицу customers
                DataTable customers = dataset.Tables.Add("customers");
                //Добавляем столбцы в таблицу
                customers.Columns.Add("id", typeof(int));
                customers.Columns.Add("name", typeof(string));
                customers.Columns.Add("age", typeof(int));
                customers.Constraints.Add("PK_Customers", customers.Columns["id"], true); // Первичный ключ
                customers.Columns["id"].AutoIncrement = true; // Identity
                customers.Columns["id"].AllowDBNull = false; // Недопустима пустая ячейка
                customers.Columns["name"].AllowDBNull = false;

                adapter1.Fill(dataset, "customers"); // заполняем DataSet и привязываем к нему DataGrid
                dataGridView1.DataSource = customers;

                adapter2 = new SqlDataAdapter("select * from salesmen", connection);
                build2 = new SqlCommandBuilder(adapter2); // команды INSERT, UPDATE, DELETE будут сгенерированы автоматически             

                // Создаем пустую таблицу salesmen
                DataTable salesmen = dataset.Tables.Add("salesmen");
                //Добавляем столбцы в таблицу
                salesmen.Columns.Add("id", typeof(int));
                salesmen.Columns.Add("name", typeof(string));
                salesmen.Columns.Add("age", typeof(int));
                salesmen.Constraints.Add("PK_Salesmen", salesmen.Columns["id"], true); // Первичный ключ
                salesmen.Columns["id"].AutoIncrement = true; // Identity
                salesmen.Columns["id"].AllowDBNull = false; // Недопустима пустая ячейка
                salesmen.Columns["name"].AllowDBNull = false;

                adapter2.Fill(dataset, "salesmen"); // заполняем DataSet и привязываем к нему DataGrid
                dataGridView2.DataSource = salesmen;

                adapter3 = new SqlDataAdapter("select * from sales", connection);
                build3 = new SqlCommandBuilder(adapter3); // команды INSERT, UPDATE, DELETE будут сгенерированы автоматически

                DataTable sales = dataset.Tables.Add("sales");
                //Добавляем столбцы в таблицу
                sales.Columns.Add("id", typeof(int));
                sales.Columns.Add("id_cust", typeof(int));
                sales.Columns.Add("id_sal", typeof(int));
                sales.Constraints.Add("PK_Sales", sales.Columns["id"], true); // Первичный ключ
                sales.Columns["id"].AutoIncrement = true; // Identity
                sales.Columns["id"].AllowDBNull = false; // Недопустима пустая ячейка
                sales.Columns["id_cust"].AllowDBNull = false;
                sales.Columns["id_sal"].AllowDBNull = false;
                ForeignKeyConstraint FK_Customers = // Внешний ключ
                    new ForeignKeyConstraint("FK_Customers", customers.Columns["id"], sales.Columns["id_cust"]);
                FK_Customers.DeleteRule = Rule.Cascade;
                FK_Customers.UpdateRule = Rule.Cascade;
                ForeignKeyConstraint FK_Salesmans = // Внешний ключ
                    new ForeignKeyConstraint("FK_Salesmen", salesmen.Columns["id"], sales.Columns["id_sal"]);
                FK_Salesmans.DeleteRule = Rule.Cascade;
                FK_Salesmans.UpdateRule = Rule.Cascade;
                sales.Constraints.Add(FK_Customers);
                sales.Constraints.Add(FK_Salesmans);
                sales.Columns.Add("quantity", typeof(int));
                sales.Columns["quantity"].AllowDBNull = false;
                sales.Columns.Add("price", typeof(int));
                sales.Columns["price"].AllowDBNull = false;
                sales.Columns.Add("salesdate", typeof(DateTime));
                sales.Columns["salesdate"].AllowDBNull = false;

                adapter3.Fill(dataset, "sales"); // заполняем DataSet и привязываем к нему DataGrid
                dataGridView3.DataSource = dataset.Tables["sales"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Insert_Rows_Click(object sender, EventArgs e)
        {
            try
            {
                // добавляем записи в таблицу покупателей
                string[] CustomerName = { "Сергеенко", "Иванченко", "Мариненко" };
                int[] CustomerAge = { 30, 33, 33 };
                for (int i = 0; i < 3; i++)
                {
                    DataRow row = dataset.Tables["customers"].NewRow();
                    row["name"] = CustomerName[i];
                    row["age"] = CustomerAge[i];
                    dataset.Tables["customers"].Rows.Add(row);
                }
                dataGridView1.Refresh();

                // Внесем изменения в источник данных
                adapter1.Update(dataset, "customers");

                // добавляем записи в таблицу продавцов
                string[] SalesmanName = { "Василенко", "Феденко", "Панченко" };
                int[] SalesmanAge = { 16, 18, 18 };
                for (int i = 0; i < 3; i++)
                {
                    DataRow row = dataset.Tables["salesmen"].NewRow();
                    row["name"] = SalesmanName[i];
                    row["age"] = SalesmanAge[i];
                    dataset.Tables["salesmen"].Rows.Add(row);
                }
                dataGridView2.Refresh();

                // Внесем изменения в источник данных
                adapter2.Update(dataset, "salesmen");

                // добавляем записи в таблицу сделок
                Random rnd = new Random();
                for (int i = 0; i < 6; i++)
                {
                    DataRow row = dataset.Tables["sales"].NewRow();
                    row["id_cust"] = rnd.Next(1, 9);
                    row["id_sal"] = rnd.Next(1, 9);
                    row["quantity"] = rnd.Next(1, 100);
                    row["price"] = rnd.Next(1, 200);
                    row["salesdate"] = DateTime.Now;
                    dataset.Tables["sales"].Rows.Add(row);
                }
                dataGridView3.Refresh();

                // Внесем изменения в источник данных
                adapter3.Update(dataset, "sales");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Select_Data_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                /*
                 Class DataTable
                    public DataRow[] Select(); - Применяет некоторый фильтр по отношению к выборке данных. 
                    Возвращается массив строк, которые были получены в результате выборки.
                */
                DataRow[] ar = dataset.Tables["customers"].Select("name like '%е%'");
                foreach (DataRow r in ar)
                {
                    string str = "", tmp = "";
                    for (int i = 0; i < r.ItemArray.Length; i++)
                    {
                        tmp = r.ItemArray[i].ToString();
                        str += tmp + "    ";
                    }
                    listBox1.Items.Add(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Row_Click(object sender, EventArgs e)
        {
            try
            {
                // Удалим из таблицы запись с указанным индексом
                dataset.Tables["customers"].Rows[int.Parse(textBox1.Text)].Delete();
                dataGridView1.Refresh();

                // Внесем изменения в источник данных
                adapter1.Update(dataset, "customers");
                dataGridView3.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Row_Click(object sender, EventArgs e)
        {
            try
            {
                // Получим запись с указанным идентификатором
                DataRow[] ar = dataset.Tables["salesmen"].Select("id = " + textBox3.Text);

                // Изменим данные в поле name
                ar[0]["name"] = textBox2.Text;
                dataGridView2.Refresh();

                // Внесем изменения в источник данных
                adapter2.Update(dataset, "salesmen");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
