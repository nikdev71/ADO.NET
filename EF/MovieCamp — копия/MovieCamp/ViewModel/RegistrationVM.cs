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
    internal class RegistrationVM : ViewModelBase
    {
        ICloseble window;
        public RegistrationVM(ICloseble win)
        {
            window = win;
        }
        string login;
        public string Login
        {
            get => login;
            set { login = value; OnPropertyChanged(nameof(Login)); }
        }
        string password;
        public string Password
        {
            get => password; set { password = value; OnPropertyChanged(nameof(Password)); }
        }
        string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword; set { repeatPassword = value; OnPropertyChanged(nameof(RepeatPassword)); }
        }
        private DelegateCommand register;
        public DelegateCommand Register
        {
            get
            {
                if (register == null) 
                {
                    register = new DelegateCommand(param => RegisterUser(), param => RegisterUserCE());
                }
                return register;
            }
        }
        void RegisterUser()
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    bool userExists = db.Users.Any(u => u.Login == login);

                    if (userExists)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует в базе данных.");
                    }
                    else
                    {
                        User newuser = new User { Login = login, Password = password };
                        db.Users.Add(newuser);
                        db.SaveChanges();
                        LoginWindow lw = new LoginWindow();
                        lw.Show();
                        window.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool RegisterUserCE()
        {
            return login != null && password!= null
            && repeatPassword == password;
        }
    }
}
