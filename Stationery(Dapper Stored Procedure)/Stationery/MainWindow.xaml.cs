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
using System.IO;
using Microsoft.Extensions.Configuration;
using Dapper;
using Stationery.Models;

namespace Stationery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button? lastbtn = null;
        public static string? connectionString;

        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        private void ExecuteQuery<T>(string sqlQuery)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var result = db.Query<T>(sqlQuery);
                    DataGrid1.ItemsSource = result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExecuteQueryProcedureWithoutParameters<T>(string proc)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var result = db.Query<T>(proc, commandType: CommandType.StoredProcedure);
                    DataGrid1.ItemsSource = result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExecuteStoredProcedure<T>(string procedureName, string parameterValue, string param)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add(param, parameterValue, dbType: DbType.String, direction: ParameterDirection.Input);
                var stats = db.Query<T>( procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
                DataGrid1.ItemsSource = stats;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExecuteQuery<TypeVM>("select Id, Title from TypesOfStationery");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ExecuteQuery<StationeryVM>("select s.Id, s.Title , s.Quantity, s.Cost , ts.Title as [Type] from Stationery as s join TypesOfStationery as ts on s.TypeId = ts.Id");
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ExecuteQuery<ManagerVM>("select Id, Name from Managers");
        }
        private void Button_Click_firms(object sender, RoutedEventArgs e)
        {
            ExecuteQuery<FirmVM>("select Id, Title from Firms");
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ExecuteQueryProcedureWithoutParameters<StationeryVM>("max_quantity");
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ExecuteQueryProcedureWithoutParameters<StationeryVM>("min_quantity");

        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ExecuteQueryProcedureWithoutParameters<StationeryVM>("max_cost");
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ExecuteQueryProcedureWithoutParameters<StationeryVM>("min_cost");
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ExecuteStoredProcedure<StationeryVM>("cur_stationery_dap", tx1.Text, "@name");
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ExecuteStoredProcedure<ManagerSales>("cur_manager", tx2.Text, "@name");
        }
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            ExecuteStoredProcedure<FirmSales>("cur_firm", tx3.Text, "@title");
        }
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            ExecuteQueryProcedureWithoutParameters<Earliest>("earliest_sale");
        }
        //Удаление
        private void DeleteCommand<T>(string procedure) where T : class, IHasId
        {
            try
            {
                if (DataGrid1.SelectedItem == null) return;
                T selectedRow = (T)DataGrid1.SelectedItem;
                int id = selectedRow.Id;
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    int n = db.Execute(procedure, dynamicParams, commandType: CommandType.StoredProcedure);
                    MessageBox.Show("Количество удаленных записей: " + n.ToString());
                }
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
                DeleteCommand<StationeryVM>("delete_stationery");
                ExecuteQuery<StationeryVM>("select s.Id, s.Title , s.Quantity, s.Cost , ts.Title as [Type] from Stationery as s join TypesOfStationery as ts on s.TypeId = ts.Id");
                deletestat.IsEnabled = false;
                editstat.IsEnabled = false;
            }
        }

        private void deletetyp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this stationery?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand<TypeVM>("delete_type");
                ExecuteQuery<TypeVM>("select Id, Title from TypesOfStationery");
                deletetyp.IsEnabled = false;
                edittyp.IsEnabled = false;
            }
        }

        private void deleteman_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this manager?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand<ManagerVM>("delete_manager");
                ExecuteQuery<ManagerVM>("select Id, Name from Managers");
                deletetyp.IsEnabled = false;
                edittyp.IsEnabled = false;
            }
        }

        private void deletefirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this firm?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteCommand<FirmVM>("delete_firm");
                ExecuteQuery<FirmVM>("select Id, Title from Firms");
                deletetyp.IsEnabled = false;
                edittyp.IsEnabled = false;
            }
            
        }
        //Канцтовары
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditStationery form = new EditStationery();
            form.ShowDialog();
            ExecuteQuery<StationeryVM>("select s.Id, s.Title , s.Quantity, s.Cost , ts.Title as [Type] from Stationery as s join TypesOfStationery as ts on s.TypeId = ts.Id");
            lastbtn = stat;
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            StationeryVM selectedStationery = (StationeryVM)DataGrid1.SelectedItem;
            int id = selectedStationery.Id;
            EditStationery form = new EditStationery(true, id);
            form.ShowDialog();
            ExecuteQuery<StationeryVM>("select s.Id, s.Title , s.Quantity, s.Cost , ts.Title as [Type] from Stationery as s join TypesOfStationery as ts on s.TypeId = ts.Id");
            deletestat.IsEnabled = false;
            editstat.IsEnabled = false;
        }
        // Тип канцтоваров
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            EditTypes form = new EditTypes();
            form.ShowDialog();
            ExecuteQuery<TypeVM>("select Id, Title from TypesOfStationery");
            lastbtn = typ;
        }

        private void edittyp_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            TypeVM selectedRow = (TypeVM)DataGrid1.SelectedItem;
            int id = selectedRow.Id;
            EditTypes form = new EditTypes(true, id);
            form.ShowDialog();
            ExecuteQuery<TypeVM>("select Id, Title from TypesOfStationery");
            deletetyp.IsEnabled = false;
            edittyp.IsEnabled = false;
        }
        //Менеджерры
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Managers form = new Managers();
            form.ShowDialog();
            ExecuteQuery<ManagerVM>("select Id, Name from Managers");
            lastbtn = man;
        }

        private void editman_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            ManagerVM selectedRow = (ManagerVM)DataGrid1.SelectedItem;
            int id = selectedRow.Id;
            Managers form = new Managers(true, id);
            form.ShowDialog(); 
            ExecuteQuery<ManagerVM>("select Id, Name from Managers");
            deleteman.IsEnabled = false;
            editman.IsEnabled = false;
        }
        //Фирмы
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Firms form = new Firms();
            form.ShowDialog();
            ExecuteQuery<FirmVM>("select Id, Title from Firms");
            lastbtn = firm;
        }

        private void editfirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            FirmVM selectedRow = (FirmVM)DataGrid1.SelectedItem;
            int id = selectedRow.Id;
            Firms form = new Firms(true, id);
            form.ShowDialog();
            ExecuteQuery<FirmVM>("select Id, Title from Firms");
            deletefirm.IsEnabled = false;
            editfirm.IsEnabled = false;
        }
        //Кнопки
        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lastbtn == stat)
            {
                if (DataGrid1.SelectedItem != null)
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
            lastbtn = (Button)sender;
        }
    }
}