namespace Bsharp.Api.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Arena
    {
        private int _size;
        private string _title;

        public IEnumerable<Tier> Tiers  { get; }
        public IEnumerable<Song> Songs  { get; }
        public Song Song                { get; }

        public Arena(string title, IEnumerable<Song> songs) 
        {
            if (songs.Count() % 2 != 0)
            {
                throw new System.ArgumentException("Sorry man," 
                + "only even number of songs can be used.");
            }

            Songs = songs;
            _title = title;
            Tiers = CreateTiers(songs);
        }

		private List<Tier> CreateTiers(IEnumerable<Song> songs)
		{
            var tiers = new List<Tier>();
            int tierCount = 0;

            var tier = Songs.ToList().Count();

            for (int i = 0; i < Songs.ToList().Count(); i++)
            {
                tierCount = tierCount / 2;

                if (i == 0)
                {
                    tiers.Add(CreateTier(songs));
                }
                else
                {
                    tiers.Add(new Tier());
                }

                if(tierCount == 1)
                {
                    break;
                }
            }

            return tiers;
		}

        private Tier CreateTier(IEnumerable<Song> songs)
        {
            var stack = new Stack<Song>(songs);
            var tier = new Tier();

            while(stack.Any())
            {
                tier.Battles.Add(new Battle(stack.Pop(), stack.Pop()));
            }

            return tier;
        }
    }

    public class Tier
    {
        public List<Battle> Battles  { get; }
        public List<Song> Winners    { get; }

        public Tier()
        {
            Battles = new List<Battle>();
            Winners = new List<Song>();
        }

    }
}
