using MovieCamp.Model;
using MovieCamp.ViewModel;
using System;
using System.Collections.Generic;
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

namespace MovieCamp.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, ICloseble
    {
        public AdminWindow()
        {
            InitializeComponent();
            using (var db = new MovieCampContext())
            {
                var quert = db.Movies.Select(x => x);
                var quert2 = db.Ratings.Select(x => x);
                var quert3 = db.Directors.Select(x => x);
                var quert4 = db.Genres.Select(x => x);

                DataContext = new AdminVM(this, quert, quert2, quert3,quert4);
            }
        }
        public void Close()
        {
            base.Close();
        }

       
    }
}
