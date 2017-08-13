using System;
namespace Bsharp.Api.Models
{
    public class Song
    {
        public User User { get; set;}
        public Artist Artist        { get; set; }
        public string Name          { get; set; }
        public string Album         { get; set; }
        //InSeconds
        public string Length        { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int VoteCount        { get; set; }

        public Song(User user, Artist artist, string name, 
                    string album, DateTime released)
        {
            Artist = artist;
            Name = name;
            Album = album;
            ReleaseDate = released;
        }
    }
}
