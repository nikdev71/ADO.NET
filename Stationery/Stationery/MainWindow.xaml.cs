using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Stationery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection? connect =null;
        SqlCommand? cmd = null;
        Button? lastbtn = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            connect = new SqlConnection(@"Initial Catalog=Stationery;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
            cmd = new SqlCommand("ALL_Stationeries", connect);
            try
            {
                await connect.OpenAsync();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid1.ItemsSource = dt.DefaultView;
                await reader.CloseAsync();
                menu.IsEnabled = true;
                lastbtn = stat;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (connect != null && cmd != null)
            {
                menu.IsEnabled = false;
                DataGrid1.ItemsSource = null;
                await cmd.DisposeAsync();
                await connect.CloseAsync();
                connect= null;
                cmd= null;
            }
        }
        async void RequestProcedure(string procedure)
        {
            try
            {
                cmd = new SqlCommand(procedure, connect);
            
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid1.ItemsSource = dt.DefaultView;
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RequestProcedure("all_types");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RequestProcedure("ALL_Stationeries");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            RequestProcedure("all_managers");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            RequestProcedure("max_quantity");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            RequestProcedure("min_quantity");

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            RequestProcedure("max_cost");

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            RequestProcedure("min_cost");

        }

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd = new SqlCommand("cur_stationery", connect);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("type", tx1.Text);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid1.ItemsSource = dt.DefaultView;
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button_Click_10(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd = new SqlCommand("cur_manager", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("name", tx2.Text);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid1.ItemsSource = dt.DefaultView;
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button_Click_11(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd = new SqlCommand("cur_firm", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("title", tx3.Text);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid1.ItemsSource = dt.DefaultView;
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_firms(object sender, RoutedEventArgs e)
        {
            RequestProcedure("all_firms");

        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            RequestProcedure("earliest_sale");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            RequestProcedure("avg_stationery");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            RequestProcedure("top_manager");
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            RequestProcedure("top_manager2");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            RequestProcedure("top_firm");

        }

        private async void Button_Click_17(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd = new SqlCommand("top_manager3", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("StartDate", date1.Text);
                cmd.Parameters.AddWithValue("EndDate", date2.Text);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataGrid1.ItemsSource = dt.DefaultView;
                await reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            RequestProcedure("top_stationery");
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            RequestProcedure("top_stationery2");

        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            RequestProcedure("stationery_pop");

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditStationery form = new EditStationery();
            form.ShowDialog();
            RequestProcedure("ALL_Stationeries");

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;    
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            EditStationery form = new EditStationery(true, id);
            form.ShowDialog();
            RequestProcedure("ALL_Stationeries");
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lastbtn == stat)
            {
                if(DataGrid1.SelectedItem != null)
                {
                    editstat.IsEnabled = true;
                    deletestat.IsEnabled = true;
                }
                else
                {
                    editstat.IsEnabled = false;
                    deletestat.IsEnabled = false;
                }
            }
            if (lastbtn == typ)
            {
                if (DataGrid1.SelectedItem != null)
                {
                    edittyp.IsEnabled = true;
                    deletetyp.IsEnabled = true;
                }
                else
                {
                    edittyp.IsEnabled = false;
                    deletetyp.IsEnabled = false;
                }
            }
            if (lastbtn == man)
            {
                if (DataGrid1.SelectedItem != null)
                {
                    editman.IsEnabled = true;
                    deleteman.IsEnabled = true;
                }
                else
                {
                    editman.IsEnabled = false;
                    deleteman.IsEnabled = false;
                }
            }
            if (lastbtn == firm)
            {
                if (DataGrid1.SelectedItem != null)
                {
                    editfirm.IsEnabled = true;
                    deletefirm.IsEnabled = true;
                }
                else
                {
                    editfirm.IsEnabled = false;
                    deletefirm.IsEnabled = false;
                }
            }
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(connect == null || cmd  == null)
            {
                MessageBox.Show("Connect to DB");
                return;
            }
            lastbtn = (Button)sender;
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            RequestProcedure("days_without_sales");

        }
        private  void DeleteCommand(string procedure)
        {
            try
            {
                if (DataGrid1.SelectedItem == null) return;
                DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
                int id = Convert.ToInt32(selectedRow["ID"]);
                cmd = new SqlCommand(procedure, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("ID", id);
                SqlDataReader reader =  cmd.ExecuteReader();
                 reader.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void deletestat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this stationery?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand("delete_stationery");
                RequestProcedure("ALL_Stationeries");
                deletestat.IsEnabled = false;
                editstat.IsEnabled = false;
            }
        }

        private void deletetyp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this stationery?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand("delete_type");
                RequestProcedure("all_types");
                deletetyp.IsEnabled = false;
                edittyp.IsEnabled = false;
            }
        }

        private void deleteman_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this manager?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand("delete_manager");
                RequestProcedure("all_managers");
                deletetyp.IsEnabled = false;
                edittyp.IsEnabled = false;
            }
        }

        private void deletefirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this firm?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand("delete_firm");
                RequestProcedure("all_firms");
                deletetyp.IsEnabled = false;
                edittyp.IsEnabled = false;
            }
            
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            EditTypes form = new EditTypes();
            form.ShowDialog();
            RequestProcedure("all_types");
        }

        private void edittyp_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            EditTypes form = new EditTypes(true, id);
            form.ShowDialog();
            RequestProcedure("all_types");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Managers form = new Managers();
            form.ShowDialog();
            RequestProcedure("all_managers");
        }

        private void editman_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            Managers form = new Managers(true, id);
            form.ShowDialog();
            RequestProcedure("all_managers");
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Firms form = new Firms();
            form.ShowDialog();
            RequestProcedure("all_firms");
        }

        private void editfirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            Firms form = new Firms(true, id);
            form.ShowDialog();
            RequestProcedure("all_firms");
        }
    }
}