using MovieCamp.Model;
using MovieCamp.View;
using MovieCamp.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MovieCamp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    //var jobs = from j in db.JobPositions
                    //           select j;
                    //var staff = from em in db.Employees
                    //            select em;
                    LoginWindow view = new LoginWindow();
                    view.Show();
                    //MainViewModel viewModel = new MainViewModel(jobs, staff);
                   // view.DataContext = viewModel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
