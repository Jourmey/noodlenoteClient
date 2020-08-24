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
        private NoteBook _currentBook;
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


        private void Button_Init_Click(object sender, RoutedEventArgs e)
        {
            if (this._apiManager.UpdateBook())
            {
                this.UC_Book.InitBooks(this._apiManager.Books.Values.ToList());
            }
        }

        private void BookNoteList_NoteChange(object sender, NoteOp e)
        {
            if (e.Op == Operator.Change)
            {
                this._currentNote = e.Note;
                this.UC_Note.UpdateNote(e.Note);
            }

        }

        private void BookNoteList_BookChange(object sender, BookOp e)
        {
            if (e.Op == Operator.Change)
            {
                this._currentBook = e.Book;
                if (this._apiManager.BookIDToNoteID.ContainsKey(e.Book.ID))
                {
                    List<Note> notes = new List<Note>();

                    var noteIDs = this._apiManager.BookIDToNoteID[e.Book.ID];
                    if (noteIDs != null && noteIDs.Count != 0)
                    {
                        foreach (var id in noteIDs)
                        {
                            var n = this._apiManager.GetOrPullNote(id);
                            notes.Add(n);
                        }
                    }

                    this.UC_Book.InitNotes(notes);
                }
            }

        }

        private void UC_Note_PushNote(object sender, Note e)
        {
            if (e != null)
            {
                MessageBox.Show(_apiManager.UpdateNote(e) ? "Push Success" : "Push Failed");
            }
        }

    }
}
