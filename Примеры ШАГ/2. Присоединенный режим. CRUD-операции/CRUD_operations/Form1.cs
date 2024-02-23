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

        void PrintBooks()
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "SELECT * FROM books";
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

        private void Select_books(object sender, EventArgs e)
        {
            PrintBooks();
        }

        private void AddBook(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintBooks();
                MessageBox.Show("Запись успешно добавлена в таблицу!");
            }
        }

        private void EditBook(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection selectedRowCollection = dataGridView1.SelectedRows;
                if (selectedRowCollection.Count == 0)
                {
                    MessageBox.Show("Не выбрана книга!");
                    return;
                }
                var selectedRow = selectedRowCollection[0];
                int Id = (int)selectedRow.Cells[0].Value;
                string Name = (string)selectedRow.Cells[1].Value;
                int Pages = (int)selectedRow.Cells[2].Value;
                int YearPress = (int)selectedRow.Cells[3].Value;
                string Theme = (string)selectedRow.Cells[4].Value;
                string Author = (string)selectedRow.Cells[5].Value;
                string Press = (string)selectedRow.Cells[6].Value;
                string Comment = (string)selectedRow.Cells[7].Value;
                int Quantity = (int)selectedRow.Cells[8].Value;
                Form2 form = new Form2(true, Id, Name, Author, Theme, Pages, Press, YearPress, Comment, Quantity);
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PrintBooks();
                    MessageBox.Show("Запись успешно обновлена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteBook(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRowCollection = dataGridView1.SelectedRows;
            if (selectedRowCollection.Count == 0)
            {
                MessageBox.Show("Не выбрана книга!");
                return;
            }
            DialogResult result = MessageBox.Show("Вы действительно желаете удалить книгу?", "Удаление книги", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
                return;
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                foreach (DataGridViewRow row in selectedRowCollection)
                {
                    int Id = (int)row.Cells[0].Value;
                    command.CommandText = $"DELETE FROM books WHERE id = {Id}";
                    int n = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
                PrintBooks();
                MessageBox.Show("Запись удалена!");
            }
        }
    }
}