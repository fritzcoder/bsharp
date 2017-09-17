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
            if (title == "current")
            {
                var current = _repo.Arenas().LastOrDefault();
                return current;
            }

            var arena = _repo.Arena(title);
            return arena;
        }

		[HttpPost]
		[Route("Vote")]
        public void Post(string arenaName, int tierNumber,
                         string songId)
        {
            var token = Request.Headers["Authorization"];
            var email = token;

            var arena = _repo.Arena(arenaName);

            var battle = arena.Tiers[tierNumber]
                 .Battles
                 .Select(x => x)
                 .FirstOrDefault(b => b.Song1.Id == songId || 
                                 b.Song2.Id == songId);


            if (battle != null && arena.CurrentTier == tierNumber)
            {
                var vote = new Vote(arena.Id, battle.Id, email, tierNumber,
                                    songId);
                _repo.Vote(vote);
            }
        }
    }
}
