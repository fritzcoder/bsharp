namespace Bsharp.Domain
{
    using System;
    public class Battle
    {
        public DateTime Date    { get; set; }
        public Song Song1       { get; set; }
        public Song Song2       { get; set; }
        public Song Winner      { get; set; }


        public Battle(){}
        public Battle(Song song1, Song song2)
        {
            Song1 = song1;
            Song2 = song2;
        }

        public Song GetWinner()
        {
            if(Song1.VoteCount == Song2.VoteCount)
            {
                throw 
                new Exception("There is a tie battle cannot determine a winner");    
            }

            if(Song1.VoteCount > Song2.VoteCount)
            {
                Winner = Song1;
                return Winner;
            }

            Winner = Song2;
            return Winner;
        }

        public void Vote(Song song)
        {
            song.VoteCount = song.VoteCount++;
        }
    }
}
