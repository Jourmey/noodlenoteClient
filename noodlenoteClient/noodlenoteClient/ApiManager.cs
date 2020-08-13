using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<bool> Ping()
        {
            //// Call asynchronous network methods in a try/catch block to handle exceptions
            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");

            //    response.EnsureSuccessStatusCode();

            //    string responseBody = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(responseBody);
            //}


            HttpResponseMessage response = await this._client.GetAsync("ping");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            return true;
        }

    }
}
