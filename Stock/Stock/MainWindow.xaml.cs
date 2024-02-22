using System.Data.SqlClient;
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

namespace Stock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connect;
        SqlCommand command;
        Button button;
        public MainWindow()
        {
            InitializeComponent();
            button = btnAllinfo;
        }
        void ConnectionToServer()
        {
            connect = new SqlConnection(@"Initial Catalog=Stock;Data Source=DESKTOP-VDC9A9A;Integrated Security=SSPI");
            command = new SqlCommand();
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "SELECT p.ID,  p.ProductTitle,    pt.Type ,   p.Cost,   p.Quantity, s.SupplierName, s.DeliveryDate \r\nFROM Products as p \r\nJOIN  ProductType as pt ON p.TypeID = pt.ID\r\nleft JOIN  Suppliers as s ON p.ID = s.ProductID;";
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
                menu.IsEnabled = true;
                MessageBox.Show("Connection is succesfull");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionToServer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ls1.Items.Clear();
            if (connect != null && command != null)
            {
                menu.IsEnabled = false;
                command.Dispose();
                connect.Close();
                command = null;
                connect = null;
            }
            MessageBox.Show("Disconnected from databse");
        }
        private void SelectFromDB(string select)
        {
            command.CommandText = select;
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
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SelectFromDB("SELECT p.ID,  p.ProductTitle,    pt.Type ,   p.Cost,   p.Quantity, s.SupplierName, s.DeliveryDate \r\nFROM Products as p \r\nJOIN  ProductType as pt ON p.TypeID = pt.ID\r\nleft JOIN  Suppliers as s ON p.ID = s.ProductID;");
        }
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (connect == null && command == null)
            {
                MessageBox.Show("Please connect to database");
                return;
            } 
            button = (Button)sender;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SelectFromDB("Select ID, Type from ProductType");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SelectFromDB("SELECT ID, SupplierName FROM Suppliers");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SelectFromDB("Select Products.ID, ProductTitle, pt.Type,Cost, Quantity\r\nFrom Products\r\n join ProductType as pt on TypeID = pt.ID  where Quantity = \r\n(Select MAX(Quantity) from Products)");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SelectFromDB("Select Products.ID, ProductTitle, pt.Type, Cost, Quantity\r\nFrom Products\r\n join ProductType as pt on TypeID = pt.ID  where Quantity = \r\n(Select MIN(Quantity) from Products)");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SelectFromDB("Select Products.ID,  ProductTitle, pt.Type,Cost, Quantity\r\nFrom Products\r\n join ProductType as pt on TypeID = pt.ID where Cost = (\r\nSelect MIN(Cost)\r\nfrom Products\r\n)");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            SelectFromDB("Select Products.ID, ProductTitle, pt.Type,  Cost, Quantity \r\nFrom Products\r\n join ProductType as pt on TypeID = pt.ID where Cost = (\r\nSelect MAX(Cost)\r\nfrom Products\r\n)");

        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string type = givencat.Text.ToString();
            if (type == null) return;
            string str = "Select Products.ID, ProductTitle, pt.Type, Quantity, Cost from Products join ProductType as pt on pt.ID = TypeID where pt.Type = '" + type+"'";
            SelectFromDB(str);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            string supp = supplier.Text.ToString();
            if (supp == null) return;
            string str = "Select Products.ID, ProductTitle, pt.Type, Cost, Quantity, SupplierName\r\nFrom Products\r\njoin Suppliers as s on s.ProductID = Products.ID join ProductType as pt on pt.ID = TypeID\r\nwhere SupplierName = '" + supp+"'";
            SelectFromDB(str);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            string supp = supplier.Text.ToString();
            if (supp == null) return;
            string str = "Select pt.Type, AVG(Quantity) From Products join ProductType as pt on pt.ID = TypeID Group By pt.Type";
            SelectFromDB(str);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            SelectFromDB("Select Products.ID, ProductTitle, pt.Type, Cost, Quantity, DeliveryDate\r\nFrom Products\r\njoin Suppliers as s on s.ProductID = Products.ID join ProductType as pt on pt.ID = TypeID\r\nwhere DeliveryDate = (\r\nSelect MIN(DeliveryDate) From Suppliers)");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditProduct form = new EditProduct();
            bool? res = form.ShowDialog();
            if(res == true) 
            {
                SelectFromDB("SELECT p.ID,  p.ProductTitle,    pt.Type ,   p.Cost,   p.Quantity, s.SupplierName, s.DeliveryDate \r\nFROM Products as p \r\nJOIN  ProductType as pt ON p.TypeID = pt.ID\r\nleft JOIN  Suppliers as s ON p.ID = s.ProductID;");
                MessageBox.Show("Product addded");

            }

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedItem = ls1.SelectedItem.ToString();

            string[] data = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string id = data[0];
            EditProduct form = new EditProduct(true,id);
            bool? res = form.ShowDialog();
            if (res == true)
            {
                MessageBox.Show("Product edited");
                SelectFromDB("SELECT p.ID,  p.ProductTitle,    pt.Type ,   p.Cost,   p.Quantity, s.SupplierName, s.DeliveryDate \r\nFROM Products as p \r\nJOIN  ProductType as pt ON p.TypeID = pt.ID\r\nleft JOIN  Suppliers as s ON p.ID = s.ProductID;");
            }

        }

        private void ls1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (button == btnAllinfo || button == btnMAXQ || button == btnMINQ || button == btnMAXC || button == btnMINC || button == btnOld || button == btnPrCat || button == btnCurSupp)
            {
                if (ls1.SelectedItem != null)
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
                if (ls1.SelectedItem != null)
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
                if (ls1.SelectedItem != null)
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
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    string selectedItem = ls1.SelectedItem.ToString();
                    string[] data = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string id = data[0];
                    command.CommandText = $"Delete From Products Where ID ={id}";
                    int n = command.ExecuteNonQuery();
                    MessageBox.Show("Product deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                SelectFromDB("SELECT p.ID,  p.ProductTitle,    pt.Type ,   p.Cost,   p.Quantity, s.SupplierName, s.DeliveryDate \r\nFROM Products as p \r\nJOIN  ProductType as pt ON p.TypeID = pt.ID\r\nleft JOIN  Suppliers as s ON p.ID = s.ProductID;");
                deleteproduct.IsEnabled = false;
                editproduct.IsEnabled = false;
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            EditProductType form = new EditProductType();
            bool? res = form.ShowDialog();
            if (res== true)
            {
                SelectFromDB("Select ID, Type from ProductType");
                MessageBox.Show("Added product type");
            }
        }

        private void edittypes_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = ls1.SelectedItem.ToString();

            string[] data = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string id = data[0];
            EditProductType form = new EditProductType(true, id);
            bool? res = form.ShowDialog();
            if (res == true)
            {
                SelectFromDB("Select ID, Type from ProductType");
                MessageBox.Show("Added product type");
            }
        }

        private void deletetypes_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product type?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    string selectedItem = ls1.SelectedItem.ToString();
                    string[] data = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string id = data[0];
                    command.CommandText = $"Delete From ProductType Where ID ={id}";
                    int n = command.ExecuteNonQuery();
                    MessageBox.Show("Product type deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                SelectFromDB("Select ID, Type from ProductType");
                deletetypes.IsEnabled = false;
                edittypes.IsEnabled = false;
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            EditSupplier form = new EditSupplier();
            bool? res = form.ShowDialog();
            if (res == true)
            {
                SelectFromDB("SELECT ID, SupplierName FROM Suppliers");
                MessageBox.Show("Added supplier");
            }
        }

        private void editsupp_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = ls1.SelectedItem.ToString();

            string[] data = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string id = data[0];
            EditSupplier form = new EditSupplier(true, id);
            bool? res = form.ShowDialog();
            if (res == true)
            {
                SelectFromDB("SELECT ID, SupplierName FROM Suppliers");
                MessageBox.Show("Added supplier");
            }
        }

        private void deletesupp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Delete product", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    string selectedItem = ls1.SelectedItem.ToString();
                    string[] data = selectedItem.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string id = data[0];
                    command.CommandText = $"Delete From Suppliers Where ID ={id}";
                    int n = command.ExecuteNonQuery();
                    MessageBox.Show("Supplier deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                SelectFromDB("SELECT ID, SupplierName FROM Suppliers");
                deletetypes.IsEnabled = false;
                edittypes.IsEnabled = false;
            }
        }
    }
}