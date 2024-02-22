using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.Configuration.Json;

namespace Stock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlCommand command;
        Button button;
        bool flag = false;

        DataSet dataset = new DataSet();
        SqlConnection connect;
        SqlDataAdapter adapter1, adapter2, adapter3,adapter4;
        SqlCommandBuilder build1, build2, build3;

        string ProductTitleTemp, ProductTypeTemp, SupplierNameTemp;
        int ProductTypeIDTemp, ProductCostTemp, ProductQuantityTemp, ProductIDTemp;
        string DeliveryDateTemp;
        public MainWindow()
        {
            InitializeComponent();
            button = btnAllinfo;

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            connect = new SqlConnection(connectionString);
        }
        //void ConnectionToServer()
        //{
        //    command = new SqlCommand();
        //    try
        //    {
        //        Create_Tables();
        //        menu.IsEnabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void Create_Tables()
        {
            try
            {
                adapter2 = new SqlDataAdapter("select * from ProductType", connect);
                build2 = new SqlCommandBuilder(adapter2);

                DataTable ProductType = dataset.Tables.Add("ProductType");
                ProductType.Columns.Add("ID", typeof(int));
                ProductType.Columns.Add("Type", typeof(string));
                ProductType.Constraints.Add("PK_ProductType", ProductType.Columns["ID"], true);
                ProductType.Columns["ID"].AutoIncrement = true;
                ProductType.Columns["ID"].AllowDBNull = false;
                ProductType.Columns["Type"].AllowDBNull = false;

                adapter2.Fill(dataset, "ProductType");


                adapter1 = new SqlDataAdapter("select * from Products", connect);
                build1 = new SqlCommandBuilder(adapter1);

                DataTable Products = dataset.Tables.Add("Products");

                Products.Columns.Add("ID", typeof(int));
                Products.Columns.Add("ProductTitle", typeof(string));
                Products.Columns.Add("TypeID", typeof(int));
                Products.Columns.Add("Cost", typeof(decimal));
                Products.Columns.Add("Quantity", typeof(int));
                Products.Constraints.Add("PK_Products", Products.Columns["ID"], true);
                Products.Columns["ID"].AutoIncrement = true;
                Products.Columns["ID"].AllowDBNull = false;
                Products.Columns["ProductTitle"].AllowDBNull = false;
                Products.Columns["Cost"].AllowDBNull = false;
                Products.Columns["Quantity"].AllowDBNull = false;
                ForeignKeyConstraint FK_Type = new ForeignKeyConstraint("FK_type", ProductType.Columns["ID"], Products.Columns["TypeID"]);
                FK_Type.DeleteRule = Rule.Cascade;
                FK_Type.UpdateRule = Rule.Cascade;
                adapter1.Fill(dataset, "Products");
                DataGrid1.ItemsSource = Products.DefaultView;



                adapter3 = new SqlDataAdapter("select * from Suppliers", connect);
                build3 = new SqlCommandBuilder(adapter3);

                DataTable Suppliers = dataset.Tables.Add("Suppliers");
                Suppliers.Columns.Add("ID", typeof(int));
                Suppliers.Columns.Add("SupplierName", typeof(string));
                Suppliers.Columns.Add("ProductID", typeof(int));
                Suppliers.Columns.Add("DeliveryDate", typeof(DateTime));
                Suppliers.Constraints.Add("PK_Suppliers", Suppliers.Columns["ID"], true);
                Suppliers.Columns["ID"].AutoIncrement = true;
                Suppliers.Columns["ID"].AllowDBNull = false;
                Suppliers.Columns["ProductID"].AllowDBNull = false;
                ForeignKeyConstraint FK_Customers =
                    new ForeignKeyConstraint("FK_Customers", Products.Columns["ID"], Suppliers.Columns["ProductID"]);
                FK_Customers.DeleteRule = Rule.Cascade;
                FK_Customers.UpdateRule = Rule.Cascade;
                Suppliers.Constraints.Add(FK_Customers);
                Suppliers.Columns["SupplierName"].AllowDBNull = false;
                Suppliers.Columns["DeliveryDate"].AllowDBNull = false;

                adapter3.Fill(dataset, "Suppliers");
                flag = true;
                menu.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Create_Tables();
            Button btn = (Button)sender;
            btn.IsEnabled = false;
        }
        private void SelectFromDB(string select)
        {
            command.CommandText = select;
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DataGrid1.ItemsSource = dt.DefaultView;
            reader.Close();
        }
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!flag)
            {
                MessageBox.Show("Please create tables");
                return;
            }
            button = (Button)sender;
        }
        void PrintTable(string tableName)
        {
            try
            {
                DataTable productsTable = dataset.Tables[tableName];
                DataGrid1.ItemsSource = productsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void MinMax(string func, string param)
        {
            try
            {
                int res = Convert.ToInt32(dataset.Tables["Products"].Compute(func + "(" + param + ")", string.Empty));

                DataRow[] rows = dataset.Tables["Products"].Select(param + " = " + res);

                DataTable resultTable = dataset.Tables["Products"].Clone();
                foreach (DataRow row in rows)
                {
                    resultTable.ImportRow(row);
                }

                DataGrid1.ItemsSource = resultTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReceiveParametersForProduct(string param1, int param2, int param3, int param4)
        {
            ProductTitleTemp = param1;
            ProductTypeIDTemp = param2;
            ProductCostTemp = param3;
            ProductQuantityTemp = param4;
        }
        public void ReceiveParametersForProductType(string type)
        {
            ProductTypeTemp = type;
        }
        public void ReceiveParametersForSupplier(string name, int ProductId, string date)
        {
            SupplierNameTemp = name;
            ProductIDTemp = ProductId;
            DeliveryDateTemp = date;
        }
        Dictionary<int, string> Types()
        {
            Dictionary<int, string> typeDictionary = new Dictionary<int, string>();
            var types = dataset.Tables["ProductType"]
                            .AsEnumerable()
                            .Select(row => new
                            {
                                ID = row.Field<int>("ID"),
                                Type = row.Field<string>("Type")
                            });

            foreach (var type in types)
            {
                typeDictionary.Add(type.ID, type.Type);
            }
            return typeDictionary;
        }
        Dictionary<int, string> ProductsPair()
        {
            Dictionary<int, string> ProductsDictionary = new Dictionary<int, string>();
            var types = dataset.Tables["Products"]
                            .AsEnumerable()
                            .Select(row => new
                            {
                                ID = row.Field<int>("ID"),
                                ProductTitle = row.Field<string>("ProductTitle")
                            });

            foreach (var type in types)
            {
                ProductsDictionary.Add(type.ID, type.ProductTitle);
            }
            return ProductsDictionary;
        }
        void MultiTableDataAdapter(string cmd, string? value)
        {
            try
            {
                string str = cmd;

                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand(str, connect);
                if(value != null) command.Parameters.AddWithValue("@Type", value);

                adapter.SelectCommand = command;

                DataSet dataset = new DataSet();

                adapter.Fill(dataset, "Products");

                DataGrid1.ItemsSource = dataset.Tables["Products"].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DeleteFunc(string tablename)
        {
            int id = DataGrid1.SelectedIndex;
            dataset.Tables[tablename].Rows[id].Delete();
            DataGrid1.ItemsSource = dataset.Tables[tablename].DefaultView;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PrintTable("Products");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PrintTable("ProductType");
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PrintTable("Suppliers");
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            MinMax("MAX", "Quantity");
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MinMax("MIN", "Quantity");
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            MinMax("MIN", "Cost");
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            MinMax("MAX", "Cost");
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string type = givencat.Text.ToString();
            if (type == null) return;
            MultiTableDataAdapter("SELECT Products.ID, ProductTitle, pt.Type, Quantity, Cost " +
                             "FROM Products " +
                             "JOIN ProductType AS pt ON pt.ID = TypeID " +
                             "WHERE pt.Type = @Type", type);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            string supp = supplier.Text;
            if (supp == null) return;
            MultiTableDataAdapter("Select Products.ID, ProductTitle, pt.Type, Cost, Quantity, SupplierName" +
                    " From Products" +
                    " join Suppliers as s on s.ProductID = Products.ID " +
                    " join ProductType as pt on pt.ID = TypeID" +
                    " where SupplierName = @Type", supp);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            
            MultiTableDataAdapter("Select pt.Type, AVG(Quantity) as Average_Count From Products join ProductType as pt on pt.ID = TypeID Group By pt.Type", null);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            MultiTableDataAdapter("Select Products.ID, ProductTitle, pt.Type, Cost, Quantity, DeliveryDate\r\nFrom Products\r\njoin Suppliers as s on s.ProductID = Products.ID join ProductType as pt on pt.ID = TypeID\r\nwhere DeliveryDate = (\r\nSelect MIN(DeliveryDate) From Suppliers)", null);
        }
        private void ls1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (button == btnAllinfo || button == btnMAXQ || button == btnMINQ || button == btnMAXC || button == btnMINC || button == btnOld || button == btnPrCat || button == btnCurSupp)
            {
                if (DataGrid1.SelectedItem != null)
                {
                    deleteproduct.IsEnabled = true;
                    editproduct.IsEnabled = true;
                }
                else
                {
                    deleteproduct.IsEnabled = false;
                    editproduct.IsEnabled = false;
                }
            }
            else
            {
                deleteproduct.IsEnabled = false;
                editproduct.IsEnabled = false;
            }

            if (button == btnAlltypes)
            {
                if (DataGrid1.SelectedItem != null)
                {
                    edittypes.IsEnabled = true;
                    deletetypes.IsEnabled = true;
                }
                else
                {
                    edittypes.IsEnabled = false;
                    deletetypes.IsEnabled = false;
                }
            }
            else
            {
                edittypes.IsEnabled = false;
                deletetypes.IsEnabled = false;
            }

            if (button == btnAllSupp)
            {
                if (DataGrid1.SelectedItem != null)
                {
                    editsupp.IsEnabled = true;
                    deletesupp.IsEnabled = true;
                }
                else
                {
                    editsupp.IsEnabled = false;
                    deletesupp.IsEnabled = false;
                }
            }
            else
            {
                editsupp.IsEnabled = false;
                deletesupp.IsEnabled = false;
            }
        }

        #region Product
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> typeDictionary = Types();
            EditProduct form = new EditProduct(typeDictionary);
            form.Owner = this;
            bool? res = form.ShowDialog();
            if (res == true)
            {
                DataRow row = dataset.Tables["Products"].NewRow();
                row["ProductTitle"] = ProductTitleTemp;
                row["TypeID"] = ProductTypeIDTemp;
                row["Cost"] = ProductCostTemp;
                row["Quantity"] = ProductQuantityTemp;
                dataset.Tables["Products"].Rows.Add(row);
                DataGrid1.ItemsSource = dataset.Tables["Products"].DefaultView;

                adapter1.Update(dataset, "Products");
                MessageBox.Show("Product addded");
            }


        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            string title = selectedRow["ProductTitle"].ToString();
            int typeid = Convert.ToInt32(selectedRow["TypeID"]);
            int cost = Convert.ToInt32(selectedRow["Cost"]);
            int quan = Convert.ToInt32(selectedRow["Quantity"]);
            Dictionary<int, string> typeDictionary = Types();
            EditProduct form = new EditProduct(typeDictionary,typeid,title,cost,quan ,true, id);
            form.Owner = this;
            bool? res = form.ShowDialog();
            if (res == true)
            {
                try
                {
                    DataRow[] row = dataset.Tables["Products"].Select("ID = " + id);
                    row[0]["ProductTitle"] = ProductTitleTemp;
                    row[0]["TypeID"] = ProductTypeIDTemp;
                    row[0]["Cost"] = ProductCostTemp;
                    row[0]["Quantity"] = ProductQuantityTemp;
                    DataGrid1.ItemsSource = dataset.Tables["Products"].DefaultView;

                    adapter1.Update(dataset, "Products");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DeleteFunc("Products");
            adapter1.Update(dataset, "Products");
        }
        #endregion

        #region ProductType
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            EditProductType form = new EditProductType();
            form.Owner = this;
            bool? res = form.ShowDialog();
            if (res == true)
            {
                DataRow row = dataset.Tables["ProductType"].NewRow();
                row["Type"] = ProductTypeTemp;
                dataset.Tables["ProductType"].Rows.Add(row);
                DataGrid1.ItemsSource = dataset.Tables["ProductType"].DefaultView;

                adapter2.Update(dataset, "ProductType");
                MessageBox.Show("Added product type");
            }
            
        }

        private void edittypes_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            string type = selectedRow["Type"].ToString();
            EditProductType form = new EditProductType(type,true, id);
            form.Owner = this;
            bool? res = form.ShowDialog();
            if (res == true)
            {
                try
                {
                    DataRow[] row = dataset.Tables["ProductType"].Select("ID = " + id);
                    row[0]["Type"] = ProductTypeTemp;
                    DataGrid1.ItemsSource = dataset.Tables["ProductType"].DefaultView;

                    adapter2.Update(dataset, "ProductType");
                    MessageBox.Show("Edit product type");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void deletetypes_Click(object sender, RoutedEventArgs e)
        {
            DeleteFunc("ProductType");
            adapter2.Update(dataset, "ProductType");
        }
        #endregion

        #region Suppliers
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> PrDictionary = ProductsPair();
            EditSupplier form = new EditSupplier(PrDictionary);
            form.Owner = this;
            bool? res = form.ShowDialog();
            if (res == true)
            {
                DataRow row = dataset.Tables["Suppliers"].NewRow();
                row["SupplierName"] = SupplierNameTemp;
                row["ProductID"] = ProductIDTemp;
                row["DeliveryDate"] = DeliveryDateTemp;
                dataset.Tables["Suppliers"].Rows.Add(row);
                DataGrid1.ItemsSource = dataset.Tables["Suppliers"].DefaultView;

                adapter3.Update(dataset, "Suppliers");
                MessageBox.Show("Added supplier");
            }
        }

        private void editsupp_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid1.SelectedItem == null) return;
            DataRowView selectedRow = (DataRowView)DataGrid1.SelectedItem;
            int id = Convert.ToInt32(selectedRow["ID"]);
            string name = selectedRow["SupplierName"].ToString();
            int ProductID = Convert.ToInt32(selectedRow["ProductID"]);
            string date = selectedRow["DeliveryDate"].ToString();
            Dictionary<int, string> PrDictionary = ProductsPair();

            EditSupplier form = new EditSupplier(PrDictionary,ProductID,name,date,true, id);
            form.Owner = this;
            bool? res = form.ShowDialog();
            if (res == true)
            {
                try
                {
                    DataRow[] row = dataset.Tables["Suppliers"].Select("ID = " + id);
                    row[0]["SupplierName"] = SupplierNameTemp;
                    row[0]["ProductID"] = ProductIDTemp;
                    row[0]["DeliveryDate"] = DeliveryDateTemp;
                    DataGrid1.ItemsSource = dataset.Tables["Suppliers"].DefaultView;

                    adapter3.Update(dataset, "Suppliers");
                    MessageBox.Show("Edited supplier");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

                
        }

        private void deletesupp_Click(object sender, RoutedEventArgs e)
        {
            DeleteFunc("Suppliers");
            adapter3.Update(dataset, "Suppliers");
        }
        #endregion
    }
}