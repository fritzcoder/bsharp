namespace Bsharp.Api.Controllers
{
    using Bsharp.Repository;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        IBSharpRepository _repo;

        public AuthController(IBSharpRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public string Test()
        {
            return "Auth controller";
        }

        [HttpPost]
		[Route("Authenticate")]
        public IActionResult Authenticate(string email, string password)
        {
            return new ObjectResult("this.token.created");
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate(string token)
        {
            if (token == "this.token.created")
            {
                return new ObjectResult(true);
            }

            return new ObjectResult(false);
        }
    }
}
