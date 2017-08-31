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

        // GET api/arenas/5
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
    }
}
