using IT_Company.Model;
using IT_Company.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Automation;

namespace IT_Company
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
                using (var db = new ITCompanyContext())
                {
                    var jobs = from j in db.JobPositions
                                 select j;
                    var staff = from em in db.Employees
                                   select em;
                    MainWindow view = new MainWindow();
                    MainViewModel viewModel = new MainViewModel(jobs, staff);
                    view.DataContext = viewModel;
                    view.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
