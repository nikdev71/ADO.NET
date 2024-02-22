using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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

namespace Stock
{
    /// <summary>
    /// Interaction logic for EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Window
    {
        bool Edit;
        int ID;
        Dictionary<int, string> products;

        public EditSupplier(Dictionary<int, string> products,int prkey=0,string name= "",string date="yyyy-MM-dd",bool Edit = false, int ID = 0)
        {
            InitializeComponent();
            this.Edit = Edit;
            this.ID = ID;
            this.products = products;
            if (Edit)
            {
                Head.Text = "Edit supplier with ID - " + ID;
            }
            foreach (var pr in products)
            {
                PrID.Items.Add(pr.Value);

                if (pr.Key == prkey)
                {
                    PrID.SelectedItem = pr.Value;
                }

            }
            SupplierName.Text = name;
            if(date == "yyyy-MM-dd")
            {
                DelDate.Text = DateTime.Today.ToString();
            }
            else DelDate.Text = date;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string suppname = SupplierName.Text;
                string date = DelDate.Text;
                string selectedValue = PrID.SelectedItem.ToString();
                KeyValuePair<int, string> selectedProduct = products.FirstOrDefault(x => x.Value == selectedValue);
                int selectedProductId = selectedProduct.Key;

                MainWindow parentWindow = this.Owner as MainWindow;
                if (parentWindow != null)
                {
                    parentWindow.ReceiveParametersForSupplier(suppname, selectedProductId, date);
                }
                DialogResult = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void TypePr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9-]+");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
