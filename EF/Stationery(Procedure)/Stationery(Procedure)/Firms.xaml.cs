using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Stationery
{
    /// <summary>
    /// Interaction logic for Firms.xaml
    /// </summary>
    public partial class Firms : Window
    {
        //SqlConnection? connect = null;
        //SqlCommand? cmd = null;
        //bool Edit;
        //int ID;
        public Firms(bool edit = false, int iD = 0)
        {
            InitializeComponent();
            //init();
            //Edit = edit;
            //ID = iD;
            //if(Edit) Head.Text = "Edit Firm";
        }
        //private async void init()
        //{
        //    connect = new SqlConnection(@"Initial Catalog=Stationery;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
        //    cmd = new SqlCommand();
        //    try
        //    {
        //        await connect.OpenAsync();
        //        cmd.Connection = connect;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        await connect.CloseAsync();
        //        await cmd.DisposeAsync();
        //    }
        //}
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (TitlePr.Text == "")
            //{
            //    MessageBox.Show("Fill the parameters");
            //    return;
            //}
            //try
            //{
            //    if (Edit)
            //    {
            //        cmd = new SqlCommand("UpdateFirms", connect);
            //        await connect.OpenAsync();
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("ID", ID);
            //        cmd.Parameters.AddWithValue("Title", TitlePr.Text);
            //        cmd.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        cmd = new SqlCommand("InsertIntoFirms", connect);
            //        await connect.OpenAsync();
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("Title", TitlePr.Text);
            //        cmd.ExecuteNonQuery();
            //    }
            //    DialogResult = true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    await connect.CloseAsync();
            //    await cmd.DisposeAsync();
            //}
            //Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
