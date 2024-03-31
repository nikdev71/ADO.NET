using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Stationery;
using System;
using System.Data;
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

namespace DbFirst_Stationery_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //Microsoft.EntityFramework.SqlClient;
        //Microsoft.EntityFramework.Tools;
        //Scaffold-DbContext "Server=DESKTOP-VDC9A9A;Database=Stationery;Integrated Security=SSPI;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer
        Button? lastbtn = null;
        public MainWindow()
        {
            InitializeComponent();
        }        
        //Добавление
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditStationery form = new EditStationery();
            form.ShowDialog();
            AllStationery();
            lastbtn = stat;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            EditTypes form = new EditTypes();
            form.ShowDialog();
            AllTypes();
            lastbtn = typ;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Managers form = new Managers();
            form.ShowDialog();
            AllManagers();
            lastbtn = man;
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Firms form = new Firms();
            form.ShowDialog();
            AllFirms();
            lastbtn = firm;
        }

        //Редактирование
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            var selectedObject = DataGrid1.SelectedItem;
            int id = (int)selectedObject.GetType().GetProperty("Id")!.GetValue(selectedObject)!;
            string title = (string)selectedObject.GetType().GetProperty("Title")!.GetValue(selectedObject)!;
            int quantity = (int)selectedObject.GetType().GetProperty("Quantity")!.GetValue(selectedObject)!;
            int cost = (int)selectedObject.GetType().GetProperty("Cost")!.GetValue(selectedObject)!;
            string type = (string)selectedObject.GetType().GetProperty("Type")!.GetValue(selectedObject)!;
            EditStationery form = new EditStationery(true, id,title,quantity,cost,type);
            form.ShowDialog();
            AllStationery();
            deletestat.IsEnabled = false;
            editstat.IsEnabled = false;
        }

        private void edittyp_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            var selectedObject = DataGrid1.SelectedItem;
            int id = (int)selectedObject.GetType().GetProperty("Id")!.GetValue(selectedObject)!;
            string title = (string)selectedObject.GetType().GetProperty("Title")!.GetValue(selectedObject)!;
            EditTypes form = new EditTypes(true, id, title);
            form.ShowDialog();
            AllTypes();
            deletetyp.IsEnabled = false;
            edittyp.IsEnabled = false;
        }

        private void editman_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            var selectedObject = DataGrid1.SelectedItem;
            int id = (int)selectedObject.GetType().GetProperty("Id")!.GetValue(selectedObject)!;
            string title = (string)selectedObject.GetType().GetProperty("Name")!.GetValue(selectedObject)!;
            Managers form = new Managers(true, id, title);
            form.ShowDialog();
            AllManagers();
            deleteman.IsEnabled = false;
            editman.IsEnabled = false;
        }

        private void editfirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            var selectedObject = DataGrid1.SelectedItem;
            int id = (int)selectedObject.GetType().GetProperty("Id")!.GetValue(selectedObject)!;
            string title = (string)selectedObject.GetType().GetProperty("Title")!.GetValue(selectedObject)!;
            Firms form = new Firms(true, id, title);
            form.ShowDialog();
            AllFirms();
            deletefirm.IsEnabled = false;
            editfirm.IsEnabled = false;
        }

        //Удаление
        private void deletestat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this stationery?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteObj("delete_stationery");
            }
            AllStationery();
            deletestat.IsEnabled = false;
            editstat.IsEnabled = false;
        }

        private void deletetyp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this type?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteObj("delete_type");
            }
            AllTypes();
            deletetyp.IsEnabled = false;
            edittyp.IsEnabled = false;
        }

        private void deleteman_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this manager?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteObj("delete_manager");
            }
            AllManagers();
            deleteman.IsEnabled = false;
            editman.IsEnabled = false;
        }

        private void deletefirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this firm?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DeleteObj("delete_firm");
            }
            AllFirms();
            deleteman.IsEnabled = false;
            editman.IsEnabled = false;
        }

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


        void DeleteObj(string proc)
        {
            try
            {
                if (DataGrid1.SelectedItem == null) return;
                var selectedObject = DataGrid1.SelectedItem;
                int id = (int)selectedObject.GetType().GetProperty("Id")!.GetValue(selectedObject)!;
                using (StationeryContext db = new StationeryContext())
                {
                    SqlParameter param = new()
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Value = id
                    };
                    var numberOfRowDeleted = db.Database.ExecuteSqlRaw($"{proc} @Id", param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void AllStationery()
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var stationery = db.Stationeries.FromSqlRaw("select * from Stationery").Select(s => new
                    {
                        s.Id,
                        s.Title,
                        s.Quantity,
                        s.Cost,
                        Type = s.Type != null ? s.Type.Title : ""
                    }).ToList();

                    DataGrid1.ItemsSource = stationery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void AllTypes()
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var stationery = db.TypesOfStationeries.FromSqlRaw("select * from TypesOfStationery").Select(t => new { t.Id, t.Title }).ToList();
                    DataGrid1.ItemsSource = stationery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void AllManagers()
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var stationery = db.Managers.FromSqlRaw("select * from Managers").Select(t => new { t.Id, t.Name }).ToList();
                    DataGrid1.ItemsSource = stationery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void AllFirms()
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var stationery = db.Firms.FromSqlRaw("select * from Firms").Select(t => new { t.Id, t.Title }).ToList();
                    DataGrid1.ItemsSource = stationery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Запросы
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AllStationery();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           AllTypes();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AllManagers();
        }

        private void Button_Click_firms(object sender, RoutedEventArgs e)
        {
            AllFirms();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                   
                    SqlParameter param = new()
                    {
                        ParameterName = "@sum",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    db.Database.ExecuteSqlRaw("max_quantity_output @sum out",param);
                    int sum = (int)param.Value;

                    var stationery = db.Stationeries
                            .Where(s => s.Quantity == sum).Select(st=> new {st.Id,st.Title,st.Quantity, st.Cost, Type = st.Type != null ? st.Type.Title : ""})
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter minQuantityParam = new SqlParameter("@minQuantity", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("min_quantity_output @minQuantity out", minQuantityParam);

                    int minQuantity = (int)minQuantityParam.Value;

                    var stationery = db.Stationeries
                            .Where(s => s.Quantity == minQuantity).Select(st => new { st.Id, st.Title, st.Quantity, st.Cost, Type = st.Type != null ? st.Type.Title : "" })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter maxCostParam = new SqlParameter("@maxCost", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output,
                    };

                    db.Database.ExecuteSqlRaw("max_cost_output @maxCost out", maxCostParam);

                    decimal maxCost = (decimal)maxCostParam.Value;

                    var stationery = db.Stationeries
                            .Where(s => s.Cost == maxCost).Select(st => new { st.Id, st.Title, st.Quantity, st.Cost, Type = st.Type != null ? st.Type.Title : "" })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter minCostParam = new SqlParameter("@minCost", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output,
                    };

                    db.Database.ExecuteSqlRaw("min_cost_output @minCost out", minCostParam);

                    decimal minCost = (decimal)minCostParam.Value;


                    var stationery = db.Stationeries
                            .Where(s => s.Cost == minCost).Select(st => new { st.Id, st.Title, st.Quantity,st.Cost, Type = st.Type != null ? st.Type.Title : "" })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter stationeryNameParam = new SqlParameter("@StationeryName", SqlDbType.NVarChar, 100)
                    {
                        Value = tx1.Text
                    };

                    SqlParameter stationeryIdParam = new SqlParameter("@StationeryId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("find_stationery_by_name @StationeryName, @StationeryId OUT", stationeryNameParam, stationeryIdParam);

                    int stationeryId = (int)stationeryIdParam.Value;


                    var stationery = db.Stationeries
                            .Where(s => s.Id == stationeryId).Select(st => new { st.Id, st.Title, st.Quantity, st.Cost, Type = st.Type != null ? st.Type.Title : "" })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter managerNameParam = new SqlParameter("@ManagerName", SqlDbType.NVarChar, 100)
                    {
                        Value = tx2.Text
                    };

                    SqlParameter managerIdParam = new SqlParameter("@ManagerId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("find_manager_by_name @ManagerName, @ManagerId OUT", managerNameParam, managerIdParam);

                    int managerId = (int)managerIdParam.Value;

                    var stationery = db.Managers
                            .Where(s => s.Id == managerId).Select(st => new { st.Id, st.Name })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter firmNameParam = new SqlParameter("@FirmName", SqlDbType.NVarChar, 100)
                    {
                        Value = tx3.Text
                    };

                    SqlParameter firmIdParam = new SqlParameter("@FirmId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("find_firm_by_name @FirmName, @FirmId OUT", firmNameParam, firmIdParam);

                    int firmId = (int)firmIdParam.Value;

                    var stationery = db.Firms
                            .Where(s => s.Id == firmId).Select(st => new { st.Id, st.Title })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter earliestSaleIdParam = new SqlParameter("@EarliestSaleId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("earliest_sale_output @EarliestSaleId OUT", earliestSaleIdParam);

                    int earliestSaleId = (int)earliestSaleIdParam.Value;

                    var stationery = db.Sales
                            .Where(s => s.Id == earliestSaleId).Select(st => new 
                            {  
                                st.Id, 
                                st.DateOfSale,
                                st.PricePerUnitSold,
                                st.Quantity,
                                Manager = st.Manager!=null ? st.Manager.Name : "",
                                Firm = st.Firm != null ? st.Firm.Title : "",
                                Stationery = st.Stationery != null ? st.Stationery.Title : ""
                            })
                            .ToList();

                    DataGrid1.ItemsSource = stationery;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var results = db.Stationeries.FromSqlRaw("Select * from Stationery")
                        .Join(db.TypesOfStationeries, st=>st.TypeId, t=>t.Id, (st, t) => new { st, t })
                        .GroupBy(o=>o.t.Title)
                        .Select(x => new { Quantity = x.Key, Count = x.Average(o => o.st.Quantity) });

                    DataGrid1.ItemsSource = results.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter totalSoldParam = new SqlParameter("@TotalSold", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter managerParam = new SqlParameter("@Manager", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("EXEC top_manager_output @TotalSold OUT, @Manager OUT", totalSoldParam, managerParam);

                    int totalSold = (int)totalSoldParam.Value;
                    string manager = (string)managerParam.Value;

                    var resultList = new[]
                    {
                        new { TotalSold = totalSold, Manager = manager }
                    };

                    DataGrid1.ItemsSource = resultList;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    SqlParameter typeTitleParam = new SqlParameter("@TypeTitle", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter totalQuantitySoldParam = new SqlParameter("@TotalQuantitySold", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    db.Database.ExecuteSqlRaw("EXEC top_stationery_output @TypeTitle OUT, @TotalQuantitySold OUT", typeTitleParam, totalQuantitySoldParam);

                    string typeTitle = (string)typeTitleParam.Value;
                    int totalQuantitySold = (int)totalQuantitySoldParam.Value;

                    var resultList = new[]
                    {
                        new { TypeTitle = typeTitle, TotalQuantitySold = totalQuantitySold }
                    };

                    DataGrid1.ItemsSource = resultList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var results = db.Sales.FromSqlRaw("Select * from Sales")
                         .Join(db.Stationeries, s => s.StationeryId, st => st.Id, (s, st) => new { s, st })
                         .Join(db.TypesOfStationeries,
                               sts => sts.st.TypeId,
                               ts => ts.Id,
                               (sts, ts) => new { sts, ts })
                         .GroupBy(x => x.ts.Title)
                         .OrderByDescending(g => g.Sum(x => x.sts.s.Quantity * x.sts.s.PricePerUnitSold))
                         .Select(g => new
                         {
                             TypeTitle = g.Key,
                             TotalProfit = g.Sum(x => x.sts.s.Quantity * x.sts.s.PricePerUnitSold)
                         });

                    DataGrid1.ItemsSource = results.ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            using (StationeryContext db = new StationeryContext())
            {
                try
                {
                    var results = db.Sales
                        .Join(db.Stationeries, sa => sa.StationeryId, s => s.Id, (sa, s) => new { sa, s })
                        .GroupBy(x => x.s.Title)
                        .OrderByDescending(g => g.Sum(x => x.sa.Quantity))
                        .Select(g => new
                        {
                            ProductTitle = g.Key,
                            TotalQuantitySold = g.Sum(x => x.sa.Quantity)
                        });

                    DataGrid1.ItemsSource = results.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            lastbtn = (Button)sender;
        }
    }
}