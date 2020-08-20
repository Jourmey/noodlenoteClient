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
    /// NoteEdit.xaml 的交互逻辑
    /// </summary>
    public partial class NoteEdit : UserControl
    {

        private Note _currentNote;

        public event EventHandler<Note> PushNote;

        public NoteEdit()
        {
            InitializeComponent();
        }

        public void UpdateNote(Note note)
        {
            if (note == null)
            {
                return;
            }
            this._currentNote = note;

            this.Lable_Note.Content = note.Title;
            this.Lable_Author.Content = note.Author;
            this.Lable_CreatAt.Content = note.CreatedAt.ToShortDateString();

            this.TextBox_Note.Text = note.Content;
        }


        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            PushNote?.Invoke(this, this._currentNote);
        }
    }
}
