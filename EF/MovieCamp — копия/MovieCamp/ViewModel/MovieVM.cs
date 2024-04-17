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
    internal class MovieVM : ViewModelBase
    {
        Movie movie;
        ObservableCollection<GenreVM> genresVM;
        List<Rating>? ratings; 
        public Director director;

        public MovieVM()
        {
            using (var db = new MovieCampContext())
            {
                movie = new Movie() { Year=1901};
                this.ratings = new List<Rating>();
                genresVM = new ObservableCollection<GenreVM>();
                this.director = new Director();
            }

        }
        public MovieVM(Movie mv, List<Genre> genres, Director dir, List<Rating>? rat)
        {
            movie = mv;
            ratings = rat; 
            director = dir;
            genresVM = new ObservableCollection<GenreVM>(genres.Select(g => new GenreVM(g)));
        }


        public int Id
        {
            get { return movie.Id; }
            set { movie.Id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string Title
        {
            get { return movie.Title; }
            set { movie.Title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string DirectorName
        {
            get { return director.Name +" "+ director.LastName; }
            set { director.Name = value; OnPropertyChanged(nameof(DirectorName)); }
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

        public string Poster
        {
            get { return movie.Poster; }
            set { movie.Poster = value; OnPropertyChanged(nameof(Poster)); }
        }
        public string Description
        {
            get { return movie.Description; }
            set { movie.Description = value; OnPropertyChanged(nameof(Description)); }
        }
        public ObservableCollection<GenreVM> GenresVM
        {
            get { return genresVM; }
        }
        public Director Director 
        {   get { return director; }
            set { director = value; OnPropertyChanged(nameof(Director));}
        }
        public double AverageRating
        {
            get
            {
                return ratings!.Any() ? ratings!.Where(r => r.Movie.Id == movie.Id).Average(r => r.Grade) : 0;
            }
        }
    }

}
