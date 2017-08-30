namespace Bsharp.Domain
{
	using System;
	using System.Collections.Generic;

    public class Artist
    {
        public string Name { get; set; }
        public IEnumerable<string> Members { get; set; }
        public DateTime Established { get; set; }

        public Artist() {}
        public Artist(string name, IEnumerable<string> members, 
                      DateTime established)
        {
            Name = name;
            Members = members;
            Established = established;
        }
    }
}
