namespace Bsharp.Domain
{
    using System;

    public class Vote
    {
        public Guid Id          { get; set; }
        public string Email     { get; set; }
        public Guid ArenaId     { get; set; }
        public int TierNumber   { get; set; }
        public string SongId    { get; set; }


        public Vote(Guid arenaId, string email, int tierNumber, string songId )
        {
            Email = email;
            ArenaId = arenaId;
            TierNumber = tierNumber;
            SongId = songId;
        }

        public Vote()
        {
            
        }
    }
}
