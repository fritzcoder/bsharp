namespace BSharp.Api.Client
{
	using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
	using Bsharp.Domain;
    using Newtonsoft.Json;

    public class BsharpSong : IBsharpSong
    {
        HttpClient _client;
        public BsharpSong(HttpClient client)
        {
            _client = client;
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
					Name = "file",
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
