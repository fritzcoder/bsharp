using System;
namespace Bsharp.Api.Models
{
    public interface IBSharpRepository
    {
        void AddSong(Guid userId, Song song);
        void CreateBattle(DateTime time, Song song1, Song song2);
        void SubmitForNextBattle(Song song);


    }
}
