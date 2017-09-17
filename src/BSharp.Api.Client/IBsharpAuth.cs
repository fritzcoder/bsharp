
namespace BSharp.Api.Client
{
	using System;
	using System.Threading.Tasks;

    public interface IBsharpAuth
    {
        Task<string> Authenticate(string email, string password);
        Task<string> Validate(string token);
    }
}