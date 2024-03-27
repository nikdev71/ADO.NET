using AcademyGroupMVVM.ViewModels;
using MovieCamp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.ViewModel
{
    internal class UserVM:ViewModelBase
    {
        User user;
        public UserVM(User usr)
        {
            user=usr;
        }
        public string Login
        {
            get { return user.Login; }
            set { user.Login = value; OnPropertyChanged(nameof(Login)); }
        }
        public string Password
        {
            get { return user.Password; }
            set { user.Password = value; OnPropertyChanged(nameof(Password)); }
        }
    }
}
