using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BSharp.Api.Client
{
    public class BsharpAuth : IBsharpAuth
    {
        private HttpClient _client;

        public BsharpAuth(HttpClient client)
        {
            _client = client; 
        }

        public async Task<string> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Validate(string token)
        {
            throw new NotImplementedException();
        }
    }
}
