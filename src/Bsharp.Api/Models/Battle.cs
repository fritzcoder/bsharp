namespace Bsharp.Api.Models
{
    using System;
    public class Battle
    {
        public DateTime Date    { get; set; }
        public Song Song1       { get; set; }
        public Song Song2       { get; set; }
        public Song Winner      { get; set; }

        public Battle(Song song1, Song song2)
        {
            Song1 = song1;
            Song2 = song2;
        }

        public Song GetWinner()
        {
            if(Song1.VoteCount > Song2.VoteCount)
            {
                return Song1;
            }

            return Song2;
        }
    }
}
