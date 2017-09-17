namespace BSharp.Api.Client
{
	using System;
	using System.Threading.Tasks;
    using Bsharp.Domain;

    public interface IBsharpUser
    {
        Task<User> CreateUser(string email, string handle,
                              string password);

        Task<User> User(string email);
    }
}
