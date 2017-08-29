namespace Bsharp.Repository
{
	using System;
	using System.Collections.Generic;
    using Bsharp.Repository.Domain;

    public interface IBSharpRepository
    {
        void DeleteSong(string id);
        void CreateSong(Guid userId, Song song);
        Song Song(string name, string artistName, string albumName);
        IEnumerable<Song> Songs();

        void CreateArena(string title, IEnumerable<Song> songs);
        Arena Arena(string title);
        IEnumerable<Arena> Arenas();

        //void CreateBattle(DateTime time, Song song1, Song song2);
        void SubmitForNextBattle(Song song);

        void CreateUser(User user);
        void DeleteUser(string email);
        void UpdateUser(User user);
        User User(string email);
    }
}
