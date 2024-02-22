using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Stationery
{
    /// <summary>
    /// Interaction logic for Stationery.xaml
    /// </summary>
    public partial class EditStationery : Window
    {
        SqlConnection? connect = null;
        SqlCommand? cmd= null;
        bool Edit ;
        int ID ;
        public EditStationery(bool edit=false, int iD=0)
        {
            InitializeComponent();
            init();
            Edit = edit;
            ID = iD;
            if (Edit) Head.Text = "Edit Stationery";

        }
        private async void init()
        {
            connect = new SqlConnection(@"Initial Catalog=Stationery;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
            cmd = new SqlCommand();
            try
            {
                await connect.OpenAsync();
                cmd.Connection = connect;
                cmd.CommandText = "select ID from TypesOfStationery";
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    int id = reader.GetInt32(0); 
                    cb1.Items.Add(id); 
                    if(cb1.Items.Count > 0 ) cb1.SelectedIndex = 0;
                }
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await connect.CloseAsync();
                await cmd.DisposeAsync();
            }
        }
        private void TypePr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9]+");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TitlePr.Text == "" || QuantityPr.Text == "" || CostPr.Text == "")
            {
                MessageBox.Show("Fill the parameters");
                return;
            }
            try
            {
                int typeId = Convert.ToInt32(cb1.SelectedValue);
                int cost = Convert.ToInt32(CostPr.Text);
                int quantity = Convert.ToInt32(QuantityPr.Text);
                if (Edit)
                {
                    cmd = new SqlCommand("UpdateStationery", connect);
                    await connect.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("Title", TitlePr.Text);
                    cmd.Parameters.AddWithValue("Quantity", quantity);
                    cmd.Parameters.AddWithValue("Cost", cost);
                    cmd.Parameters.AddWithValue("TypeID", typeId);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("InsertIntoStationery", connect);
                    await connect.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Title", TitlePr.Text);
                    cmd.Parameters.AddWithValue("Quantity", quantity);
                    cmd.Parameters.AddWithValue("Cost", cost);
                    cmd.Parameters.AddWithValue("TypeID", typeId);
                    cmd.ExecuteNonQuery();
                }
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await connect.CloseAsync();
                await cmd.DisposeAsync();
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
