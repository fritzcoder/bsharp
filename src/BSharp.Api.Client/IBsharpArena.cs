namespace BSharp.Api.Client
{
	using System;
	using System.Threading.Tasks;
	using Bsharp.Domain;

    public interface IBsharpArena
    {
        Task<Arena> Arena(string title);
    }
}
