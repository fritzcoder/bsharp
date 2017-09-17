namespace BSharp.Api.Client
{
    using Bsharp.Domain;
    using System.Net.Http;
    using System;
    using System.Threading.Tasks;

    public class BsharpClient : IBsharpAuth, IBsharpSong, IBsharpUser, 
    IBsharpArena
    {
        private const string HOME_BASE = "http://localhost:5000/";
        private IBsharpAuth _auth;
        private IBsharpSong _song;
        private IBsharpUser _user;
        private IBsharpArena _arena;
        private string _url;
        private HttpClient _client;

        public BsharpClient()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(HOME_BASE)
            };

            _auth = new BsharpAuth(_client);
            _song = new BsharpSong(_client);
            _user = new BsharpUser(_client);
            _arena = new BsharpArena(_client);
        }

        public BsharpClient(IBsharpAuth auth, IBsharpUser user, 
                            IBsharpSong song, IBsharpArena arena)
        {
            _auth = auth;
            _user = user;
            _song = song;
            _arena = arena;
        }

        public async Task<User> CreateUser(string email, string handle,
                                           string password)
        {
            return await _user.CreateUser(email, 
                                          handle, password);
        }

		public async Task<User> User(string email)
        {
            return await _user.User(email);
        }

        public async Task<Arena> Arena(string title)
        {
            return await _arena.Arena(title);
        }

        public async Task<Song> CreateSong(byte[] file, Song song)
        {
            return await _song.CreateSong(file, song);
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
