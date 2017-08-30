namespace Bsharp.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Arena
    {
        private int _size;
        public string Title             { get; set; }
        public List<Tier> Tiers         { get; set; }
        public int CurrentTier          { get; set; }
        public Song Winner              { get; set; }

        public Arena(string title, IEnumerable<Song> songs) 
        {
            if (songs.Count() % 2 != 0)
            {
                throw new ArgumentException("Sorry man," 
                + "only an even number of songs can be used.");
            }

            CurrentTier = 0;
            _size = songs.ToList().Count();
            Title = title;
            Tiers = CreateTiers(songs);
        }

		private List<Tier> CreateTiers(IEnumerable<Song> songs)
		{
            var tiers = new List<Tier>();
            int tierCount = _size;

            for (int i = 0; i < _size; i++)
            {
                tierCount = tierCount / 2;

                if (i == 0)
                {
                    tiers.Add(CreateTier(songs, i));
                }
                else
                {
                    tiers.Add(new Tier(i));
                }

                if(tierCount == 2)
                {
                    break;
                }
            }

            return tiers;
		}

        private Tier CreateTier(IEnumerable<Song> songs, int level)
        {
            var stack = new Stack<Song>(songs);
            var tier = new Tier(level);

            while(stack.Any())
            {
                tier.Battles.Add(new Battle(stack.Pop(), stack.Pop()));
            }

            return tier;
        }

        public void CreateNextTier()
        {
            var winners = new List<Song>();

            foreach(var battle in Tiers[CurrentTier].Battles)
            {
                try
                {
					var song = battle.GetWinner();
					winners.Add(song);
                }
                catch(Exception ex)
                {
                    throw new Exception("Next tier cannot be created. There is" +
                                       " a tie! " +
                                        ex.Message);
                }
            }

            CurrentTier++;

            if ((CurrentTier) < Tiers.Count())
            {
                Tiers[CurrentTier] = CreateTier(winners, CurrentTier);

            }
            else
            {
                CurrentTier--;
                Winner = winners.First();
            }
        }

        public Tier GetCurrentTier()
        {
            return Tiers[CurrentTier];
        }
    }

    public class Tier
    {
        public int Level            { get; set; }
        public List<Battle> Battles { get; set; }


        public Tier() {}
        public Tier(int level)
        {
            Level = level;
            Battles = new List<Battle>();
        }

        public IEnumerable<Song> GetWinners()
        {
            foreach(var battle in Battles)
            {
                yield return battle.Winner;
            }
        }
    }
}
