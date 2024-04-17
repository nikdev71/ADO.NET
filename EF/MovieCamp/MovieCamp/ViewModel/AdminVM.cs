using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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
    class AdminVM : ViewModelBase
    {
        ObservableCollection<DirectorVM> directors;
        public ObservableCollection<MovieVM> moviesDB { get; set; }
        public ObservableCollection<string> sorting;
        public ObservableCollection<GenreVM> genres { get; set; }
        bool isEdit = true;
        ICloseble window;
        public AdminVM(ICloseble win, IQueryable<Movie> movies, IQueryable<Rating> ratings, IQueryable<Director> directors, IQueryable<Genre> genres)
        {
            window = win;


            var moviesWithGenres = movies
            .Include(m => m.Genres)
            .ToList();

            var moviesWithRatings = moviesWithGenres
                .Select(m => new MovieVM(m, m.Genres.ToList(), m.Director, ratings.Where(r => r.Movie.Id == m.Id).ToList()))
                .ToList();

            moviesDB = new ObservableCollection<MovieVM>(moviesWithRatings);

            Genres = new ObservableCollection<GenreVM>(genres.Select(d => new GenreVM(d)));

            Directors = new ObservableCollection<DirectorVM>(directors.Select(d => new DirectorVM(d)));

            filteredMovies = moviesDB;
        }
        private ObservableCollection<MovieVM> filteredMovies;
        public ObservableCollection<MovieVM> Movies
        {
            get { return filteredMovies; }
            private set
            {
                filteredMovies = value;
                OnPropertyChanged(nameof(Movies));
            }
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
        public ObservableCollection<GenreVM> Genres
        {
            get { return genres; }
            private set
            {
                genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }

        int director_index = -1;
        public int DirectorIndex
        {
            get => director_index;
            set { director_index = value; OnPropertyChanged(nameof(DirectorIndex)); }
        }

        private MovieVM selectedMovie;
        public MovieVM SelectedMovie
        {
            get => selectedMovie;
            set
            {
                selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                DirectorIndex = Directors.IndexOf(Directors.FirstOrDefault(director => director.Id == SelectedMovie.Director.Id)!);
                IsDetailsVisible = Visibility.Visible;
                isEdit = true;
                BtnText = "Edit";
            }
        }
        string btnText = "Edit";
        public string BtnText
        {
            get { return btnText; }
            set { btnText = value; OnPropertyChanged(nameof(BtnText)); }
        }

        private Visibility isDetailsVisible = Visibility.Collapsed;
        public Visibility IsDetailsVisible
        {
            get { return isDetailsVisible; }
            set { isDetailsVisible = value; OnPropertyChanged(nameof(IsDetailsVisible)); }
        }

        DelegateCommand closeDetails;
        public DelegateCommand CloseDetails
        {
            get
            {
                if (closeDetails == null)
                {
                    closeDetails = new DelegateCommand(param => CloseDetailsMovie(), param => true);
                }
                return closeDetails;
            }
        }
        void CloseDetailsMovie()
        {
            IsDetailsVisible = Visibility.Collapsed;
        }

        #region Edit
        DelegateCommand editDetails;
        public DelegateCommand EditDetails
        {
            get
            {
                if (editDetails == null)
                {
                    editDetails = new DelegateCommand(param => EditDetailsMovie(), param => EditCan_Execute());
                }
                return editDetails;
            }
        }
        void EditDetailsMovie()
        {
            try
            {
                if (isEdit)
                {
                    using (var db = new MovieCampContext())
                    {
                        var query = db.Movies.Where(x => x.Id == SelectedMovie.Id).Single();
                        query.Title = SelectedMovie.Title;
                        query.Description = SelectedMovie.Description;
                        query.Year = SelectedMovie.Year;
                        query.Poster = SelectedMovie.Poster;
                        var curDirector = Directors[DirectorIndex];
                        query.Director = db.Directors.FirstOrDefault(x => x.Id == curDirector.Id)!;

                        db.Movies.Update(query);
                        db.SaveChanges();

                        SelectedMovie.Director = query.Director;

                        IsDetailsVisible = Visibility.Collapsed;
                        OnPropertyChanged(nameof(Movies));
                    }
                }
                else
                {
                    using (var db = new MovieCampContext())
                    {
                        Movie movie = new Movie();
                        movie.Title = SelectedMovie.Title;
                        movie.Description = SelectedMovie.Description;
                        movie.Year = SelectedMovie.Year;
                        movie.Poster = SelectedMovie.Poster;
                        var curDirector = Directors[DirectorIndex];
                        movie.Director = db.Directors.FirstOrDefault(x => x.Id == curDirector.Id)!;
                        List<Genre> genres = db.Genres.Where(g => SelectedMovie.GenresVM.Select(vm => vm.Title).Contains(g.Title)).ToList();
                        movie.Genres = genres;


                        db.Movies.Add(movie);
                        db.SaveChanges();


                        Movies.Add(new MovieVM(movie, movie.Genres.ToList(), movie.Director, db.Ratings.Where(r => r.Movie.Id == movie.Id).ToList()));

                        IsDetailsVisible = Visibility.Collapsed;
                        OnPropertyChanged(nameof(Movies));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool EditCan_Execute()
        {
            if (isEdit)
            {
                if (SelectedMovie != null)
                {
                    return SelectedMovie.Title != string.Empty;
                }
                return false;
            }
            else
            {
                return SelectedMovie.Poster != null && SelectedMovie.Title != null && selectedMovie.Director != null
                        && SelectedMovie.Description != null && SelectedMovie.GenresVM != null && SelectedMovie.Year > 1900;
            }

        }
        #endregion

        #region Select Image
        DelegateCommand selectImageCommand;
        public DelegateCommand SelectImageCommand
        {
            get
            {
                if (selectImageCommand == null)
                {
                    selectImageCommand = new DelegateCommand(param => SelectImage(), param => true);
                }
                return selectImageCommand;
            }
        }
        void SelectImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string postersFolderPath = Path.Combine(projectDirectory, "Posters");


                string newPosterPath = Path.Combine(postersFolderPath, Path.GetFileName(selectedImagePath));

                File.Copy(selectedImagePath, newPosterPath, true);

                SelectedMovie.Poster = newPosterPath;
                SelectedMovie.Poster = selectedImagePath;
                OnPropertyChanged(nameof(SelectedMovie));
            }
        }
        #endregion

        DelegateCommand addMovie;
        public DelegateCommand AddMovie
        {
            get
            {
                if (addMovie == null)
                {
                    addMovie = new DelegateCommand(param => NewMovie(), param => true);
                }
                return addMovie;
            }
        }
        void NewMovie()
        {
            try
            {
                var moviee = new MovieVM();
                SelectedMovie = moviee;
                IsDetailsVisible = Visibility.Visible;
                isEdit = false;
                BtnText = "Add";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        DelegateCommand addDirector;
        public DelegateCommand AddDirector
        {
            get
            {
                if (addDirector == null)
                {
                    addDirector = new DelegateCommand(param => NewDirector(), param => true);
                }
                return addDirector;
            }
        }
        void NewDirector()
        {
            try
            {
                Directorxaml win = new Directorxaml();
                win.ShowDialog();
                using (var db = new MovieCampContext())
                {
                    var query = db.Directors.Select(x => x);
                    Directors = new ObservableCollection<DirectorVM>(query.Select(q=> new DirectorVM(q)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Genres
        int currentgenreIndex = -1;
        public int CurrentGenreIndex
        {
            get { return currentgenreIndex; }
            set { currentgenreIndex = value; OnPropertyChanged(nameof(CurrentGenreIndex)); }
        }

        int genreIndex = -1;
        public int GenreIndex
        {
            get { return genreIndex; }
            set { genreIndex = value; OnPropertyChanged(nameof(GenreIndex)); }
        }

        DelegateCommand addGenre;
        public DelegateCommand AddGenre
        {
            get
            {
                if (addGenre == null)
                {
                    addGenre = new DelegateCommand(param => AddGenreToMovie(), param => AddGenreToMovieCE());
                }
                return addGenre;
            }
        }
        void AddGenreToMovie()
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    if (isEdit)
                    {
                        var curmovie = db.Movies.FirstOrDefault(x => x.Id == SelectedMovie.Id);
                        var curgenre = db.Genres.FirstOrDefault(x => x.Title == Genres[GenreIndex].Title);
                        if (curmovie?.Genres.Any(g => g.Id == curgenre.Id) == false)
                        {
                            SelectedMovie.GenresVM.Add(Genres[GenreIndex]);
                            curmovie?.Genres.Add(curgenre);
                        }
                    }
                    else
                    {
                        SelectedMovie.GenresVM.Add(Genres[GenreIndex]);
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool AddGenreToMovieCE()
        {
            return GenreIndex != -1;
        }

        DelegateCommand removeGenre;
        public DelegateCommand RemoveGenre
        {
            get
            {
                if (removeGenre == null)
                {
                    removeGenre = new DelegateCommand(param => removeGenreToMovie(), param => RemoveGenreToMovieCE());
                }
                return removeGenre;
            }
        }
        void removeGenreToMovie()
        {
            try
            {
                using (var db = new MovieCampContext())
                {
                    if (isEdit)
                    {
                        var curmovie = db.Movies.FirstOrDefault(x => x.Id == SelectedMovie.Id);
                        var curgenre = db.Genres.FirstOrDefault(x => x.Title == SelectedMovie.GenresVM[CurrentGenreIndex].Title);
                        SelectedMovie.GenresVM.Remove(SelectedMovie.GenresVM[CurrentGenreIndex]);
                        curmovie?.Genres.Remove(curgenre);
                    }
                    else
                    {
                        SelectedMovie.GenresVM.Remove(SelectedMovie.GenresVM[CurrentGenreIndex]);
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool RemoveGenreToMovieCE()
        {
            return CurrentGenreIndex != -1;
        }
        #endregion


        #region Remove
        DelegateCommand removeMovie;
        public DelegateCommand RemoveMovie
        {
            get
            {
                if (removeMovie == null)
                {
                    removeMovie = new DelegateCommand(param => Remove(), param => true);
                }
                return removeMovie;
            }
        }
        void Remove()
        {
            if (MessageBox.Show("Are you sure you want to delete this movie?", "Delete Movie", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            using (var db = new MovieCampContext())
            {
                var movie = db.Movies.FirstOrDefault(x=>x.Id == SelectedMovie.Id);
                db.Movies.Remove(movie);
                db.SaveChanges();
                Movies.Remove(SelectedMovie);
                OnPropertyChanged(nameof(Movies));
            }
            IsDetailsVisible = Visibility.Collapsed;
        }
        #endregion

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                UpdateFilteredMovies();
            }
        }

        private void UpdateFilteredMovies()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                Movies = new ObservableCollection<MovieVM>(moviesDB);
            }
            else
            {
                string searchQueryLower = SearchQuery.ToLower();

                Movies = new ObservableCollection<MovieVM>(moviesDB.Where(movie =>
                    movie.Title.ToLower().Contains(searchQueryLower) ||
                    movie.DirectorName.ToLower().Contains(searchQueryLower) ||
                    movie.DirectorLastName.ToLower().Contains(searchQueryLower) ||
                    movie.Year.ToString().Contains(SearchQuery)
                ));
            }
        }

    }
}
