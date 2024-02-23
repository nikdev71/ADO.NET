using System.Data.SqlClient;

namespace CRUD_operations
{
    public partial class Form2 : Form
    {
        bool Edit;
        int Id;

        public Form2(bool Edit = false, int Id = 0, string Name = "", string Author = "", string Theme = "", int Pages = 0,
                     string Press = "", int YearPress = 0, string Comment = "", int Quantity = 0)
        {
            InitializeComponent();
            this.Edit = Edit;
            this.Id = Id;
            if (!Edit)
            {
                Text = "Добавление книги";
                button1.Text = "Добавить";
                return;
            }
            Text = "Редактирование книги";
            button1.Text = "Изменить";
            NameText.Text = Name;
            AuthorText.Text = Author;
            ThemesText.Text = Theme;
            PressText.Text = Press;
            YearPressText.Text = YearPress.ToString();
            PagesText.Text = Pages.ToString();
            QuantityText.Text = Quantity.ToString();
            CommentText.Text = Comment;
        }

        private void Add_book(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Initial Catalog=Database_Books;Data Source=DESKTOP-G30VB0K\MSSQLSERVER01;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                string Name = NameText.Text;
                string Author = AuthorText.Text;
                string Theme = ThemesText.Text;
                string Press = PressText.Text;
                int YearPress = Convert.ToInt32(YearPressText.Text);
                int Pages = Convert.ToInt32(PagesText.Text);
                int Quantity = Convert.ToInt32(QuantityText.Text);
                string Comment = CommentText.Text;
                if(!Edit)
                {
                    command.CommandText = $"INSERT INTO books (name, pages, yearpress, themes, author, press, comment, quantity )" +
                                          $"VALUES ('{Name}', {Pages}, {YearPress}, '{Theme}', '{Author}', '{Press}', '{Comment}', {Quantity})";
                    int n = command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = $"UPDATE books SET name = '{Name}', pages = {Pages}, yearpress = {YearPress}, " +
                        $"themes = '{Theme}', author = '{Author}', press = '{Press}', comment = '{Comment}', " +
                        $"quantity = {Quantity} WHERE id = {Id}";
                    int n = command.ExecuteNonQuery();
                }
                DialogResult = DialogResult.OK;
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
