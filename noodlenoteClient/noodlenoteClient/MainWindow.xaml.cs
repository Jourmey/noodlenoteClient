using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace noodlenoteClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private ApiManager _apiManager;
        private Book _currentBook;
        private Note _currentNote;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this._apiManager = new ApiManager();
        }


        private void Button_Ping_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this._apiManager.Ping().ToString());
        }


        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(this.ListBox_BookList.SelectedItem is Book))
            {
                return;
            }
            this._currentBook = this.ListBox_BookList.SelectedItem as Book;
            this.Lable_BookTitle.Content = this._currentBook.ToInfo();
            updateNoteList(this._currentBook.Notes);
        }

        private void ListBox_NoteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(this.ListBox_NoteList.SelectedItem is Note))
            {
                return;
            }
            this._currentNote = this.ListBox_NoteList.SelectedItem as Note;
            this.Lable_NoteTitle.Content = this._currentNote.ToInfo();
            this.TextBox_Note.Text = this._currentNote.Content;
        }
        private void updateBookList(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                return;
            }

            this.ListBox_BookList.ItemsSource = books;
        }

        private void updateNoteList(List<Note> notes)
        {
            if (notes == null || notes.Count == 0)
            {
                return;
            }

            this.ListBox_NoteList.ItemsSource = notes;
        }

        private void Button_Init_Click(object sender, RoutedEventArgs e)
        {
            this._apiManager.InitBook();
            updateBookList(this._apiManager.Books);
        }
    }
}
