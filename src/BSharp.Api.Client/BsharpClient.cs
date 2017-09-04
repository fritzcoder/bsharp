namespace BSharp.Api.Client
{
    using Bsharp.Domain;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.IO;

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

        public async Task<Arena> Arena(string title)
        {
			var result =
				await _client.GetAsync(string.Format("/api/arenas/{0}",
													 title));

			if (result.IsSuccessStatusCode)
			{
				var resultContent = await result.Content.ReadAsStringAsync();
				return (JsonConvert.DeserializeObject<Arena>(
					resultContent));
			}

			throw new Exception("Could not find arena.");
        }

        public async Task<Song> CreateSong(byte[] file, Song song)
        {
			var content = new MultipartFormDataContent();

			var values = new[]
			{
				new KeyValuePair<string, string>("Id", song.Id),
				new KeyValuePair<string, string>("Name", song.Name)
			};

			foreach (var keyValuePair in values)
			{
				content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
			}

			var fileContent = new ByteArrayContent(file);
			fileContent.Headers.ContentDisposition = 
                new ContentDispositionHeaderValue("form-data")
			{
                Name="file",
				FileName = "Upload.mp3"
			};

			content.Add(fileContent);

			var result = await _client.PostAsync("/api/songs/",
												 content);
			if (result.IsSuccessStatusCode)
			{
				var resultContent = await result.Content.ReadAsStringAsync();
				return (JsonConvert.DeserializeObject<Song>(
					resultContent));
			}

			throw new Exception("Could not create song.");
		}
    }
}
