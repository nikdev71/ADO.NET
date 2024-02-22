using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stock
{
    /// <summary>
    /// Interaction logic for EditProductType.xaml
    /// </summary>
    public partial class EditProductType : Window
    {
        bool Edit;
        string ID;
        public EditProductType(bool Edit = false, string ID = "0")
        {
            InitializeComponent();
            this.Edit = Edit;
            this.ID = ID;
            if (Edit)
            {
                Head.Text = "Edit product type with ID - " + ID;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connect = new SqlConnection(@"Initial Catalog=Stock;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
            SqlCommand command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                string producttype = TypePr.Text;
                if (Edit)
                {
                    command.CommandText = $"Update ProductType Set Type = '{producttype}' Where ID = {ID}";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "insert into ProductType (Type ) Values" +
                    $"('{producttype}')";
                    command.ExecuteNonQuery();
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
                command.Dispose();
            }
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
