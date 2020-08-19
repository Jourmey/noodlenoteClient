using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace noodlenoteClient
{
    class ApiManager
    {

        public List<Book> Books { get; private set; } = new List<Book>();

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
            HttpResponseMessage response = this._client.GetAsync("ping").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }

            return string.Equals(response.Content.ReadAsStringAsync().Result, "Pong");
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

        public bool InitBook()
        {
            var books = this.GetNoteBookAll();
            if (books == null || books.Count == 0)
            {
                return true;
            }

            this.Books = new List<Book>();

            foreach (var oneBook in books)
            {
                Book book = new Book(oneBook);
                var noteids = this.GetNoteIDs(oneBook.ID);
                if (noteids != null && noteids.Count != 0)
                {
                    foreach (var noteid in noteids)
                    {
                        var one = this.GetNoteByID(noteid);
                        book.Notes.Add(one);
                    }
                }
                this.Books.Add(book);
            }

            return true;
        }

    }
}
