namespace Bsharp.Client.Test
{
	using System;
    using System.IO;
	using Xunit;
    using BSharp.Api.Client;
    using System.Threading.Tasks;
    using Bsharp.Domain;

          
    public class ClientTest
    {
        private string URL = "http://localhost:5000/";

        [Fact]
        public async void CreateUserTestAsync()
        {
            var client = new BsharpClient(URL);
            var user = await client.CreateUser("test@bsharp.io", "testhandle",
                                               "testpassword");
            
            Assert.Equal("test@bsharp.io",user.Email);
        }


		[Fact]
		public async void GetUserAsync()
		{
			var client = new BsharpClient(URL);
			var user = await client.User("test@bsharp.io");

			Assert.Equal("test@bsharp.io", user.Email);
		}

		[Fact]
		public async void GetArenaAsync()
        {
			var client = new BsharpClient(URL);
			var arena = await client.Arena("testarena1");

            Assert.Equal("testarena1", arena.Title);   
        }

		[Fact]
		public async void CreateSongAsync()
		{
			var client = new BsharpClient(URL);
            var s = new Song("test@bsharp.io", "testartist", "testsong",
                             "testalbum", 300, DateTime.Now);
            
            var bytes = File.ReadAllBytes("test.mp3");
            var song = await client.CreateSong(bytes, s);

            Assert.Equal("testsong", song.Name);
		}
    }
}
