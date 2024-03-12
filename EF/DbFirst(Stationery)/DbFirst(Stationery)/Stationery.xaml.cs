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
using DbFirst_Stationery_;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Stationery
{
    /// <summary>
    /// Interaction logic for Stationery.xaml
    /// </summary>
    public partial class EditStationery : Window
    {
        bool Edit ;
        int ID ;
        public EditStationery(bool edit=false, int iD=0, string title = "", int quantity=0, int cost=0, string type="")
        {
            InitializeComponent();
            init(type);
            Edit = edit;
            ID = iD;
            TitlePr.Text= title;
            QuantityPr.Text = quantity.ToString();
            CostPr.Text = cost.ToString();
            if (Edit) Head.Text = "Edit Stationery";
        }
        private void init(string type)
        {
            try
            {
                using (StationeryContext db = new StationeryContext())
                {
                    var typesOfStationery =  db.TypesOfStationeries.ToList();

                    cb1.Items.Clear();

                    foreach (var types in typesOfStationery)
                    {
                        cb1.Items.Add(types.Title);
                    }
                    if (type != string.Empty)
                    {
                        cb1.SelectedItem = type;
                    }
                    else if (cb1.Items.Count > 0)
                    {
                        cb1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void TypePr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[0-9]+");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StationeryContext db = new StationeryContext())
                {
                    int numberOfRowInserted=0;
                    var currentType = db.TypesOfStationeries.FirstOrDefault(t => t.Title == cb1.Text);
                    if (Edit)
                    {
                        SqlParameter[] sqlParameters = {
                            new SqlParameter("Id", ID),
                            new SqlParameter("Title", TitlePr.Text),
                            new SqlParameter("Quantity", int.Parse(QuantityPr.Text)),
                            new SqlParameter("Cost", int.Parse(CostPr.Text)),
                            new SqlParameter("TypeId", currentType?.Id),
                        };
                        numberOfRowInserted = db.Database.ExecuteSqlRaw("UpdateStationery @Id, @Title, @Quantity, @Cost, @TypeId", sqlParameters);
                    }
                    else
                    {
                        SqlParameter[] sqlParameters = {
                            new SqlParameter("Title", TitlePr.Text),
                            new SqlParameter("Quantity", int.Parse(QuantityPr.Text)),
                            new SqlParameter("Cost", int.Parse(CostPr.Text)),
                            new SqlParameter("TypeId", currentType?.Id),
                        };
                        numberOfRowInserted = db.Database.ExecuteSqlRaw("InsertIntoStationery @Title, @Quantity, @Cost, @TypeId", sqlParameters);
                    }
                    if (numberOfRowInserted == 1)
                    MessageBox.Show("Row is affected!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
