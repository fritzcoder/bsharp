namespace Bsharp.Api.Controllers
{
    using Bsharp.Repository;
    using System.Linq;
    using Bsharp.Domain;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ArenasController : Controller
    {
        private IBSharpRepository _repo;

        public ArenasController(IBSharpRepository repo)
        {
            _repo = repo;
        }

        // GET api/arenas
        [HttpGet]
        public IEnumerable<Arena> Get()
        {
            var arenas = _repo.Arenas();
            return arenas;
        }

        // GET api/arenas/nameofarena
        [HttpGet("{title}")]
        public Arena Get(string title)
        {
            if(title == "current")
            {
                var current = _repo.Arenas().LastOrDefault();
                return current;
            }

			var arena = _repo.Arena(title);
			return arena;
        }

        [HttpPost]
        public void Post(string arenaName, int tierNumber, string songId, 
                         bool vote)
        {
            var arena = _repo.Arena(arenaName);

            if(arena.CurrentTier == tierNumber)
            {
                var tier = arena.GetCurrentTier();

                var song1 = tier.Battles
                                .Select(s => s.Song1)
                                .FirstOrDefault(x => x.Id == songId);
                
				var song2 = tier.Battles
                                .Select(s => s.Song2)
                                .FirstOrDefault(x => x.Id == songId);
                
                if(song1 != null)
                {
                    song1.VoteCount++;
                }
                else
                {
                    song2.VoteCount++;    
                }

                _repo.UpdateArena(arena);
            }
        }
    }
}
