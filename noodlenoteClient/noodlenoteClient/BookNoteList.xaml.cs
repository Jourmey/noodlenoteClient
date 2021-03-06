﻿using System;
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
        public event EventHandler<BookOp> BookChange;
        public event EventHandler<NoteOp> NoteChange;

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

            this.Lable_Book.Content = b.Name;
            this.Lable_CreatAt.Content = b.CreatedAt.ToShortDateString();

            BookChange?.Invoke(this, new BookOp { Book = b, Op = Operator.Change });
        }


        private void ListBox_Notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(this.ListBox_Notes.SelectedItem is Note))
            {
                return;
            }

            var n = this.ListBox_Notes.SelectedItem as Note;
            NoteChange?.Invoke(this, new NoteOp { Note = n, Op = Operator.Change });
        }

        private void RadioButton_AddBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_AddNote_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
