using System;
using System.Collections.Generic;
using System.Text;
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
    /// BookNoteList.xaml 的交互逻辑
    /// </summary>
    public partial class BookNoteList : UserControl
    {
        public BookNoteList()
        {
            InitializeComponent();
        }

        public void InitBooks(List<NoteBook> books)
        {
            this.ListBox_Books.ItemsSource = books;
        }
        public void InitNotes(List<Note> notes)
        {
            this.ListBox_Notes.ItemsSource = notes;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            this.SplitView_Books.IsPaneOpen = !this.SplitView_Books.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SplitView_Books.IsPaneOpen = false;
            if (!(this.ListBox_Books.SelectedItem is NoteBook))
            {
                return;
            }

            var b = this.ListBox_Books.SelectedItem as NoteBook;

            BookChange?.Invoke(this, b);

        }

        public event EventHandler<NoteBook> BookChange;
        public event EventHandler<Note> NoteChange;

        private void ListBox_Notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(this.ListBox_Notes.SelectedItem is Note))
            {
                return;
            }

            var n = this.ListBox_Notes.SelectedItem as Note;
            NoteChange?.Invoke(this, n);
        }
    }
}
