using AuthorsAndBooks_DB_.Models;
using AuthorsAndBooks_DB_.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace AuthorsAndBooks_DB_
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
               using(var db = new AuthorsAndBooksContext())
               {
                    var authors = db.Authors.Select( a=> a);
                    var books = db.Books.Select( b => b);
                    MainWindow view = new MainWindow();
                    MainViewModel mainViewModel = new MainViewModel(books, authors);
                    view.DataContext = mainViewModel;
                    view.Show();
               }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }

}
