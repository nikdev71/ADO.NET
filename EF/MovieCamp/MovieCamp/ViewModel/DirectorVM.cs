using AcademyGroupMVVM.ViewModels;
using MovieCamp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.ViewModel
{
    internal class DirectorVM: ViewModelBase
    {
        Director director;
        public DirectorVM(Director dir)
        {
            director = dir;
        }
        public string Name
        {
            get { return director.Name; }
            set { director.Name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string LastName
        {
            get { return director.LastName; }
            set { director.LastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        public int Age
        {
            get { return director.Age; }
            set { director.Age = value; OnPropertyChanged(nameof(Age)); }
        }
    }
}
