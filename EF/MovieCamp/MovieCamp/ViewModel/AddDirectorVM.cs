using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using Microsoft.VisualBasic.Logging;
using MovieCamp.Model;
using MovieCamp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieCamp.ViewModel
{
    class AddDirectorVM: ViewModelBase
    {
        ICloseble window;
        public  AddDirectorVM(ICloseble win)
        {
            window = win;
        } 
        string directorName;
        public string DirectorName
        {
            get { return directorName; }
            set { directorName = value; OnPropertyChanged(nameof(DirectorName)); }
        }
        string directorLastName;
        public string DirectorLastName
        {
            get { return directorLastName; }
            set { directorLastName = value; OnPropertyChanged(nameof(DirectorLastName)); }
        }
        int age;
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(nameof(Age)); }
        }
        private DelegateCommand? add;
        public DelegateCommand Add
        {
            get
            {
                if (add == null)
                {
                    add = new DelegateCommand(param => AddDir(), param => AddCE());
                }
                return add;
            }
        }
        void AddDir()
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    db.Directors.Add(new Director { Name = DirectorName, LastName = DirectorLastName, Age = Age });
                    db.SaveChanges();
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка : " + ex.Message);
            }
        }

        bool AddCE()
        {
            return DirectorName != string.Empty && DirectorLastName != string.Empty && Age >0 && Age <120 ;
        }

    }
}
