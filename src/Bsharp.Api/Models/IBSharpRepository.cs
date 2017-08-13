using System;
namespace Bsharp.Api.Models
{
    public interface IBSharpRepository
    {
        void AddSong();
        void CreateBattle(DateTime time, Song song1, Song song2);
        void SubmitForNextBattle(Song song);


    }
}
