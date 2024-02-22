using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FruitsAndVegeyables
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connect;
        SqlCommand command;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectionToServer()
        {
            connect = new SqlConnection(@"Initial Catalog=FruitsAndVegetables;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
            command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                //command.CommandText = "SELECT * FROM Products";
                //SqlDataReader reader = command.ExecuteReader();
                //DataTable dt = new DataTable();
                //dt.Load(reader);
                //DataGrid1.ItemsSource = dt.DefaultView;
                //reader.Close();
                command.CommandText = "SELECT Title, Type, Color ,Cal  FROM Products";
                SqlDataReader reader = command.ExecuteReader();
                int count = reader.FieldCount;
                ls1.DataContext = null;
                ls1.Items.Clear();
                while (reader.Read())
                {
                    string? res = "", temp = "";
                    for (int i = 0; i < count; i++)
                    {
                        temp = reader[i].ToString();
                        res += temp + "  ";
                    }
                    ls1.Items.Add(res);
                    res = "";
                }
                reader.Close();
                MessageBox.Show("Connection is succesfull");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionToServer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //DataGrid1.ItemsSource = null;
            ls1.Items.Clear();
            if (connect != null && command != null)
            {
                command.Dispose();
                connect.Close();
                command = null;
                connect = null;
            }
            MessageBox.Show("Disconnected from databse");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            command.CommandText = "SELECT Title, Type, Color ,Cal  FROM Products";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
           
            command.CommandText = "SELECT Title, Type  FROM Products";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            
            command.CommandText = "SELECT Title, Color  FROM Products";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
            command.CommandText = "SELECT MAX(Cal), Title FROM Products Group By Title";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close(); 
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            
            command.CommandText = "SELECT MIN(Cal), Title  FROM Products Group By Title ";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
          
            command.CommandText = "SELECT AVG(Cal), Type FROM Products Group by Type ";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
           
            command.CommandText = "SELECT Count(Type) FROM Products Where Type='Овощ' ";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
           
            command.CommandText = "SELECT Count(Type) FROM Products Where Type='Фрукт' ";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
           
            string curcolor = CurColor.Text.ToString();
            command.CommandText = "SELECT Count(Title) FROM Products Where Color = '"+curcolor+"'";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            
            command.CommandText = "SELECT Count(Title), Color FROM Products Group by Color ";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            string curcal = CurСal.Text.ToString();
            if (curcal == "") return;
            command.CommandText = "SELECT Title, Cal FROM Products Where Cal < " + curcal;
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            
            string curcal = CurСal2.Text.ToString();
            if (curcal == "") return;
            command.CommandText = "SELECT Title, Cal FROM Products Where Cal > " + curcal;
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void CurСal2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            
            string curcal = CurСal3.Text.ToString();
            string curcal2 = CurСal4.Text.ToString();
            if (curcal == "" || curcal2=="") return;
            command.CommandText = "SELECT Title, Cal FROM Products Where Cal between " + curcal+" and "+ curcal2;
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
           
            command.CommandText = "SELECT Title, Color FROM Products Where Color = 'Красный' or Color = 'Желтый'";
            SqlDataReader reader = command.ExecuteReader();
            int count = reader.FieldCount;
            ls1.DataContext = null;
            ls1.Items.Clear();
            while (reader.Read())
            {
                string? res = "", temp = "";
                for (int i = 0; i < count; i++)
                {
                    temp = reader[i].ToString();
                    res += temp + "  ";
                }
                ls1.Items.Add(res);
                res = "";
            }
            reader.Close();
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (connect == null && command == null)
            {
                MessageBox.Show("Please connect to database");
                return;
            }
        }
    }
}