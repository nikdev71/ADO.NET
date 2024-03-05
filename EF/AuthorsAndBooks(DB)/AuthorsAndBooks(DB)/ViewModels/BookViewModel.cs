using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace AuthorsAndBooks_DB_.ViewModels
{
    internal class BookViewModel : ViewModelBase
    {
        Book Book;
        public BookViewModel(Book book)
        {
            Book = book;
        }
        public string Title
        {
            get { return Book.Title; }
            set
            {
                Book.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string AuthorName
        {

            get {  return Book.Author.Name;  }
            set
            {
                Book.Author.Name = value; 
                OnPropertyChanged(nameof(AuthorName));
            }
        }
    }
}
