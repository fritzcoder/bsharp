namespace BSharp.Api.Client
{
	using System;
	using System.Threading.Tasks;
    using Bsharp.Domain;

    public interface IBsharpSong
    {
        Task<Song> CreateSong(byte[] file, Song song);
    }
}
