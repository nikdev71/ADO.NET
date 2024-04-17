using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using Microsoft.VisualBasic.Logging;
using MovieCamp.Model;
using MovieCamp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieCamp.ViewModel
{
    class AddDirectorVM: ViewModelBase
    {
        ObservableCollection<DirectorVM> directors;
        ICloseble window;
        public  AddDirectorVM(ICloseble win, IQueryable<Director> directors)
        {
            window = win;
            Directors = new ObservableCollection<DirectorVM>(directors.Select(d => new DirectorVM(d)));

        }
        public ObservableCollection<DirectorVM> Directors
        {
            get { return directors; }
            private set
            {
                directors = value;
               
                OnPropertyChanged(nameof(Directors));
            }
        }
        int director_index = -1;
        public int DirectorIndex
        {
            get => director_index;
            set { director_index = value;
                if (DirectorIndex != -1)
                {
                    var dir = directors.FirstOrDefault(d => d.Id == Directors[DirectorIndex].Id);
                    DirectorName = dir.Name;
                    DirectorLastName = dir.LastName;
                    Age = dir.Age;
                }
                OnPropertyChanged(nameof(DirectorIndex)); }
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

        private DelegateCommand? edit;
        public DelegateCommand Edit
        {
            get
            {
                if (edit == null)
                {
                    edit = new DelegateCommand(param => EditDir(), param => EditCE());
                }
                return edit;
            }
        }
        void EditDir()
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    var dir = db.Directors.FirstOrDefault(d => d.Id == Directors[DirectorIndex].Id);
                    if (dir != null)
                    {
                        dir.Name = DirectorName;
                        dir.LastName = DirectorLastName;
                        dir.Age = Age;
                        db.Update(dir);
                        db.SaveChanges();
                    }
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка : " + ex.Message);
            }
        }

        bool EditCE()
        {
            return DirectorName != string.Empty && DirectorLastName != string.Empty && Age > 0 && Age < 120 && director_index != -1;
        }

        private DelegateCommand? remove;
        public DelegateCommand Remove
        {
            get
            {
                if (remove == null)
                {
                    remove = new DelegateCommand(param => RemoveDir(), param => RemoveCE());
                }
                return remove;
            }
        }
        void RemoveDir()
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this director?", "Delete Director", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
                using (var db = new MovieCampContext())
                {
                    var dir = db.Directors.FirstOrDefault(d => d.Id == Directors[director_index].Id);
                    if(dir != null)
                    {
                        db.Remove(dir);
                        db.SaveChanges();
                    }
                    window.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка : " + ex.Message);
            }
        }

        bool RemoveCE()
        {
            return director_index != -1;
        }

    }
}
