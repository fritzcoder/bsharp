using System;
using System.Net.Http;
using System.Threading.Tasks;
using Bsharp.Domain;
using Newtonsoft.Json;

namespace BSharp.Api.Client
{
    public class BsharpArena : IBsharpArena
    {
        private HttpClient _client;

        public BsharpArena(HttpClient client)
        {
            _client = client;
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
    }
}
