using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using MovieCamp.Model;
using MovieCamp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieCamp.ViewModel
{
    internal class LoginVM : ViewModelBase
    {
        ICloseble window;
        public LoginVM(ICloseble win) 
        {
            window = win;
        }
        string login = "admin";
        public string Login
        {
            get => login;
            set { login = value; OnPropertyChanged(nameof(Login)); }
        }
        string password = "adminadmin";
        public string Password
        {
            get => password; set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        private DelegateCommand? entry;
        public DelegateCommand Entry
        {
            get
            {
                if (entry == null)
                {
                    entry = new DelegateCommand(param => EntryUser(), param => EntryUserCE());
                }
                return entry;
            }
        }
        void EntryUser()
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Login == login);

                    if (user == null)
                    {
                        MessageBox.Show("Пользователь с таким логином не найден.");
                    }
                    else
                    {
                        if (user.Password == "adminadmin" && user.Login == "admin")
                        {
                            AdminWindow view = new AdminWindow();
                            view.Show();
                            window.Close();
                        }
                        else if (user.Password == Password)
                        {
                            MainWindow view = new MainWindow();
                            view.Show();
                            window.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при входе: " + ex.Message);
            }
        }

        bool EntryUserCE()
        {
            return login != null && password != null;
        }
        private DelegateCommand? register;
        public DelegateCommand Register
        {
            get
            {
                if (register == null)
                {
                    register = new DelegateCommand(param => RegisterUser(), null);
                }
                return register;
            }
        }
        void RegisterUser()
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            window.Close();
        }
    }
}
