namespace Bsharp.Api.Controllers           
{
    using System.Collections.Generic;
    using Bsharp.Domain;
    using Bsharp.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class SongsController : Controller
    {
        IBSharpRepository _repo;

        public SongsController(IBSharpRepository repo)
        {
            _repo = repo;
        }

		[HttpPost]
		public void Post([FromBody]IFormFile file, [FromBody]Song value)
		{

		}

        [HttpGet]
        public IEnumerable<Song> Get()
        {
            return _repo.Songs();
        }

		[HttpGet]
		public Song Get(string name, string artistName, 
                                     string albumName)
		{
            return _repo.Song(name,artistName,albumName);
		}
    }
}
