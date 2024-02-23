using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
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
        int ID;
        Dictionary<int, string> types;
        public EditProduct( Dictionary<int, string> types,string typestr="", string title="", int cost=0, int quantity=0, bool Edit = false, int ID = 0)
        {
            InitializeComponent();
            this.Edit = Edit;
            this.ID = ID;
            this.types = types;
            if(Edit)
            {
                Head.Text = "Edit product with ID - "+ ID;
            }
            foreach(var type in types)
            {
                TypePr.Items.Add(type.Value);

                if (type.Value == typestr)
                {
                    TypePr.SelectedItem = type.Value;
                }

            }
            TitlePr.Text = title;
            CostPr.Text = cost.ToString();
            QuantityPr.Text = quantity.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitlePr.Text;
                int cost = Convert.ToInt32(CostPr.Text);
                int quantity = Convert.ToInt32(QuantityPr.Text);
                string selectedValue = TypePr.SelectedItem.ToString();
                KeyValuePair<int, string> selectedType = types.FirstOrDefault(x => x.Value == selectedValue);
                int selectedTypeId = selectedType.Key;

                MainWindow parentWindow = this.Owner as MainWindow;
                if (parentWindow != null)
                {
                    parentWindow.ReceiveParametersForProduct(title, selectedTypeId, cost, quantity);
                }
                DialogResult = true;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
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
