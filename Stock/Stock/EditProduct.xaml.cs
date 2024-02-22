using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;

namespace Stock
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        bool Edit;
        string ID;
        public EditProduct(bool Edit = false, string ID = "0")
        {
            InitializeComponent();
            this.Edit = Edit;
            this.ID = ID;
            if(Edit)
            {
                Head.Text = "Edit product with ID - "+ ID;
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
                string title = TitlePr.Text;
                int typeId = Convert.ToInt32(TypePr.Text);
                int cost = Convert.ToInt32(CostPr.Text);
                int quantity = Convert.ToInt32(QuantityPr.Text);
                if (Edit)
                {
                    command.CommandText = $"Update Products Set ProductTitle = '{title}', TypeId = {typeId}, Cost = {cost}, Quantity = {quantity} Where ID = {ID}";  
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "insert into Products (ProductTitle, TypeID, Cost, Quantity ) Values" +
                    $"('{title}',{typeId}, {cost},{quantity})";
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
        private void TypePr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9]+");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
