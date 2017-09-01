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
        public void Post(IFormFile file, Song song)
        {
            using (var f = System.IO.File.Create(song.Id + ".mp3"))
            {
                file.CopyTo(f);
				_repo.CreateSong(song);
            }
        }
		
        [HttpGet]
        public IEnumerable<Song> Get()
        {
            return _repo.Songs();
        }

		[HttpGet]
		public Song Get([FromQuery]string name, [FromQuery]string artistName, 
                                     [FromQuery]string albumName)
		{
            return _repo.Song(name,artistName,albumName);
		}
    }
}
