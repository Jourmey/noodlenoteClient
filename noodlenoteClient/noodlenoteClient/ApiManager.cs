using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace noodlenoteClient
{
    class ApiManager
    {

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

        public void  GetNoteBookAll() 
        {
            
            HttpResponseMessage response = this._client.GetAsync("notebook/all").Result;
            var jsonData = response.Content.ReadAsByteArrayAsync().Result;

            NoteBook noteBook = JsonSerializer.Deserialize<NoteBook>()
        }


    }
}
