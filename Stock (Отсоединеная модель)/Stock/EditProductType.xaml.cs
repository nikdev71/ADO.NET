using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Stock
{
    /// <summary>
    /// Interaction logic for EditProductType.xaml
    /// </summary>
    public partial class EditProductType : Window
    {
        bool Edit;
        int ID;
        public EditProductType(string type="",bool Edit = false, int ID = 0)
        {
            InitializeComponent();
            this.Edit = Edit;
            this.ID = ID;
            if (Edit)
            {
                Head.Text = "Edit product type with ID - " + ID;
            }
            TypePr.Text = type;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string type = TypePr.Text;
                MainWindow parentWindow = this.Owner as MainWindow;
                if (parentWindow != null)
                {
                    parentWindow.ReceiveParametersForProductType(type);
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
    }
}
