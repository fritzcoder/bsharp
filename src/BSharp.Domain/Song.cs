namespace Bsharp.Domain
{
    using System;

    public class Song
    {
        public string Id            { get; set; }
        public string UserEmail     { get; set; }
        public Artist Artist        { get; set; }
        public string Name          { get; set; }
        public string Album         { get; set; }
        //InSeconds
        public int Length           { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int VoteCount        { get; set; }

        public Song() {}

        public Song(string userEmail, Artist artist, string name,
                    string album, int length, DateTime released)
        {
            string id = "{0}{1}{2}{3}";

            Artist = artist;
            Name = name;
            Album = album;
            ReleaseDate = released;
            UserEmail = userEmail;
            VoteCount = 0;
            Length = length;
            Id = string.Format(id, Artist.Name, Name, Album, Length)
                       .Replace(" ", string.Empty).ToLower();
        }
    }
}
