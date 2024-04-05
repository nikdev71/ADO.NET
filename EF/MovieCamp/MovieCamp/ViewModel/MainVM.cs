using AcademyGroupMVVM.ViewModels;
using MovieCamp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.ViewModel
{
    internal class MainVM: ViewModelBase
    {
        public ObservableCollection<MovieVM> Movies { get; set; }
        public MainVM(IQueryable<Movie> m) 
        {
            Movies = new ObservableCollection<MovieVM>(m.Select(mov=>new MovieVM(mov, mov.Genres.ToList()))); 
        }
    }
}
