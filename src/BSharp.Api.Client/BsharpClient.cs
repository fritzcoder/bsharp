namespace BSharp.Api.Client
{
    using Bsharp.Domain;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class BsharpClient
    {
        private string _url;
        private HttpClient _client;

        public BsharpClient(string url)
        {
            _url = url;
        }

        public User CreateUser(User user)
        {
            
        }
    }
}
