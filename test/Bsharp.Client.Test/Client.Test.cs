namespace Bsharp.Client.Test
{
	using System;
	using Xunit;
    using BSharp.Api.Client;
    using System.Threading.Tasks;
    using Bsharp.Domain;
          
    public class UnitTest1
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
    }
}
