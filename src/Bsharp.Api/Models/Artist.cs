namespace Bsharp.Api.Models
{
	using System;
	using System.Collections.Generic;

    public class Artist
    {
        public string Name                  { get; set; }
        public IEnumerable<string> Members  { get; set; }
        public DateTime Established         { get; set; }

        public Artist()
        {
        }
    }
}
