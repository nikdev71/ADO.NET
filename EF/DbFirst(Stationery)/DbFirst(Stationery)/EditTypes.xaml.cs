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
    /// Interaction logic for EditTypes.xaml
    /// </summary>
    public partial class EditTypes : Window
    {
        bool Edit;
        int ID;
        public EditTypes(bool edit = false, int iD = 0, string title = "")
        {
            InitializeComponent();
            Edit = edit;
            ID = iD;
            TitlePr.Text = title;
            if (Edit) Head.Text = "Edit Types";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TitlePr.Text == "")
            {
                MessageBox.Show("Fill the parameters");
                return;
            }
            try
            {
                using (StationeryContext db = new StationeryContext())
                {
                    int numberOfRowInserted = 0;
                    if (Edit)
                    {
                        SqlParameter[] sqlParameters = {
                            new SqlParameter("Id", ID),
                            new SqlParameter("Title", TitlePr.Text),
                        };
                        numberOfRowInserted = db.Database.ExecuteSqlRaw("UpdateTypes @Id, @Title", sqlParameters);
                    }
                    else
                    {
                        SqlParameter[] sqlParameters = {
                            new SqlParameter("Title", TitlePr.Text),
                        };
                        numberOfRowInserted = db.Database.ExecuteSqlRaw("InsertIntoTypes @Title", sqlParameters);
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
