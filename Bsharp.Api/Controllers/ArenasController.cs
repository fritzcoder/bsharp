namespace Bsharp.Api.Controllers
{
    using Bsharp.Repository;
    using Bsharp.Api.Models;
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


        [Route("api/[controller]/current")]
		public Arena GetCurrent()
		{
            var arena = _repo.Arenas().LastOrDefault();
			return arena;
		}

        // GET api/arenas/5
        [HttpGet("{title}")]
        public Arena Get(string title)
        {
			var arena = _repo.Arena(title);
			return arena;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Arena value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
