namespace BSharp.Api.Client
{
	using System;
    using System.Collections.Generic;
    using System.Net.Http;
	using System.Threading.Tasks;
	using Bsharp.Domain;
    using Newtonsoft.Json;

    public class BsharpUser : IBsharpUser
    {
        private HttpClient _client;

        public BsharpUser(HttpClient client)
        {
            _client = client;
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

        public async Task<User> User(string email)
        {
			var result =
				await _client.GetAsync(string.Format("/api/users/{0}",
													 email));

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
