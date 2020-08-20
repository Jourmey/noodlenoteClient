using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace noodlenoteClient
{
    class ApiManager
    {

        public Dictionary<int, NoteBook> Books { get; private set; } = new Dictionary<int, NoteBook>();
        public Dictionary<int, Note> Notes { get; private set; } = new Dictionary<int, Note>();
        public Dictionary<int, List<int>> BookIDToNoteID { get; private set; } = new Dictionary<int, List<int>>();


        private HttpClientHandler _handler;
        private HttpClient _client;

        public ApiManager()
        {
            // Create an HttpClientHandler object and set to use default credentials
            this._handler = new HttpClientHandler();
            this._handler.UseDefaultCredentials = true;

            // Create an HttpClient object
            this._client = new HttpClient(this._handler);
            this._client.BaseAddress = new Uri("http://localhost:9000/");

        }

        public bool Ping()
        {
            try
            {
                HttpResponseMessage response = this._client.GetAsync("ping").Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }

                return string.Equals(response.Content.ReadAsStringAsync().Result, "Pong");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<NoteBook> GetNoteBookAll()
        {
            HttpResponseMessage response = this._client.GetAsync("notebook/all").Result;
            var jsonData = response.Content.ReadAsStreamAsync().Result;
            var noteBook = JsonSerializer.DeserializeAsync<List<NoteBook>>(jsonData).Result;

            return noteBook;
        }



        internal List<int> GetNoteIDs(int id)
        {
            HttpResponseMessage response = this._client.GetAsync($"notebook/list/{id}").Result;
            var jsonData = response.Content.ReadAsStringAsync().Result;
            var noteids = JsonSerializer.Deserialize<List<int>>(jsonData);

            return noteids;
        }

        internal Note GetNoteByID(int noteid)
        {
            HttpResponseMessage response = this._client.GetAsync($"note/get/{noteid}").Result;
            var jsonData = response.Content.ReadAsStreamAsync().Result;
            var note = JsonSerializer.DeserializeAsync<Note>(jsonData).Result;

            return note;
        }

        internal Note GetOrPullNote(int id)
        {
            if (this.Notes.ContainsKey(id))
            {
                return this.Notes[id];
            }
            else
            {
                return UpdateNote(id);
            }
        }

        public bool UpdateBook()
        {
            var books = this.GetNoteBookAll();
            if (books == null || books.Count == 0)
            {
                return true;
            }

            foreach (var oneBook in books)
            {
                this.Books.Add(oneBook.ID, oneBook);
                var noteids = this.GetNoteIDs(oneBook.ID);
                if (noteids != null && noteids.Count != 0)
                {
                    this.BookIDToNoteID.Add(oneBook.ID, noteids);
                }
            }

            return true;
        }

        public Note UpdateNote(int noteid)
        {
            var note = GetNoteByID(noteid);
            if (!this.Notes.ContainsKey(note.ID))
            {
                this.Notes.Add(note.ID, note);
            }
            else
            {
                this.Notes[note.ID] = note;
            }
            return note;
        }

    }
}
