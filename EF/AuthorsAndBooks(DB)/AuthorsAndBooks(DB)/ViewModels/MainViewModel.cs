using AcademyGroupMVVM.ViewModels;
using AuthorsAndBooks_DB_.Commands;
using AuthorsAndBooks_DB_.Models;
using AuthorsAndBooks_DB_.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Reflection.Metadata.BlobBuilder;

namespace AuthorsAndBooks_DB_.ViewModels
{
    internal class MainViewModel: ViewModelBase
    {
        private ObservableCollection<BookViewModel> originalBooksList;
        public ObservableCollection<AuthorViewModel> AuthorsList { get; set; }
        public ObservableCollection<BookViewModel> booksList { get; set; }

        bool editableBook = true;
        public MainViewModel(IQueryable<Book> books, IQueryable<Author> authors )
        {
            AuthorsList = new ObservableCollection<AuthorViewModel>(authors.Select(a => new AuthorViewModel(a)));
            BooksList = new ObservableCollection<BookViewModel>(books.Select(b => new BookViewModel(b)));
            originalBooksList = new ObservableCollection<BookViewModel>(books.Select(b => new BookViewModel(b)));
        }

        private string authorName;

        public string AuthorName
        {
            get { return authorName; }
            set
            {
                authorName = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }

        private string bookTitle;
        public string BookTitle
        {
            get => bookTitle;
            set
            {
                bookTitle = value;
                OnPropertyChanged(nameof(BookTitle));
            }
        }

        private int index_selected_book = -1;

        public int Index_selected_book
        {
            get { return index_selected_book; }
            set
            {
                index_selected_book = value;
                OnPropertyChanged(nameof(Index_selected_book));
            }
        }

        private int index_selected_author = -1;

        public int Index_selected_author
        {
            get { return index_selected_author; }
            set
            {
                index_selected_author = value;
                OnPropertyChanged(nameof(Index_selected_author));
                Filter();
            }
        }

        private bool checkedChange = false;
        public bool CheckedChange
        {
            get { return checkedChange; }
            set
            {
                checkedChange = value;
                OnPropertyChanged(nameof(checkedChange));
            }
        }

        public ObservableCollection<BookViewModel> BooksList
        {
            get { return booksList; }
            set
            {
                booksList = value;
                OnPropertyChanged(nameof(BooksList));
            }
        }
        public ObservableCollection<BookViewModel> OriginalBooksList
        {
            get { return originalBooksList; }
            set
            {
                originalBooksList = value;
                OnPropertyChanged(nameof(OriginalBooksList));
            }
        }
        private DelegateCommand addAuthorCommand;

        public DelegateCommand AddAuthorCommand
        {
            get
            {
                if (addAuthorCommand == null)
                {
                    addAuthorCommand = new DelegateCommand(param =>AddAuthor(), null);
                }
                return addAuthorCommand;
            }
        }

        void AddAuthor()
        {
            try
            {
                using (var db = new AuthorsAndBooksContext())
                {
                    Editor editor = new Editor();
                    editor.txtblock.Text = "Добавление автора";
                    if (editor.ShowDialog()==true)
                    {
                        if (Editor.name == string.Empty) throw new Exception("Incorrect name");
                        var author = new Author { Name = Editor.name}; 
                        db.Authors.Add(author);
                        db.SaveChanges();
                        var authorvm = new AuthorViewModel(author); 
                        AuthorsList.Add(authorvm);
                        MessageBox.Show("Author added");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DelegateCommand editAuthorCommand;

        public DelegateCommand EditAuthorCommand
        {
            get
            {
                if (editAuthorCommand == null)
                {
                    editAuthorCommand = new DelegateCommand(param => EditAuthor(), param => CanEcecute_author());
                }
                return editAuthorCommand;
            }
        }

        void EditAuthor()
        {
            try
            {
                using (var db = new AuthorsAndBooksContext())
                {
                    Editor editor = new Editor();
                    var author1 = AuthorsList[Index_selected_author];
                    var query = db.Authors.Where(a => a.Name == author1.Name).Single();
                    editor.txtblock.Text = "Изменение автора";
                    editor.txt1.Text = query.Name;
                    if (editor.ShowDialog() == true)
                    {
                        if (Editor.name == string.Empty) throw new Exception("Incorrect name");
                        query.Name = Editor.name;
                        db.SaveChanges();
                        AuthorsList[Index_selected_author] = new AuthorViewModel(query);
                        BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList);
                        foreach (BookViewModel book in BooksList)
                        {
                            if (book.AuthorName == author1.Name)
                            {
                                book.AuthorName = Editor.name;
                            }
                        }
                        MessageBox.Show("Author Edited");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanEcecute_author()
        {
            return Index_selected_author != -1;
        }
        
        private DelegateCommand removeAuthorCommand;

        public ICommand RemoveAuthorCommand
        {
            get
            {
                if (removeAuthorCommand == null)
                {
                    removeAuthorCommand = new DelegateCommand(param => RemoveAuthor(), param => CanEcecute_author());
                }
                return removeAuthorCommand;
            }
        }

        private void RemoveAuthor()
        {
            try
            {
                var delauthor = AuthorsList[Index_selected_author];
                DialogResult result = MessageBox.Show("Вы действительно желаете удалить автора " + delauthor.Name +
                    " ?", "Удаление автора", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return;
                using (var db = new AuthorsAndBooksContext())
                {
                    var query = (from a in db.Authors.Include(b=>b.Books)
                                where a.Name == delauthor.Name
                                select a).Single();
                    db.Authors.RemoveRange(query);
                    db.SaveChanges();
                    AuthorsList.Remove(delauthor);
                    var books = OriginalBooksList.Where(b => b.AuthorName == delauthor.Name).ToList();
                    foreach(var book in books)
                    OriginalBooksList.Remove(book);
                    BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList);
                    MessageBox.Show("Author deleted!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DelegateCommand addBookCommand;

        public DelegateCommand AddBookCommand
        {
            get
            {
                if (addBookCommand == null)
                {
                    addBookCommand = new DelegateCommand(param => AddBook(), param =>CanEcecute_author());
                }
                return addBookCommand;
            }
        }
        void AddBook()
        {
            try
            {
                using (var db = new AuthorsAndBooksContext())
                {
                    Editor editor = new Editor();
                    editor.txtblock.Text = "Добавление книги к текущему автору";
                    if (editor.ShowDialog() == true)
                    {
                        if (Editor.name == string.Empty) throw new Exception("Incorrect title");
                        var currentAuthor = AuthorsList[Index_selected_author];
                        var query = db.Authors.Where(x=> x.Name == currentAuthor.Name).Single();
                        var book = new Book { Title = Editor.name, Author = query};
                        db.Books.Add(book);
                        db.SaveChanges();
                        var bookvm = new BookViewModel(book);
                        OriginalBooksList.Add(bookvm);
                        BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList);
                        MessageBox.Show("Book added");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DelegateCommand editBookCommand;

        public DelegateCommand EditBookCommand
        {
            get
            {
                if (editBookCommand == null)
                {
                    editBookCommand = new DelegateCommand(param => EditBook(), param => CanEcecute_book());
                }
                return editBookCommand;
            }
        }

        void EditBook()
        {
            try
            {
                using (var db = new AuthorsAndBooksContext())
                {
                    Editor editor = new Editor();
                    var book1 = OriginalBooksList[Index_selected_book];
                    var query = db.Books
                         .Include(b => b.Author)  
                         .Where(b => b.Title == book1.Title)
                         .Single();
                    editor.txtblock.Text = "Изменение книги";
                    editor.txt1.Text = query.Title;
                    if (editor.ShowDialog() == true)
                    {
                        if (Editor.name == string.Empty) throw new Exception("Incorrect title");
                        query.Title = Editor.name;
                        db.SaveChanges();
                        OriginalBooksList[Index_selected_book] = new BookViewModel(query);
                        BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList);
                        MessageBox.Show("Book edited");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanEcecute_book()
        {
            return Index_selected_book != -1 && editableBook;
        }

        private DelegateCommand removeBookCommand;

        public ICommand RemoveBookCommand
        {
            get
            {
                if (removeBookCommand == null)
                {
                    removeBookCommand = new DelegateCommand(param => RemoveBook(), param => CanEcecute_book());
                }
                return removeBookCommand;
            }
        }

        private void RemoveBook()
        {
            try
            {
                var delbook = OriginalBooksList[Index_selected_book];
                DialogResult result = MessageBox.Show("Вы действительно желаете удалить книгу " + delbook.Title +
                    " ?", "Удаление книги", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return;
                using (var db = new AuthorsAndBooksContext())
                {
                    var query = from b in db.Books
                                where b.Title == delbook.Title
                                select b;
                    db.Books.RemoveRange(query);
                    db.SaveChanges();
                    OriginalBooksList.Remove(delbook);
                    BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList);
                    MessageBox.Show("Book deleted!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private DelegateCommand filterCommand;
        public DelegateCommand FilterCommand
        {
            get
            {
                if(filterCommand == null)
                {
                    filterCommand = new DelegateCommand(param=>Filter(), param=>CanEcecute_author());
                }
                return filterCommand;
            }
        }
        private void Filter()
        {
            
            if (CheckedChange && Index_selected_author >= 0 && Index_selected_author < AuthorsList.Count)
            {
                editableBook = false;
                var selectedAuthor = AuthorsList[Index_selected_author];
                BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList.Where(book => book.AuthorName == selectedAuthor.Name)); 
            }
            else
            {
                editableBook = true;
                BooksList = new ObservableCollection<BookViewModel>(OriginalBooksList);
            }
        }
    }
}
