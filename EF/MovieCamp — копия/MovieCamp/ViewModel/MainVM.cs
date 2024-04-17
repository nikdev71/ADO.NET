﻿using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MovieCamp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MovieCamp.ViewModel
{
    internal class MainVM: ViewModelBase
    {
        public ObservableCollection<MovieVM> moviesDB { get; set; }
        public ObservableCollection<string> sorting;
        public MainVM(ObservableCollection<MovieVM> movies)
        {
            Movies = movies;
        }
        public MainVM(IQueryable<Movie> movies,  IQueryable<Rating> ratings) 
        {
            var moviesWithGenres = movies
            .Include(m => m.Genres) 
            .ToList();

            var moviesWithRatings = moviesWithGenres
                .Select(m => new MovieVM(m, m.Genres.ToList(), m.Director, ratings.Where(r => r.Movie.Id == m.Id).ToList()))
                .ToList();

            moviesDB = new ObservableCollection<MovieVM>(moviesWithRatings);
            filteredMovies = moviesDB;
            sorting = new ObservableCollection<string>(){ "Year","Rating","Alphabet"};

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

        public ObservableCollection<string> Sorting
        {
            get { return sorting; }
            set { sorting = value; OnPropertyChanged(nameof(Sorting)); }
        }
        private string selectedSortCriteria="Rating";
        public string SelectedSortCriteria
        {
            get { return selectedSortCriteria; }
            set
            {
                selectedSortCriteria = value;
                OnPropertyChanged(nameof(SelectedSortCriteria));
                SortMovies();
            }
        }
       
        private void SortMovies()
        {
            switch (SelectedSortCriteria)
            {
                case "Year":
                    Movies = new ObservableCollection<MovieVM>(Movies.OrderByDescending(movie => movie.Year));
                    break;
                case "Alphabet":
                    Movies = new ObservableCollection<MovieVM>(Movies.OrderBy(movie => movie.Title));
                    break;
                case "Rating":
                    Movies = new ObservableCollection<MovieVM>(Movies.OrderByDescending(movie => movie.AverageRating));
                    break;
                default:
                    break;
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

                filteredMovies = new ObservableCollection<MovieVM>(moviesDB.Where(movie =>
                    movie.Title.ToLower().Contains(searchQueryLower) ||
                    movie.DirectorName.ToLower().Contains(searchQueryLower) ||
                    movie.DirectorLastName.ToLower().Contains(searchQueryLower) ||
                    movie.Year.ToString().Contains(SearchQuery)
                ));
                SortMovies();
            }
        }


        private MovieVM selectedMovie;
        public MovieVM SelectedMovie
        {
            get => selectedMovie;
            set
            {
                selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                IsDetailsVisible = Visibility.Visible;
            }
        }

        private Visibility isDetailsVisible= Visibility.Collapsed;
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
                if(closeDetails == null)
                {
                    closeDetails = new DelegateCommand(param => CloseDetailsMovie(), null);
                }
                return closeDetails;
            }
        }
        void CloseDetailsMovie()
        {
            IsDetailsVisible = Visibility.Collapsed;
        }
    }
    
}
