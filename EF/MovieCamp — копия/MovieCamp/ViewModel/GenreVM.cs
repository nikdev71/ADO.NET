using AcademyGroupMVVM.ViewModels;
using MovieCamp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.ViewModel
{
    internal class GenreVM: ViewModelBase
    {
        Genre genre;
        public GenreVM(Genre grn)
        {
            genre = grn;
        }
        public string Title
        {
            get { return genre.Title; }
            set { genre.Title = value; OnPropertyChanged(nameof(Title)); }
        }
    }
}
