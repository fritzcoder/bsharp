namespace Bsharp.Repository
{
	using System;
	using System.Collections.Generic;
    using Bsharp.Repository.Domain;

    public interface IBSharpRepository
    {
        void DeleteSong(Song song);
        void UpdateSong(Song song);
        void CreateSong(Guid userId, Song song);
        Song Song();
        IEnumerable<Song> Songs();

        void CreateArena(string title, IEnumerable<Song> songs);
        void DeleteArena(string title);
        Arena Arena(string title);
        IEnumerable<Arena> Arenas();

        void CreateBattle(DateTime time, Song song1, Song song2);
        void SubmitForNextBattle(Song song);

        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        User User(string email);
    }
}
