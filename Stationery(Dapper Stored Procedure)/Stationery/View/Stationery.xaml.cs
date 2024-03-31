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
using Dapper;
using Stationery.Models;

namespace Stationery
{
    /// <summary>
    /// Interaction logic for Stationery.xaml
    /// </summary>
    public partial class EditStationery : Window
    {
        bool Edit ;
        int ID ;
        public EditStationery(bool edit=false, int iD=0)
        {
            InitializeComponent();
            init();
            Edit = edit;
            ID = iD;
            if (Edit) Head.Text = "Edit Stationery";

        }
        private void init()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(MainWindow.connectionString))
                {
                    var types = db.Query<TypeVM>("select Id, Title from TypesOfStationery");
                    foreach (TypeVM t in types)
                    {
                        cb1.Items.Add(t.Title);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TitlePr.Text == "" || QuantityPr.Text == "" || CostPr.Text == "")
            {
                MessageBox.Show("Fill the parameters");
                return;
            }
            try
            {
                string? typeName = cb1.SelectedItem as string;
                int typeId = GetTypeId(typeName);
                int cost = Convert.ToInt32(CostPr.Text);
                int quantity = Convert.ToInt32(QuantityPr.Text);
                if (Edit)
                {
                    using (IDbConnection db = new SqlConnection(MainWindow.connectionString))
                    {


                        var dynamicParams = new DynamicParameters();
                        dynamicParams.Add("@Id", ID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                        dynamicParams.Add("@Title", TitlePr.Text, dbType: DbType.String, direction: ParameterDirection.Input);
                        dynamicParams.Add("@Quantity", quantity, dbType: DbType.Int32, direction: ParameterDirection.Input);
                        dynamicParams.Add("@Cost", cost, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                        dynamicParams.Add("@TypeId", typeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                        int n = db.Execute("UpdateStationery", dynamicParams, commandType: CommandType.StoredProcedure);
                        if (n == 1)
                            MessageBox.Show("Канцтовар успешно изменен!");
                    }
                }
                else
                {
                    using (IDbConnection db = new SqlConnection(MainWindow.connectionString))
                    {
                        

                        var dynamicParams = new DynamicParameters();
                        dynamicParams.Add("@Title", TitlePr.Text, dbType: DbType.String, direction: ParameterDirection.Input);
                        dynamicParams.Add("@Quantity", quantity, dbType: DbType.Int32, direction: ParameterDirection.Input);
                        dynamicParams.Add("@Cost", cost, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                        dynamicParams.Add("@TypeId", typeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                        int n = db.Execute("InsertIntoStationery", dynamicParams, commandType: CommandType.StoredProcedure);
                        if (n == 1)
                            MessageBox.Show("Канцтовар успешно добавлена в таблицу!");
                    }
                }
                DialogResult = true;
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
        private int GetTypeId(string typeName)
        {
            int typeId = -1; 

            using (IDbConnection db = new SqlConnection(MainWindow.connectionString))
            {
                var parameters = new { Title = typeName };
                typeId = db.QueryFirstOrDefault<int>("SELECT Id FROM TypesOfStationery WHERE Title = @Title", parameters);
            }

            return typeId;
        }
    }
}
