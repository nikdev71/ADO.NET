using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorsAndBooks_DB_.ViewModels
{
    internal class AuthorViewModel: ViewModelBase
    {
        private Author Author;

        public AuthorViewModel(Author author)
        {
            Author = author;
        }
        public string Name
        {
            get {  return Author.Name; }
            set 
            { 
                Author.Name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}
