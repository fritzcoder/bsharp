namespace BSharp.Api.Client
{
    using Bsharp.Domain;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class BsharpClient
    {
        private string _url;
        private HttpClient _client;

        public BsharpClient(string url)
        {
			_client = new HttpClient();
			_client.BaseAddress = new Uri(url);
			_url = url;
        }

        public async Task<User> CreateUser(string email, string handle,
                                           string password)
        {
			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("Email", email),
				new KeyValuePair<string, string>("Handle", handle),
                new KeyValuePair<string, string>("Password", password)
			});

			var result = await _client.PostAsync("/api/users/",
												 content);
			if (result.IsSuccessStatusCode)
			{
				var resultContent = await result.Content.ReadAsStringAsync();
				return (JsonConvert.DeserializeObject<User>(
                    resultContent));
			}
			
				
		    throw new Exception("Could not create account.");
        }
    }
}
