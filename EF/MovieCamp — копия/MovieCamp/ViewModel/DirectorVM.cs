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
        public int Id
        {
            get { return director.Id; }
            set { director.Id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string Name
        {
            get { return director.Name +" "+ director.LastName; }
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
