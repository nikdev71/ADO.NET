using MovieCamp.Model;
using MovieCamp.ViewModel;
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

namespace MovieCamp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new MovieCampContext())
            {
                var quert = db.Movies.Select(x => x);
                var quert2 = db.Ratings.Select(x => x);
                
                DataContext = new MainVM(quert,quert2);
            }
        }
    }
}