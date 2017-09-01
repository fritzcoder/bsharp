namespace BSharp.Domain
{
	using System;
    using System.Collections.Generic;
    using Bsharp.Domain;

	public class Tier
	{
		public int Level { get; set; }
		public List<Battle> Battles { get; set; }
        public bool Active { get; set; }

		public Tier() { }
		public Tier(int level)
		{
			Level = level;
			Battles = new List<Battle>();
		}

		public IEnumerable<Song> GetWinners()
		{
			foreach (var battle in Battles)
			{
				yield return battle.Winner;
			}
		}
	}
}
