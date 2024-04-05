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
    internal class MovieVM: ViewModelBase
    {
        Movie movie;
        ObservableCollection<GenreVM> genresVM;
        public MovieVM(Movie mv, List<Genre> genres) 
        {
            movie = mv;
            genresVM = new ObservableCollection<GenreVM>(genres.Select(g => new GenreVM(g)));
        }

        public string Title
        {
            get {  return movie.Title;}
            set { movie.Title = value; OnPropertyChanged(nameof(Title)); }
        }
        public string DirectorName
        {
            get { return movie.Director.Name; }
            set { movie.Director.Name = value; OnPropertyChanged(nameof(DirectorName)); }
        }
        public string DirectorLastName
        {
            get { return movie.Director.LastName; }
            set { movie.Director.LastName = value; OnPropertyChanged(nameof(DirectorLastName)); }
        }
        public int Year
        {
            get { return movie.Year; }
            set { movie.Year = value; OnPropertyChanged(nameof(Year)); }
        }
        public byte[] Poster
        {
            get { return movie.Poster; }
            set { movie.Poster = value; OnPropertyChanged(nameof(Poster)); }
        }
        //public IEnumerable<Genre> Genres 
        //{ 
        //    get { return movie.Genres; }
        //}
        public ObservableCollection<GenreVM> GenresVM
        {
            get { return genresVM; }
        }
        public IEnumerable<Rating> Ratings
        {
            get { return movie.Ratings; }
        }
    }
}
