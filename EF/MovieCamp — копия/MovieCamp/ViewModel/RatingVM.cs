using AcademyGroupMVVM.ViewModels;
using MovieCamp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCamp.ViewModel
{
    internal class RatingVM: ViewModelBase
    {
        Rating rating;
        public RatingVM(Rating rtn)
        {
            rating = rtn;
        }
        public int UserId
        {
            get { return rating.User.Id; }
            set { rating.User.Id = value; OnPropertyChanged(nameof(UserId)); }
        }
        public int MovieId
        {
            get { return rating.Movie.Id; }
            set { rating.Movie.Id = value; OnPropertyChanged(nameof(MovieId)); }
        }
        public double Grade
        {
            get { return rating.Grade; }
            set { rating.Grade = value; OnPropertyChanged(nameof(Grade)); }
        }
    }
}
